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
            Console.WriteLine("Welcome to the Payroll Report Generator.\n");

            PayPeriodDataFactory factory = new PayPeriodDataFactory(getInputPath());

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

            factory.writeDictionary();

            Console.Write("Hit q to quit, or hit enter to search employee payroll data ");
            string input = Console.ReadLine();
            input = input.ToLower();
            if (!input.Equals("q"))
            {
                employeeFetchLoop(factory);
            }

            Console.WriteLine("Thanks, have a great day.");

            //Console.ReadLine();
        }

        private static string getInputPath()
        {
            if (isDefaultInputPrompt())
            {
                return Constants.DEFAULT_INPUT_PATH;
            }

            int tries = 0;
            string input = promptUser("Enter path of input file: ");

            while (!isValidFilename(input) || !isFileOfEmployeeRecords(input))
            {
                tries++;
                input = promptUser("Enter path of input file: ");
                if(tries == 3)
                {
                    Console.WriteLine("Failed to set path, proceeding with default file (Employees.txt).\n");
                    input = @"..\..\Resources\Employees.txt";
                    break;
                }        
            }
            return input;
        }

        private static string promptUser(string prompt)
        {
            string input = null;
            while (string.IsNullOrEmpty(input))
            {
                Console.Write(prompt);
                input = Console.ReadLine();
            }
            return input;
        }

        private static bool isValidFilename(string path)
        {
            bool isValid = false;
            if (path.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {

                isValid = true;
            }
            else
            {
                Console.Write("Invalid file name.\nEnter path of input file: ");
            }
            return isValid;
        }

        private static bool isFileOfEmployeeRecords(string path)
        {
            try
            {
                string line1 = File.ReadLines(path).First();
                string[] fields = line1.Split(',');
                PayrollEntry testEntry = new HourlyEmployee(fields);
            }
            catch
            {
                Console.WriteLine("File entered is not in the valid payroll format.");
                Console.WriteLine("File must have: Id, firstName, lastName, payType, salary, startDate, residenceState, hoursWorked.");
                Console.WriteLine("Example:\n1,JANE,DOE,H,29.77,9/5/05,NM,70");
                return false;
            }
            return true;
        }
        private static bool isDefaultInputPrompt()
        {
            string input = "";
            while (!input.Equals("y") && !input.Equals("n"))
            {
                Console.Write("Use default input file? (Employees.txt) Enter y/n ");
                input = Console.ReadLine();
                input = input.ToLower();
            }
            return (input.Equals("y")) ? true : false;
        }

        public static void employeeFetchLoop(PayPeriodDataFactory factory)
        {
            string input = "";
            while (string.IsNullOrEmpty(input))
            {
                Console.Write("Enter employee id: ");

                string id = Console.ReadLine();
                PayrollEntry employee = factory.GetByEmployeeId(id);
                string emp = "No employee by that id.";
                if (employee != null)
                {
                    emp =
                    "Employee Id:  " + employee.Id + "\n" +
                    "First Name:   " + employee.firstName + "\n" +
                    "Last Name:    " + employee.lastName + "\n" +
                    "Pay Type:     " + employee.payType + "\n" +
                    "Salary:      $" + employee.salary.ToString("F") + "\n" +
                    "Start Date:   " + employee.startDate.ToString() + "\n" +
                    "State of Res: " + employee.residenceState.getName() + "\n" +
                    "Hours worked: " + employee.currentHours.ToString("F");
                }
                Console.WriteLine(emp);
                Console.Write("Hit q to quit, or hit enter to search again ");
                input = Console.ReadLine();
            }
        }

    }
}
