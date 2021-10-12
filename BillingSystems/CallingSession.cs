﻿using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ATEapp
{
    public class CallingSession
    {
        public event EventHandler<CallingSessionEventArgs> CallingSessionClosed;
        public event EventHandler<CallingSessionEventArgs> CallingSessionTerminated;

        public int PortNumber { get; }
        public int CalledNumber { get; }
        public decimal Cost { get; protected set; }
        public DateTime Date { get; protected set; }
        public Stopwatch Timer;
        public Guid SesionKey { get; private set; }
        public bool IsStarted { get; private set; }

        public CallingSession(int portNumber, int calledNumber)
        {
            Date = new DateTime();
            Timer = new Stopwatch();
            PortNumber = portNumber;
            CalledNumber = calledNumber;
        }
       
        public void StartCallingSession()
        {
            Date = DateTime.Now;
            IsStarted = true;
            Timer.Start();
           
        }
        public CallingSession StopCallingSession()
        {
            IsStarted = false;
            Timer.Stop();
            //Console.WriteLine("Callinc session closed.....");
            CallingSessionClosed?.Invoke(this, new CallingSessionEventArgs(Date, Timer.Elapsed, PortNumber, CalledNumber, SesionKey));
            return this;
        }
        public void SetSesionKey(Guid key)
        {
            SesionKey = key;
        }
        public void SetSesionCost(decimal suum)
        {
            Cost = suum;
        }
        internal void TerminateCallingSession()
        {
            CallingSessionTerminated?.Invoke(this, new CallingSessionEventArgs(Date, Timer.Elapsed, PortNumber, CalledNumber, SesionKey));
        }
    }
}