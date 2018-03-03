using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteveJulienSNLtest.Models
{
   
    public abstract class Employee
    {
 
        public string Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string payType { get; set; }
        public double salary { get; set; }
        public DateTime startDate { get; set; }
        public string residenceState { get; set; }
        public double currentHours { get; set; }

        abstract public double getPeriodGrossPay();
    }

    public class PayPeriod
    {
        public static double WEEKS_PER_YEAR = 52;
        public static double WEEKS_PER_PAY_PERIOD = 2;
    }

}
