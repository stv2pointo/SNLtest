using SteveJulienSNLtest.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SteveJulienSNLtest
{
    public static class Sorter
    {
        public static EmployeePayrollEntry[] sortPayrollEntriesByGross(EmployeePayrollEntry[] entries)
        {
            Array.Sort(entries, delegate (EmployeePayrollEntry emp1, EmployeePayrollEntry emp2)
            {
                return emp2.getPeriodGrossPay().CompareTo(emp1.getPeriodGrossPay());
            });

            return entries;
        }

        public static List<TopEarnerModel> getSortedListGrossLastNameFirstName(EmployeePayrollEntry[] entries)
        {
            List<TopEarnerModel> topEarnerList = new List<TopEarnerModel>();
            for (int i = 0; i < entries.Length; i++)
            {
                EmployeePayrollEntry entry = entries[i];
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

        public static List<EmployeePayrollEntry>[] sortEntriesIntoArrayOfEntryListsByState(EmployeePayrollEntry[] entries)
        {
            int numStates = (int)States.STATE_COUNT;
            List<EmployeePayrollEntry>[] stateEntries = new List<EmployeePayrollEntry>[numStates];

            for(int i = 0; i < numStates; i++)
            {
                stateEntries[i] = new List<EmployeePayrollEntry>();
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
                return report1.getName().CompareTo(report2.getName());
            });

            return reports;
        }
    }
}
