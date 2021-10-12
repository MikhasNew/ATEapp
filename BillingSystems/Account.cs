using System;
using System.Collections.Generic;
using System.Linq;

namespace ATEapp
{
    public class Account
    {
        public delegate void GetReporttHandler(object sender,  AccauntEvenArgs<List<CallingSession>> accauntEvenArgs);
        public event GetReporttHandler GetReporttEvent;
        
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
        public void GetReport(SortBy sortBy)
        {

            var data = new AccauntEvenArgs<List<CallingSession>>(AccauntEventType.GetReport, this.PortNumber, null);
            GetReporttEvent?.Invoke(this, data);
            List<CallingSession> SortedList;
            switch (sortBy)
            {
                case SortBy.Date:
                    SortedList = data.ResponseData.OrderBy(i => i.Date).ToList();
                    break;
                case SortBy.Time:
                    SortedList = data.ResponseData.OrderBy(i => i.Timer.Elapsed.TotalMilliseconds).ToList();
                    break;
                case SortBy.Cost:
                    SortedList = data.ResponseData.OrderBy(i => i.Cost).ToList();
                    break;
                case SortBy.CalledNumber:
                    SortedList = data.ResponseData.OrderBy(i => i.CalledNumber).ToList();
                    break;
                default:
                    SortedList = data.ResponseData;
                    break;
            }
            Console.WriteLine($"The report consumer {this.PortNumber}");
            Console.WriteLine($"{"Date connecting"}         {"duration "}    {"called subscriber"}    {"cost"}");
            foreach (CallingSession item in SortedList)
            {
                 Console.WriteLine($"{item.Date:dd MMMM yyyy}          {item.Timer.Elapsed:\\:mm\\:ss}         {item.CalledNumber}      {item.Cost}");
            }

            Console.WriteLine($"\r\n");

        }
    }
}