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

            Console.ReadLine();
        }

        public static PayrollEntry[] sortEmployeesByGross(PayrollEntry[] employees)
        {
            Array.Sort(employees, delegate (PayrollEntry emp1, PayrollEntry emp2)
            {
                return emp2.getPeriodGrossPay().CompareTo(emp1.getPeriodGrossPay());
            });

            return employees;
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

      

        public static string[] createPayrollLines(PayrollEntry[] sorted)
        {
            string[] paychecks = new string[sorted.Length];

            for(int i = 0; i < sorted.Length; i++)
            {
                PayrollEntry e = sorted[i];
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
