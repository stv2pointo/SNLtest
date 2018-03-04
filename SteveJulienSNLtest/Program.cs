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
            PayrollEntry[] sortedPayrollEntries = new PayrollEntrySorter().sortPayrollEntriesByGross(payrollEntries);
            PayCheckWriter writer = new PayCheckWriter(null, sortedPayrollEntries);
            writer.writePaychecksToFile();

            Console.ReadLine();
        }

    }
}
