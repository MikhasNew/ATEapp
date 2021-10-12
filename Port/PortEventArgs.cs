namespace ATEapp
{
    public class PortEventArgs 
    {
        public PortState PortState { get;}
        public int CallingNumber { get; }
        public int CalledNumber { get; }
        public PortEventArgs(PortState portState,  int calledNumber  =0,int callingNumber = 0)
        {
            PortState = portState;
            CalledNumber = calledNumber;
            CallingNumber = callingNumber;
        }
    }
}