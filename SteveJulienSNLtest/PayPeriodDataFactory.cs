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
    public class PayPeriodDataFactory
    {
        public static int ROWS_TO_COLLECT = 5;
        private string[] rawLines;
        private PayrollEntry[] payrollEntries;
        private PayrollEntry[] sortedGross;
        private Dictionary<string, PayrollEntry> payrollDict;
        private string path;

        public PayPeriodDataFactory(string userInputPath)
        {
            string pathEnd = (userInputPath == null) ? @"..\..\Resources\Employees.txt" : userInputPath;
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),pathEnd); 
            run();
        }

        public PayrollEntry[] getPayrollEntries()
        {
            return payrollEntries;
        }

        public PayrollEntry[] getSortedGross()
        {
            return sortedGross;
        }
        private void run()
        {
            getLinesFromInputFile();
            createPayrollRecords();
        }
        private void getLinesFromInputFile()
        {
            int index = 0;
            string line;
            int numLines = File.ReadLines(path).Count();
            if (numLines > 0)
            {
                rawLines = new string[numLines];
                try
                {
                    System.IO.StreamReader file =
                        new System.IO.StreamReader(path);
                    while ((line = file.ReadLine()) != null && index < ROWS_TO_COLLECT)
                    {
                        rawLines[index] = line;
                        index++;
                    }

                    file.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            
        }

        private void createPayrollRecords()
        {
            payrollEntries = new PayrollEntry[rawLines.Length];

            for (int i=0;i<rawLines.Length;i++)
            {
                string[] fields = rawLines[i].Split(',');
                string payType = fields[3];
                PayrollEntry emp = null;
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

                payrollEntries[i] = emp;

            }
        }

        public void writeDictionary()
        {
            payrollDict = new Dictionary<string, PayrollEntry>();
            for(int i = 0; i < payrollEntries.Length; i++)
            {
                PayrollEntry payrollEntry = payrollEntries[i];
                payrollDict.Add(payrollEntry.Id, payrollEntry);
            }
        }
    }
}
