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
        public static int ROWS_TO_COLLECT = 50000000;
        static void Main(string[] args)
        {
            Stopwatch readWatch = new Stopwatch();
            Stopwatch arrayWatch = new Stopwatch();
            Stopwatch dictWatch = new Stopwatch();
            Stopwatch sortWatch = new Stopwatch();
            Stopwatch payrollWatch = new Stopwatch();
            Stopwatch writeWatch = new Stopwatch();

            readWatch.Start();
            string[] rawLines = getLinesFromInputFile();
            readWatch.Stop();
            Console.WriteLine("REad took " + readWatch.Elapsed.TotalSeconds.ToString());

            arrayWatch.Start();
            Employee[] employees = createAndGetEmployeePayrollRecords(rawLines);
            arrayWatch.Stop();
            Console.WriteLine("Array took " + arrayWatch.Elapsed.TotalSeconds.ToString());

            dictWatch.Start();
            Dictionary<string, Employee> dictionary = getEmployeeDict(rawLines);
            dictWatch.Stop();
            Console.WriteLine("Dictionary took " + dictWatch.Elapsed.TotalSeconds.ToString());

            sortWatch.Start();
            Employee[] sortedPaychecks = sortEmployeesByGross(employees);
            sortWatch.Stop();
            Console.WriteLine("Sort took " + sortWatch.Elapsed.TotalSeconds.ToString());

            payrollWatch.Start();
            string[] paychecks = createPayrollLines(sortedPaychecks);
            payrollWatch.Stop();
            Console.WriteLine("Payroll lines took " + payrollWatch.Elapsed.TotalSeconds.ToString());

            writeWatch.Start();
            writePaychecksToFile(paychecks);
            writeWatch.Stop();
            Console.WriteLine("Write took " + writeWatch.Elapsed.TotalSeconds.ToString());

            Console.ReadLine();
        }

        private static Employee[] sortEmployeesByGross(Employee[] employees)
        {
            Array.Sort(employees, delegate (Employee emp1, Employee emp2)
            {
                return emp2.getPeriodGrossPay().CompareTo(emp1.getPeriodGrossPay());
            });

            return employees;
        }
    
        public static string[] getLinesFromInputFile()
        {
            int counter = 0;
            string line;
            List<string> fileLines = new List<string>();
            //string path = @"c:\SecurityNational\Employees.txt";
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\..\Resources\Employees.txt");
            try
            {
                System.IO.StreamReader file =
                    new System.IO.StreamReader(path);
                while ((line = file.ReadLine()) != null && counter < ROWS_TO_COLLECT)
                {
                    fileLines.Add(line);
                    counter++;
                }

                file.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return fileLines.ToArray();
        }

        public static void writePaychecksToFile(string[] paychecks)
        {
            try
            {
                System.IO.File.WriteAllLines(PayrollConstants.DEFAULT_PAYCHECKS_WRITE_PATH, paychecks);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public static Dictionary<string, Employee> getEmployeeDict(string[] lines)
        {
            Dictionary<string, Employee> employeeDict = new Dictionary<string, Employee>();
            foreach (string line in lines)
            {
                string[] fields = line.Split(',');
                string payType = fields[3];
                Employee emp = null;
                if (payType.ToUpper().Equals("H"))
                {
                    emp = new HourlyEmployee(fields);
                }
                else if (payType.ToUpper().Equals("S"))
                {
                    emp = new SalaryEmployee(fields);
                }
                else
                {
                    Console.WriteLine("ERROR: Unknown PayType for Employee Id: " + fields[0] + " Name: " +
                        fields[2] + ", " + fields[1]);
                }
                if (emp != null)
                {
                    employeeDict.Add(fields[0], emp);
                }
            }
            return employeeDict;
        }

        public static Employee[] createAndGetEmployeePayrollRecords(string[] rawLines)
        {
            Employee[] employees = new Employee[rawLines.Length];
            int index = 0;
            foreach (string line in rawLines)
            {
                string[] fields = line.Split(',');
                string payType = fields[3];
                Employee emp = null;
                if (payType.ToUpper().Equals("H"))
                {
                    emp = new HourlyEmployee(fields);
                }
                else if (payType.ToUpper().Equals("S"))
                {
                    emp = new SalaryEmployee(fields);
                }
                else
                {
                    Console.WriteLine("ERROR: Unknown PayType for Employee Id: " + fields[0] + " Name: " +
                        fields[2] + ", " + fields[1]);
                }
                if (emp != null)
                {
                    employees[index] = emp;
                    index++;
                }
            }
            return employees;
        }

        public static string[] createPayrollLines(Employee[] sorted)
        {
            string[] paychecks = new string[sorted.Length];

            for(int i = 0; i < sorted.Length; i++)
            {
                Employee e = sorted[i];
                string text = e.Id;
                text += " " + e.firstName;
                text += " " + e.lastName;
                text += " " + e.getPeriodGrossPay().ToString("F");
                text += " " + e.getFederalTax().ToString("F");
                text += " " + e.getStateTax().ToString("F");
                text += " " + e.getNetPay().ToString("F");
                paychecks[i] = text;
            }
            return paychecks;
        }

    }
}
