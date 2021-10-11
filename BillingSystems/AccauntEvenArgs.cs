using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATEapp
{
    public class AccauntEvenArgs<T>
    {
        public AccauntEventType EventType { get; }
        public int PortNumber { get; }
        public T ResponseData { get; set; }

        public AccauntEvenArgs(AccauntEventType eventType, int portNumber, T data)
        {
            EventType = eventType;
            PortNumber = portNumber;
            ResponseData = data;
        }


    }
}
