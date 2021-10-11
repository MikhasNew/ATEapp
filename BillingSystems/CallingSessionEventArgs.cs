using System;

namespace ATEapp
{
     public class CallingSessionEventArgs 
    {
        public DateTime DateSession { get; }
        public TimeSpan TimeSession { get; }
        public int PortNumber { get; }
        public int CalledNumber { get; }
        public Guid SesionKey { get; }
        
        public CallingSessionEventArgs(DateTime dateSession, TimeSpan timeSession, int portNumber, int calledNumber, Guid sesionKey)
        {
            DateSession = dateSession;
            TimeSession = timeSession;
            PortNumber =  portNumber ;
            CalledNumber = calledNumber;
            SesionKey = SesionKey;
        }
    }
}