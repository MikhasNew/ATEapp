using System;

namespace ATEapp
{
    public class Account
    {
        public DateTime ChangeTariffDate { get; private set; }
        public decimal Summ { get; private set; }
        public AccountTypes AccountTyp { get; private set; }
        public int ClientId { get; }
        public int PortNumber { get; private set; }

        public Account(int clientID, AccountTypes accountTyp)
        {
            ChangeTariffDate = DateTime.Now;
            ClientId = clientID;
            AccountTyp = accountTyp;
            Summ = GetBonus(accountTyp);
        }
        public void SetPortNumber(int portNumber)
        {
            PortNumber = portNumber;
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
        public decimal GetBalance()
        {
            Console.WriteLine($"Dear customer {ClientId}, the balance is ${Summ}\r\n");
            return Summ;
        }
        public void WriteOffSumm(decimal summ)
        {
            Summ = Summ - summ;
           
        }
        public void AddSumm(decimal summ)
        {
            Summ = Summ + summ;
            Console.WriteLine($"Dear customer {ClientId}, ${summ} has been credited to your account. " +
                $" \r\nThe balance is ${Summ}\r\n");
        }
        public void ChangeTariff(AccountTypes accountType)
        {
            if (ChangeTariffDate.AddDays(30) < DateTime.Now) 
            {
                AccountTyp = accountType;
                Console.WriteLine($"Dear customer {ClientId}, the tariff changed.\r\n");
            }
            else
                Console.WriteLine($"Dear customer {ClientId}, the tariff can be changed every 30 days," +
                    $" the last shift {ChangeTariffDate}. \r\nPlease wait.\r\n");

        }
    }
}