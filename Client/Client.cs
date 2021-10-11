using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ATEapp
{
    abstract class Client
    {
        public int ClientId { get; }
        public Account Account { get; private set; }
        public Terminal Terminal { get; private set; }

        protected Client(int id)
        {
            ClientId = id;
        }

        internal void AddTerminal(Terminal terminal)
        {
            Terminal = terminal;
        }

        internal void AddAccount(Account account)
        {
            Account = account;
        }

        
    }
}
