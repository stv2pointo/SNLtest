using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteveJulienSNLtest.Models
{
    public class SalaryEmployee : PayrollEntry
    {
        public SalaryEmployee(string[] values) : base(values)
        {
        }

        public override double getPeriodGrossPay()
        {
            return salary / (Constants.WEEKS_PER_YEAR / Constants.WEEKS_PER_PAY_PERIOD);
        }
    }
}
