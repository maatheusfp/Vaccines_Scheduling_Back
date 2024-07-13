using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vaccines_Scheduling.Utility.Exceptions
{
    public class InvalidDateFormatException : Exception
    {
        public InvalidDateFormatException() { }
        public InvalidDateFormatException(string message) : base(message){ }
        public InvalidDateFormatException(string message, Exception exception) : base(message, exception) { }
    }
}
