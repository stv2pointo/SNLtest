using SteveJulienSNLtest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteveJulienSNLtest
{
    public class Sorter
    {
        public PayrollEntry[] sortPayrollEntriesByGross(PayrollEntry[] entries)
        {
            Array.Sort(entries, delegate (PayrollEntry emp1, PayrollEntry emp2)
            {
                return emp2.getPeriodGrossPay().CompareTo(emp1.getPeriodGrossPay());
            });

            return entries;
        }

        public List<TopEarnerModel> getSortedListGrossLastNameFirstName(PayrollEntry[] entries)
        {
            List<TopEarnerModel> topEarnerList = new List<TopEarnerModel>();
            for(int i = 0; i < entries.Length; i++)
            {
                PayrollEntry entry = entries[i];
                TopEarnerModel earner = new TopEarnerModel();
                earner.lastName = entry.lastName;
                earner.firstName = entry.firstName;
                earner.numYearsWorked = getNumYearsWorked(entry.startDate);
                earner.grossPay = entry.getPeriodGrossPay();
                topEarnerList.Add(earner);
            }

            var top = topEarnerList.OrderBy(e => e.grossPay).
                ThenBy(e => e.lastName).
                ThenBy(e => e.firstName).
                ToList();
            return top;
        }

        private int getNumYearsWorked(DateTime startDate)
        {
            DateTime now = DateTime.Now;
            TimeSpan timespan = now.Subtract(startDate);
            return (int) timespan.TotalDays / 365;
        }


    }
}
