using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATEapp
{
    class Terminal
    {
        public ConsoleColor ConsoleColorTerminal { get; }
        public Port TerminalPort { get; private set; }

        public Terminal( ConsoleColor consoleColor)
        {
            ConsoleColorTerminal = consoleColor;
        }
        public void AddPort(Port terminalPort)
        {
            if (TerminalPort == null)
                TerminalPort = terminalPort;
            //TerminalPort.СhangePortStateEvent += TerminalPort_СhangePortStateEvent;
            TerminalPort.ATEMessageEvent += TerminalPort_ATEMessageEvent;
        }

        public void GreateOutCalling(int number)
        {
            ShowTerminalMessage($"Сonsumer calling to {number}");
            TerminalPort.SetPortState(PortState.OutCall, calledNumber:number) ;
        }
        public void AcceptCalling(int number)
        {
            ShowTerminalMessage("The yes button is pressed, confirmation of connection...");
            TerminalPort.SetPortState(PortState.InCall, number);
        }
        public void ClouseCalling()
        {
            ShowTerminalMessage($"Сonnection terminated.");
            TerminalPort.SetPortState(PortState.Open);
        }
        public void RejectCalling()
        {
            ShowTerminalMessage("consumer reject a calling");
            TerminalPort.SetPortState(PortState.Open);
        }
        public void TurnOff()
        {
            ShowTerminalMessage("consumer turn off the terminal");
            TerminalPort.SetPortState(PortState.Bloked);
            TerminalPort.ATEMessageEvent -= TerminalPort_ATEMessageEvent;
        }
        public void TurnOn()
        {
            ShowTerminalMessage("consumer turn on the terminal");
            TerminalPort.SetPortState(PortState.Open);
            TerminalPort.ATEMessageEvent += TerminalPort_ATEMessageEvent;
        }

        private void TerminalPort_СhangePortStateEvent(object sender, PortEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void TerminalPort_ATEMessageEvent(object sender, PortEventArgs e)
        {
            if (e.PortState == PortState.InCall)
            {
                ShowTerminalMessage($"Incoming call from {e.CalledNumber}. \r\n" +
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

        private void ShowTerminalMessage(string message)
        {
            var fc = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColorTerminal;
            Console.WriteLine($"Terminal {this.TerminalPort.PortNumber} infomation: {message}");
            Console.ForegroundColor = fc;
        }


    }
}

