using SteveJulienSNLtest.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SteveJulienSNLtest
{
    class Program
    { 
        static void Main(string[] args)
        {
            //new TimeTest().run();
            PayPeriodDataFactory factory = new PayPeriodDataFactory(null);
   
            PayrollEntry[] payrollEntries = factory.getPayrollEntries();
            Console.WriteLine("Pay period factory created");

            PayrollEntry[] sortedPayrollEntries = Sorter.sortPayrollEntriesByGross(payrollEntries);
            Console.WriteLine("Sorted by gross");

            PayCheckReport paycheckReport = new PayCheckReport(null, sortedPayrollEntries);
            paycheckReport.writePaychecksToFile();
            Console.WriteLine("Paychecks written");

            Top15PercentEarnersReport topEarners = new Top15PercentEarnersReport(null, sortedPayrollEntries);
            topEarners.writeTopEarnersReportToFile();
            Console.WriteLine("Top Earners written");

            new StateReportFactory(null, payrollEntries);
            Console.WriteLine("state reports written");

            Console.ReadLine();
        }

    }
}
