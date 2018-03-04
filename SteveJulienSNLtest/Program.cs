using SteveJulienSNLtest.Models;
using System;
using System.Collections.Generic;
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
            string[] rawLines = getLinesFromInputFile();
            Employee[] employees = createAndGetEmployeePayrollRecords(rawLines);

            //Dictionary<string, Employee> dictionary = getEmployeeDict(rawLines);
            //Employee[] sortedPaychecks = sortDictionaryByGrossPayDesc(dictionary);
            Employee[] sortedPaychecks = sortEmployeesByGross(employees);
            createPayChecks(sortedPaychecks);
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
                while ((line = file.ReadLine()) != null)
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

        public static void writeToFile(string[] lines)
        {
            try
            {
                System.IO.File.WriteAllLines(@"C:\SecurityNational\WriteLines.txt", lines);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //public static Dictionary<string, Employee> getEmployeeDict(string[] lines)
        //{
        //    Dictionary<string, Employee> employeeDict = new Dictionary<string, Employee>();
        //    foreach (string line in lines)
        //    {
        //        string[] fields = line.Split(',');
        //        string payType = fields[3];
        //        Employee emp = null;
        //        if (payType.ToUpper().Equals("H"))
        //        {
        //            emp = new HourlyEmployee(fields);
        //        }
        //        else if (payType.ToUpper().Equals("S"))
        //        {
        //            emp = new SalaryEmployee(fields);
        //        }
        //        else
        //        {
        //            Console.WriteLine("ERROR: Unknown PayType for Employee Id: " + fields[0] + " Name: " +
        //                fields[2] + ", " + fields[1]);
        //        }
        //        if (emp != null)
        //        {
        //            employeeDict.Add(fields[0], emp);
        //        }
        //    }
        //    return employeeDict;
        //}

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


        public static void createPayChecks(Employee[] employeesSortedGrossDescArray)
        {
            string path = PayrollConstants.DEFAULT_PAYCHECKS_WRITE_PATH;
            if (File.Exists(path))
            {
                File.Delete(path);
            }
    
            try
            {
                foreach (Employee e in employeesSortedGrossDescArray)
                {
                    System.IO.File.AppendAllText(path,
                        string.Format("{0} {1} {2} {3} {4} {5} {6} {7}",
                            e.Id,
                            e.firstName,
                            e.lastName,
                            e.getPeriodGrossPay().ToString("F"),
                            e.getFederalTax().ToString("F"),
                            e.getStateTax().ToString("F"),
                            e.getNetPay().ToString("F"),
                            Environment.NewLine));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        //public static Employee[] sortDictionaryByGrossPayDesc(Dictionary<string, Employee> dictionary)
        //{
        //    Employee[] paychecksArray = new Employee[dictionary.Count];
        //    int index = 0;
        //    foreach (KeyValuePair<string, Employee> kvp in dictionary)
        //    {
        //        paychecksArray[index] = kvp.Value;
        //        index++;
        //    }
        //    Array.Sort(paychecksArray, delegate(Employee emp1, Employee emp2) {
        //        return emp2.getPeriodGrossPay().CompareTo(emp1.getPeriodGrossPay());
        //    });

        //    return paychecksArray;
        //}
    }
}
