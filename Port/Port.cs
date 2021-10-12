using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATEapp
{
    class Port : IMessager
    {
        public delegate Task EventHandler(object sender, PortEventArgs e);
        public event EventHandler СhangePortStateEvent;

       // public event EventHandler<PortEventArgs> СhangePortStateEvent;
        public event EventHandler<PortEventArgs> ATEMessageEvent;

        public ConsoleColor TextColor { get; }
        public Port TerminalPort { get; }
        
        public PortState PortState { get; private set; }
        public int PortNumber { get; }

        public Port(int portNumber, ConsoleColor consoleColor = default)
        {
            TerminalPort = this;
            TextColor = consoleColor;
            PortState = PortState.Open;
            PortNumber = portNumber;
        }
        protected virtual void SetPortStateOn(PortEventArgs e)
        {
            Messager.ShowMessage(this,$"Port checked to {e.PortState}");
            PortState = e.PortState;
            СhangePortStateEvent?.Invoke(this, e);
        }

        
        public void SetPortState(PortState portState, int calledNumber=0, int callingNumber=0)
        {
            SetPortStateOn(new PortEventArgs(portState,  calledNumber, callingNumber));
        }

        public void  SetATEComand(PortState portState, int calledNumber=0, int callingNumber=0)
        {
            ATEMessageEvent.Invoke(this, new PortEventArgs(portState, calledNumber, callingNumber));
        }

        public PortState GetPortState()
        {
            return PortState;
        }
        
    }
}
