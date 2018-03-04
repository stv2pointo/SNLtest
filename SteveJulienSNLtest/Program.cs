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
            PayrollEntry[] sortedPayrollEntries = new Sorter().sortPayrollEntriesByGross(payrollEntries);
            PayCheckReport paycheckReport = new PayCheckReport(null, sortedPayrollEntries);
            paycheckReport.writePaychecksToFile();
            Top15PercentEarnersReport topEarners = new Top15PercentEarnersReport(null, sortedPayrollEntries);
            topEarners.writeTopEarnersReportToFile();


            Console.ReadLine();
        }

    }
}
