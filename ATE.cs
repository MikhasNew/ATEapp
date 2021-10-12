using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATEapp
{
    class ATE : IMessager
    {
        public ConsoleColor TextColor { get; }
        public Port TerminalPort { get; }


        private List<ConsoleColor> ConsoleColorList = new List<ConsoleColor>();
        private Dictionary<int, Port> Ports = new Dictionary<int, Port>();

        private BillingSystem ATEBillingSystem;
        private int TerminalIterator = 1;

        public ATE()
        {
            TextColor = ConsoleColor.Yellow;
            TerminalPort = new Port(0, ConsoleColor.Green);
            
            ConsoleColorList = Enum.GetValues<ConsoleColor>().ToList();
            ATEBillingSystem = new BillingSystem();
        }

        public Account GreateContract(int clientId, AccountTypes accountTyp)
        {
            var account = new Account(clientId, accountTyp);
            ATEBillingSystem.AddAccount(account);
            return account;
        }
        public Terminal GreateTerminal(Account account)
        {
            if (TerminalIterator > 12)
                TerminalIterator = 1;

            var terminal = new Terminal(ConsoleColorList[TerminalIterator]);
            var terminalPort = new Port(new Random(account.ClientId).Next(), ConsoleColorList[TerminalIterator]);
            terminalPort.СhangePortStateEvent += TerminalPort_СhangePortStateEventAsync;
            terminal.AddPort(terminalPort);
            Ports.Add(terminal.TerminalPort.PortNumber, terminalPort);
            account.SetPortNumber(terminal.TerminalPort.PortNumber);

            TerminalIterator++;
            return terminal;
        }

        private async Task TerminalPort_СhangePortStateEventAsync(object sender, PortEventArgs e)
        {
            var initiator = (Port)sender;

            if (e.PortState == PortState.OutCall)
            {
                if (Ports[e.CalledNumber].PortState == PortState.Open)
                {
                   Messager.ShowMessage(this,$"Connection {initiator.PortNumber} to {e.CalledNumber}...");
                   
                    ATEBillingSystem.AddCurrentCallingSessions(new CallingSession(initiator.PortNumber, e.CalledNumber));
                    Ports[e.CalledNumber].SetATEComand(PortState.InCall, initiator.PortNumber);
                }
                else if (Ports[e.CalledNumber].PortState != PortState.Open)
                {
                    Messager.ShowMessage(this, $"The line is engaged...");
                    Ports[initiator.PortNumber].SetATEComand(PortState.Open);
                }
            }
            else if (e.PortState == PortState.InCall)
            {
                var callingSession = ATEBillingSystem.CurrentCallingSessions.Where(session => session.Value.CalledNumber == initiator.PortNumber
                                                                      && session.Value.PortNumber == e.CalledNumber).FirstOrDefault();
                Messager.ShowMessage(this, $"Connection {callingSession.Value.PortNumber} to {callingSession.Value.CalledNumber} is ready.\r\n");
                await Task.Run(() => callingSession.Value.StartCallingSession());
            }
            else if (e.PortState == PortState.Open)
            {
                if (ATEBillingSystem.CurrentCallingSessions.Count() > 0)
                {
                    var callingSession = ATEBillingSystem.CurrentCallingSessions.Where(session => session.Value.PortNumber == initiator.PortNumber
                                                                          || session.Value.CalledNumber == initiator.PortNumber).First();
                    if (callingSession.Value != null && callingSession.Value.IsStarted)
                    {
                        callingSession.Value.StopCallingSession();
                        Messager.ShowMessage(this, $"Connection {callingSession.Value.PortNumber} to {callingSession.Value.CalledNumber} is cloused.");

                        if (callingSession.Value.CalledNumber == initiator.PortNumber)
                        {
                            Ports[callingSession.Value.PortNumber].SetATEComand(PortState.Open);
                        }
                        else
                            Ports[callingSession.Value.CalledNumber].SetATEComand(PortState.Open);
                        
                    }
                    else
                    {
                        callingSession.Value.TerminateCallingSession();
                        Messager.ShowMessage(this, $"Connection {callingSession.Value.PortNumber} to {callingSession.Value.CalledNumber} is cloused.");
                        Ports[callingSession.Value.PortNumber].SetATEComand(PortState.Open);
                     }
                    Console.WriteLine($"\r\n");
                }
            }
        }
        
    }
}
