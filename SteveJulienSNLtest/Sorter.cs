using SteveJulienSNLtest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteveJulienSNLtest
{
    public static class Sorter
    {
        public static PayrollEntry[] sortPayrollEntriesByGross(PayrollEntry[] entries)
        {
            Array.Sort(entries, delegate (PayrollEntry emp1, PayrollEntry emp2)
            {
                return emp2.getPeriodGrossPay().CompareTo(emp1.getPeriodGrossPay());
            });

            return entries;
        }

        public static List<TopEarnerModel> getSortedListGrossLastNameFirstName(PayrollEntry[] entries)
        {
            List<TopEarnerModel> topEarnerList = new List<TopEarnerModel>();
            for (int i = 0; i < entries.Length; i++)
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

        private static int getNumYearsWorked(DateTime startDate)
        {
            DateTime now = DateTime.Now;
            TimeSpan timespan = now.Subtract(startDate);
            return (int)timespan.TotalDays / 365;
        }

        public static List<PayrollEntry>[] sortEntriesIntoArrayOfEntryListsByState(PayrollEntry[] entries)
        {
            int numStates = (int)States.STATE_COUNT;
            List<PayrollEntry>[] stateEntries = new List<PayrollEntry>[numStates];

            for(int i = 0; i < numStates; i++)
            {
                stateEntries[i] = new List<PayrollEntry>();
            }

            for (int i = 0; i < entries.Length; i++)
            {
                string stateName = entries[i].residenceState.getName();
                int stateIndex = (int)Enum.Parse(typeof(States), stateName);
                stateEntries[stateIndex].Add(entries[i]);      
            }
            
            return stateEntries;
        }

        public static StateReport[] sortStateReportByStateName(StateReport[] reports)
        {
            Array.Sort(reports, delegate (StateReport report1, StateReport report2)
            {
                return report2.getName().CompareTo(report1.getName());
            });

            return reports;
        }


    }
}
