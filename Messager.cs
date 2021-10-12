using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATEapp
{
    static class Messager
    {
        public static void ShowMessage(IMessager sender, string message)
        {
            var fc = Console.ForegroundColor;
            Console.ForegroundColor = sender.TextColor;
            Console.WriteLine($"{sender.GetType().Name} {sender.TerminalPort.PortNumber} infomation: {message}");
            Console.ForegroundColor = fc;
        }
    }
}
