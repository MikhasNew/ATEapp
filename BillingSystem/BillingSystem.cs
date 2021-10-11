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
        
        private ConcurrentDictionary<Guid, CallingSession> currentCallingSessions;
        public IEnumerable<KeyValuePair<Guid, CallingSession>> CurrentCallingSessions
        {
            get
            {
                 foreach (var cs in currentCallingSessions)
                {
                    yield return cs;
                }
            }
        }
        public List<CallingSession> BillingSystemHistory { get; }

        private List<Account> Accounts = new List<Account>();
        public BillingSystem()
        {
            currentCallingSessions = new ConcurrentDictionary<Guid, CallingSession>();
            BillingSystemHistory = new List<CallingSession>();
        }
        internal void AddAccount(Account account)
        {
            if (!Accounts.Any(a => a.ClientId == account.ClientId))
                Accounts.Add(account);
        }

        internal void AddCurrentCallingSessions(CallingSession cs)
        {
            var key = Guid.NewGuid();
            cs.SetSesionKey(key);
            cs.CallingSessionClosed += Cs_CallingSessionClosed;
            cs.CallingSessionTerminated += Cs_CallingSessionTerminated;
            currentCallingSessions.TryAdd(key, cs);
        }

        private void Cs_CallingSessionTerminated(object sender, CallingSessionEventArgs e)
        {
            CallingSession cs = (CallingSession)sender;
            CallingSession temp;                                                
            currentCallingSessions.TryRemove(cs.SesionKey, out temp);
        }

        private void Cs_CallingSessionClosed(object sender, CallingSessionEventArgs e)
        {
            CallingSession cs = (CallingSession)sender;
            BillingSystemHistory.Add(cs);
            CallingSession temp;                                                
            currentCallingSessions.TryRemove(cs.SesionKey, out temp);
        }

        

    }
}
