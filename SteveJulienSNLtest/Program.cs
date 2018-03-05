using SteveJulienSNLtest.Models;
using System;
using System.Threading;

namespace SteveJulienSNLtest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Payroll Report Generator.\n");

            PayPeriodDataFactory factory = null;
            while (factory == null)
            {
                factory = makeFactory();
            }

            Console.WriteLine("Processing payroll checks...\n");

            EmployeePayrollEntry[] entries = factory.getPayrollEntries();

            EmployeePayrollEntry[] sortedPayrollEntries =
                Sorter.sortPayrollEntriesByGross(entries);

            Console.Write("Would you like comma delimited results? Enter y/n? ");
            string delimiter = Console.ReadLine();
            delimiter = delimiter.ToLower();
            delimiter = (delimiter.Equals("y")) ? "," : "";

            PayCheckReport.run(delimiter, sortedPayrollEntries);
            Console.WriteLine("\nPaychecks report written to " + Constants.DEFAULT_PAYCHECKS_WRITE_PATH);

            Top15PercentEarnersReport.run(delimiter, sortedPayrollEntries);
            Console.WriteLine("Top Earners report written to " + Constants.DEFAULT_TOP_EARNERS_WRITE_PATH);

            StateReportFactory.run(delimiter, sortedPayrollEntries);
            Console.WriteLine("State reports written to " + Constants.DEFAULT_STATE_REPORT_WRITE_PATH);

            factory.writeDictionary();

            Console.Write("\nEnter q to quit, or hit enter to search employee payroll data ");
            string input = Console.ReadLine();
            input = input.ToLower();
            if (!input.Equals("q"))
            {
                EmployeeFetcher.run(factory);
            }


        }
        public static PayPeriodDataFactory makeFactory()
        {
            PayPeriodDataFactory factory = null;
            try
            {
                factory = new PayPeriodDataFactory(InputFileValidator.getInputPath());
            }
            catch (Exception e)
            {
                Console.WriteLine("Problem with input file : " + e.Message);
            }
            return factory;
        }
    }
}
