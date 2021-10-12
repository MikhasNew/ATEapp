using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATEapp
{
    class Terminal : IMessager
    {
        public ConsoleColor TextColor { get; }
        public Port IdObject { get; private set; }
        public Port TerminalPort { get; private set; }

        public Terminal(ConsoleColor consoleColor)
        {
            TextColor = consoleColor;
        }
        public void AddPort(Port terminalPort)
        {

            if (TerminalPort == null)
            {
                TerminalPort = terminalPort;
                IdObject = TerminalPort;
                TerminalPort.ATEMessageEvent += TerminalPort_ATEMessageEvent;
            }
        }

        public void GreateOutCalling(int number)
        {
            Messager.ShowMessage(this, $"Сonsumer calling to {number}");
            TerminalPort.SetPortState(PortState.OutCall, calledNumber: number);
        }
        public void AcceptCalling(int number)
        {
            Messager.ShowMessage(this, "The yes button is pressed, confirmation of connection...");
            TerminalPort.SetPortState(PortState.InCall, number);
        }
        public void ClouseCalling()
        {
            Messager.ShowMessage(this, $"Сonnection terminated.");
            TerminalPort.SetPortState(PortState.Open);
        }
        public void RejectCalling()
        {
            Messager.ShowMessage(this, "consumer reject a calling");
            TerminalPort.SetPortState(PortState.Open);
        }
        public void TurnOff()
        {
            Messager.ShowMessage(this, "consumer turn OFF the terminal");
            TerminalPort.SetPortState(PortState.Bloked);
            TerminalPort.ATEMessageEvent -= TerminalPort_ATEMessageEvent;
        }
        public void TurnOn()
        {
            Messager.ShowMessage(this, "consumer turn ON the terminal");
            TerminalPort.SetPortState(PortState.Open);
            TerminalPort.ATEMessageEvent += TerminalPort_ATEMessageEvent;
        }
        
        private void TerminalPort_ATEMessageEvent(object sender, PortEventArgs e)
        {
            if (e.PortState == PortState.InCall)
            {
                Messager.ShowMessage(this, $"Incoming call from {e.CalledNumber}. \r\n" +
                                           $"Press 'Y' to accept.");
                if (Console.ReadKey(true).Key == ConsoleKey.Y)
                {
                    AcceptCalling(e.CalledNumber);
                }
                else
                {
                    RejectCalling();
                }
            }
            if (e.PortState == PortState.Open && (TerminalPort.PortState == PortState.OutCall
                || TerminalPort.PortState == PortState.InCall))
            {
                ClouseCalling();
            }

        }
        
    }
}

