using System;
using System.Threading;

namespace ATEapp
{
    class Program
    {
        static void Main(string[] args)
        {
            ATE ate = new ATE();

            var client1 = new IndividualClient(11111111, "Billy", "Booms");
            client1.AddAccount(ate.GreateContract(client1.ClientId, AccountTypes.Base));   //ats.GreateContract(client1, AccountTypes.Base);
            client1.AddTerminal(ate.GreateTerminal(client1.Account));

            var client2 = new IndividualClient(22222222, "Dilly", "Dooms");
            client2.AddAccount(ate.GreateContract(client2.ClientId, AccountTypes.Base));  //ats.GreateContract(client2, AccountTypes.Base);
            client2.AddTerminal(ate.GreateTerminal(client2.Account));

            var client3 = new IndividualClient(33333333, "Willy", "Wooms");
            client3.AddAccount(ate.GreateContract(client3.ClientId, AccountTypes.Base));   //ats.GreateContract(client1, AccountTypes.Base);
            client3.AddTerminal(ate.GreateTerminal(client3.Account));

            client1.Terminal.GreateOutCalling(new Random(22222222).Next());
            Thread.Sleep(1000);
            client3.Terminal.GreateOutCalling(new Random(11111111).Next());
            Thread.Sleep(1000);
            client3.Terminal.GreateOutCalling(new Random(11111111).Next());
            Thread.Sleep(1000);
            client3.Terminal.GreateOutCalling(new Random(11111111).Next());
            Thread.Sleep(1000);
            client2.Terminal.ClouseCalling();
            Thread.Sleep(1000);
            client3.Terminal.GreateOutCalling(new Random(11111111).Next());
            //client1.Terminal.GreateOutCalling(new Random(11111111).Next());

            client1.Terminal.ClouseCalling();

            Console.ReadLine();
        }
    }
}
