using SteveJulienSNLtest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteveJulienSNLtest
{
    public class PayrollEntrySorter
    {
        public PayrollEntry[] sortPayrollEntriesByGross(PayrollEntry[] employees)
        {
            Array.Sort(employees, delegate (PayrollEntry emp1, PayrollEntry emp2)
            {
                return emp2.getPeriodGrossPay().CompareTo(emp1.getPeriodGrossPay());
            });

            return employees;
        }

        
    }
}
