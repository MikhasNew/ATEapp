using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ATEapp
{
    internal class CallingSession
    {
        public event EventHandler<CallingSessionEventArgs> CallingSessionClosed;
        public event EventHandler<CallingSessionEventArgs> CallingSessionTerminated;

        public DateTime Date { get; protected set; }
        public Stopwatch Timer;
        public Guid SesionKey { get; private set; }
        public bool IsStarted { get; private set; }

        public CallingSession(int portNumber, int calledNumber)
        {
            PortNumber = portNumber;
            CalledNumber = calledNumber;
        }
       
        public void StartCallingSession()
        {
            Date = DateTime.Today.Date;
            Timer = new Stopwatch();
            IsStarted = true;
            Timer.Start();
            // Thread.Sleep(40000);
            //StopCallingSession();
        }
        public CallingSession StopCallingSession()
        {
            IsStarted = false;
            Timer.Stop();
            Console.WriteLine("Callinc session closed.....");
            CallingSessionClosed?.Invoke(this, new CallingSessionEventArgs(Date, Timer.Elapsed, PortNumber, CalledNumber, SesionKey));
            return this;
        }
        public void SetSesionKey(Guid key)
        {
            SesionKey = key;
        }
        public int PortNumber { get; }
        public int CalledNumber { get; }

        internal void TerminateCallingSession()
        {
            CallingSessionTerminated?.Invoke(this, new CallingSessionEventArgs(Date, Timer.Elapsed, PortNumber, CalledNumber, SesionKey));
        }
    }
}