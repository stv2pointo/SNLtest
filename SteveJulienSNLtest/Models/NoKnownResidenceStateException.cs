using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteveJulienSNLtest.Models
{
    public class NoKnownResidenceStateException : Exception
    {
        public NoKnownResidenceStateException()
        {
        }
        public NoKnownResidenceStateException(string message) : base(message)
        {
        }
    }
}
