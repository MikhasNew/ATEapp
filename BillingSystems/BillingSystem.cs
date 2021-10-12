using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATEapp
{
     class BillingSystem
    {

        private readonly ConcurrentDictionary<Guid, CallingSession> _currentCallingSessions;
        public IEnumerable<KeyValuePair<Guid, CallingSession>> CurrentCallingSessions
        {
            get
            {
                foreach (var cs in _currentCallingSessions)
                {
                    yield return cs;
                }
            }
        }
        public List<CallingSession> BillingSystemHistory { get; }

        private readonly List<Account> _accounts = new List<Account>();

        public BillingSystem()
        {
            _currentCallingSessions = new ConcurrentDictionary<Guid, CallingSession>();
            BillingSystemHistory = new List<CallingSession>();
        }
        internal void AddAccount(Account account)
        {
            if (_accounts.All(a => a.ClientId != account.ClientId))
            {
                account.GetReporttEvent += Account_AccauntEvent;
                _accounts.Add(account);
            }
        }

        private void Account_AccauntEvent(object sender, AccauntEvenArgs<List<CallingSession>> accauntEvenArgs)
        {
            accauntEvenArgs.ResponseData = BillingSystemHistory.Where(cs => cs.PortNumber == accauntEvenArgs.PortNumber).ToList();
        }
        
        internal void AddCurrentCallingSessions(CallingSession cs)
        {
            var key = Guid.NewGuid();
            cs.SetSesionKey(key);
            cs.CallingSessionClosed += Cs_CallingSessionClosed;
            cs.CallingSessionTerminated += Cs_CallingSessionTerminated;
            _currentCallingSessions.TryAdd(key, cs);
        }

        private void Cs_CallingSessionTerminated(object sender, CallingSessionEventArgs e)
        {
            CallingSession cs = (CallingSession)sender;
            CallingSession temp;
            _currentCallingSessions.TryRemove(cs.SesionKey, out temp);
        }

        private void Cs_CallingSessionClosed(object sender, CallingSessionEventArgs e)
        {
            CallingSession cs = (CallingSession)sender;
            BillingSystemHistory.Add(cs);
            WriteOffSumm(cs);

            CallingSession temp;
            _currentCallingSessions.TryRemove(cs.SesionKey, out temp);
        }

        private void WriteOffSumm(CallingSession e)
        {
            var ac = _accounts.First(a => a.PortNumber == e.PortNumber);
            int callingTime = (int)e.Timer.Elapsed.TotalSeconds;
            
            decimal coe;
            switch (ac.AccountTyp)
            {
                case AccountTypes.Base:
                    coe = 0.04m;
                    break;
                case AccountTypes.Gold:
                    coe = 0.06m;
                    break;
                case AccountTypes.Silver:
                    coe = 0.05m;
                    break;
                default:
                    coe = 0.06m;
                    break;
            }
            decimal summ = callingTime * coe * 0.1m;
            e.SetSesionCost(summ);
            ac.WriteOffSumm(summ);


        }

    }
}