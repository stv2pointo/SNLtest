using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteveJulienSNLtest.Models
{
    public class HourlyEmployee : Employee
    {
        public override double getPeriodGrossPay()
        {
            return salary * currentHours;
        }
    }
}
