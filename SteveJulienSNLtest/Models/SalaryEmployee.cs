using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteveJulienSNLtest.Models
{
    public class SalaryEmployee : Employee
    {
        public override double getPeriodGrossPay()
        {
            return salary / (PayPeriod.WEEKS_PER_YEAR / PayPeriod.WEEKS_PER_PAY_PERIOD);
        }
    }
}
