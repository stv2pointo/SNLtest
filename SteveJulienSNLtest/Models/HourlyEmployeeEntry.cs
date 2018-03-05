using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteveJulienSNLtest.Models
{
    public class HourlyEmployeeEntry : EmployeePayrollEntry
    {
        public HourlyEmployeeEntry(string[] values) : base(values)
        {
        }

        public override double getPeriodGrossPay()
        {
            double pay = 0.0;
            if(currentHours >= 0)
            {             
                if(currentHours > 90)
                {
                    pay = salary * 80;
                    double eightyToNinety = salary * 10 * Constants.OVER_80_FACTOR;
                    pay += eightyToNinety;
                    double overNinety = (currentHours - 90) * Constants.OVER_90_FACTOR;
                    pay += overNinety;
                }
                else if(currentHours > 80)
                {
                    pay = salary * 80;
                    double overEighty = (currentHours - 80) * Constants.OVER_80_FACTOR;
                    pay += overEighty;
                }
                else
                {
                    pay = salary * currentHours;
                }
            }
            return pay;
        }

    }
}
