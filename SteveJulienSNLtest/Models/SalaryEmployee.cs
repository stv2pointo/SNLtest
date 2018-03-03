using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteveJulienSNLtest.Models
{
    public class SalaryEmployee : Employee
    {
        public SalaryEmployee(string[] values) : base(values)
        {
        }

        public override double getPeriodGrossPay()
        {
            return salary / (PayrollConstants.WEEKS_PER_YEAR / PayrollConstants.WEEKS_PER_PAY_PERIOD);
        }
    }
}
