namespace ATEapp
{
    public class Account
    {
        public decimal Summ { get; }
        public AccountTypes AccountTyp { get; }
        public int ClientId { get; }

        public Account(int clientID, AccountTypes accountTyp)
        {
            ClientId = clientID;
            AccountTyp = accountTyp;
            Summ = GetBonus(accountTyp);
        }

        private decimal GetBonus(AccountTypes tip)
        {
            switch (tip)
            {
                case AccountTypes.Gold:
                    return 500;
                case AccountTypes.Silver:
                    return 350;
                default:
                    return 100;
            }

        }
    }
}