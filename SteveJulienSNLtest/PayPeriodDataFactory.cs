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
        public static int ROWS_TO_COLLECT = 5;// int.MaxValue;
        private string[] rawLines;
        private PayrollEntry[] payrollEntries;
        private Dictionary<string, PayrollEntry> payrollDict;
        private string path;

        public PayPeriodDataFactory(string userInputPath)
        {
            string pathEnd = (userInputPath == null) ? @"..\..\Resources\Employees.txt" : userInputPath;
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),pathEnd); 
            initializeDataSetsForPayCheckReports();
        }

        public PayrollEntry[] getPayrollEntries()
        {
            return payrollEntries;
        }

        private void initializeDataSetsForPayCheckReports()
        {
            getLinesFromInputFile();
            createPayrollRecords();
        }
        private void getLinesFromInputFile()
        { 
            string line;
            int numLines = File.ReadLines(path).Count();
            // TODO: REMOVE COUNT STUFF AFTER TESTING
            numLines = (numLines > ROWS_TO_COLLECT) ? ROWS_TO_COLLECT : numLines;

            if (numLines > 0)
            {
                rawLines = new string[numLines];
                try
                {
                    int index = 0;
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

        public PayrollEntry GetByEmployeeId(string employeeId)
        {
            PayrollEntry entry = null;
            if (payrollDict != null && payrollDict.Count > 0)
            {
                try
                {
                    entry = payrollDict[employeeId];
                }
                catch(Exception e)
                {
                    Console.WriteLine("No employee by that id: " + e.Message);
                }
            }
            return entry;
        }
    }
}
