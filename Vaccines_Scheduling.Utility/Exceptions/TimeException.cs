using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vaccines_Scheduling.Utility.Exceptions
{
    public class InvalidTimeFormatException : Exception
    {
        public InvalidTimeFormatException() { }
        public InvalidTimeFormatException(string message) : base(message) { }
        public InvalidTimeFormatException(string message, Exception exception) : base(message, exception) { }
    }
}
