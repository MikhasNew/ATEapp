﻿namespace ATEapp
{
    public class PortEventArgs 
    {
        public PortState PortState { get; private set; }
        public int CallingNumber { get; private set; }
        public int CalledNumber { get; private set; }
        public PortEventArgs(PortState portState,  int calledNumber  =0,int callingNumber = 0)
        {
            PortState = portState;
            CalledNumber = calledNumber;
            CallingNumber = callingNumber;
        }
    }
}