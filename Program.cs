using System;
using System.Threading;

namespace ATEapp
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowDemonstration();
        }

        static void ShowDemonstration()
        {
            ATE ate = new ATE();
            var client1 = new IndividualClient(11111111, "Billy", "Booms");
            client1.AddAccount(ate.GreateContract(client1.ClientId, AccountTypes.Gold));
            client1.AddTerminal(ate.GreateTerminal(client1.Account));

            var client2 = new IndividualClient(22222222, "Dilly", "Dooms");
            client2.AddAccount(ate.GreateContract(client2.ClientId, AccountTypes.Silver));
            client2.AddTerminal(ate.GreateTerminal(client2.Account));

            var client3 = new IndividualClient(33333333, "Willy", "Wooms");
            client3.AddAccount(ate.GreateContract(client3.ClientId, AccountTypes.Base));
            client3.AddTerminal(ate.GreateTerminal(client3.Account));

            var client4 = new CorporateClient(44444444, "Gilly&Co", "StatName, Gooms str. 88");
            client4.AddAccount(ate.GreateContract(client4.ClientId, AccountTypes.Silver));
            client4.AddTerminal(ate.GreateTerminal(client4.Account));


            client1.Terminal.GreateOutCalling(new Random(33333333).Next());
            Thread.Sleep(3000);
            client1.Terminal.ClouseCalling();
            Thread.Sleep(3000);

            client1.Account.ChangeTariff(AccountTypes.Silver);
            client1.Account.GetBalance();
            client1.Account.AddSumm(30);

            client1.Terminal.TurnOff();
            client2.Terminal.GreateOutCalling(new Random(11111111).Next());
            Thread.Sleep(3000);
            client1.Terminal.TurnOn();

            client1.Terminal.GreateOutCalling(new Random(22222222).Next());
            Thread.Sleep(3000);

            client3.Terminal.GreateOutCalling(new Random(11111111).Next());
            Thread.Sleep(3000);
            client4.Terminal.GreateOutCalling(new Random(22222222).Next());
            Thread.Sleep(3000);

            client3.Terminal.GreateOutCalling(new Random(44444444).Next());
            Thread.Sleep(3000);

            client1.Terminal.ClouseCalling();
            Thread.Sleep(3000);


            client1.Account.GetBalance();
            Thread.Sleep(3000);
            client1.Account.GetReport(SortBy.Cost);
            Thread.Sleep(3000);
            client1.Account.GetReport(SortBy.CalledNumber);
            Thread.Sleep(3000);
            client1.Account.GetReport(SortBy.Date);
            Thread.Sleep(3000);

            client4.Terminal.ClouseCalling();
            Thread.Sleep(3000);


            client3.Account.GetBalance();
            Thread.Sleep(3000);
            client3.Account.GetReport(SortBy.Cost);
            Thread.Sleep(3000);

            client4.Account.GetBalance();
            Thread.Sleep(3000);
            client4.Account.GetReport(SortBy.Date);

            Console.ReadLine();
        }
    }
}
