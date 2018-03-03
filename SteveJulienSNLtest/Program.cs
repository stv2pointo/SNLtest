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
            string[] rawLines = readFromFile();
        
            Dictionary<string, Employee> dictionary = getEmployeeDict(rawLines);
            createPayChecks(dictionary);
            //for(int i=5;i<8;i++)
            //{
            //    Employee emp = dictionary[i.ToString()];
            //    string firstEmployeeName = emp.firstName + " " + emp.lastName;
            //    string net = emp.getNetPay().ToString();
            //    string gross = emp.getPeriodGrossPay().ToString();
            //    string fed = emp.getFederalTax().ToString();
            //    string stat = emp.getStateTax().ToString();
            //    string hours = emp.currentHours.ToString();
            //    Console.WriteLine(firstEmployeeName + " hours: " + hours + " net: " + net + " gross: " + gross + " fed: " + fed + " stat: " + stat);
            //}
            
            //writeToFile(readFromFile());
       
            //Console.ReadLine();
        }

        public static string[] readFromFile()
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
                while ((line = file.ReadLine()) != null && counter < 5)
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
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static Dictionary<string, Employee> getEmployeeDict(string[] lines)
        {
            Dictionary<string, Employee> employeeDict = new Dictionary<string, Employee>();
            foreach(string line in lines)
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


        public static void createPayChecks(Dictionary<string, Employee> dictionary)
        {
            string path = PayrollConstants.DEFAULT_PAYCHECKS_WRITE_PATH;
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            string[] payChecks = new string[dictionary.Count];
            try
            {
                foreach (KeyValuePair<string, Employee> kvp in dictionary)
                {

                    System.IO.File.AppendAllText(path, 
                        string.Format("{0} {1} {2} {3} {4} {5} {6} {7}",
                            kvp.Key,
                            kvp.Value.firstName,
                            kvp.Value.lastName,
                            kvp.Value.getPeriodGrossPay().ToString("F"),
                            kvp.Value.getFederalTax().ToString("F"),
                            kvp.Value.getStateTax().ToString("F"),
                            kvp.Value.getNetPay().ToString("F"),
                            Environment.NewLine));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //public static void sortDictionaryByGrossPayDesc(Dictionary<string, Employee> dictionary)
        //{
        //    List<KeyValuePair<string, Employee>> myList = dictionary.ToList();

        //    myList.Sort(
        //        delegate (KeyValuePair<string, Employee> pair1,
        //        KeyValuePair<string, Employee> pair2)
        //        {
        //            return pair1.Value.getPeriodGrossPay().CompareTo(pair2.Value.getPeriodGrossPay());
        //        }
        //    );
        //}
    }
}
