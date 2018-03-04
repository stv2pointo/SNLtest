using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteveJulienSNLtest.Models
{
    public class HourlyEmployee : PayrollEntry
    {
        public HourlyEmployee(string[] values) : base(values)
        {
        }

        public override double getPeriodGrossPay()
        {
            double pay = -1000000;
            if(currentHours >= 0)
            {
                pay = salary * currentHours;
                
                if(currentHours > 90)
                {
                    pay *= PayrollConstants.OVER_90_FACTOR;
                }
                else if(currentHours > 80)
                {
                    pay *= PayrollConstants.OVER_80_FACTOR;
                }
            }
            return pay;
        }

    }
}
