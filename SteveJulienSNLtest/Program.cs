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
            bool isDefaultInput = getIsDefaultInput();

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

            factory.writeDictionary();

            Console.Write("Hit q to quit, or hit enter to search employee payroll data ");
            string input = Console.ReadLine();
            input = input.ToLower();
            if (!input.Equals("q"))
            {
                employeeFetchLoop(factory);
            }
        
            Console.WriteLine("Thanks, have a great day.");
            Console.WriteLine("............");
        }

        private static bool getIsDefaultInput()
        {
            string input = "";
            while ( !input.Equals("y") && !input.Equals("n"))
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
                Console.Write("Hit q to quit, or hit enter to search again " );
                input = Console.ReadLine();
            }
        }

    }
}
