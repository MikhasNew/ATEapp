using System;

namespace ATEapp
{
    internal interface IMessager
    {
        public ConsoleColor TextColor { get; }
        public Port TerminalPort { get; }
        
    }
}