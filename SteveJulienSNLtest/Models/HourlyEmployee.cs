using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteveJulienSNLtest.Models
{
    public class HourlyEmployee : Employee
    {
        public HourlyEmployee(string[] values) : base(values)
        {
        }

        public override double getPeriodGrossPay()
        {
            double grossPay = 0.0;
            if(currentHours >= 0)
            {
                double basePay = salary * currentHours;
                
                if(currentHours > 90)
                {
                    grossPay *= 1.75;
                }
                else if(currentHours > 80)
                {
                   grossPay *= 1.5;
                }
                else
                {
                    grossPay = basePay;
                }
            }
            return grossPay;
        }
    }
}
