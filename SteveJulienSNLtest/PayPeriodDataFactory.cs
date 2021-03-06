﻿using SteveJulienSNLtest.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SteveJulienSNLtest
{
    public class PayPeriodDataFactory
    {
        private string[] rawLines;
        private EmployeePayrollEntry[] payrollEntries;
        private Dictionary<string, EmployeePayrollEntry> payrollDict;
        private string path;

        public PayPeriodDataFactory(string path)
        {
            this.path = path;
            initializeDataSetsForPayCheckReports();
        }

        public EmployeePayrollEntry[] getPayrollEntries()
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
            int numLines = 0;
            try
            {
                numLines = File.ReadLines(path).Count();
            }
            catch
            {

            }

            if (numLines > 0)
            {
                rawLines = new string[numLines];

                int index = 0;
                StreamReader file =
                    new StreamReader(path);
                while ((line = file.ReadLine()) != null)
                {
                    rawLines[index] = line;
                    index++;
                }

                file.Close();


            }

        }

        private void createPayrollRecords()
        {
            try
            {
                payrollEntries = new EmployeePayrollEntry[rawLines.Length];

                for (int i = 0; i < rawLines.Length; i++)
                {
                    string[] fields = rawLines[i].Split(',');
                    string payType = fields[3];
                    EmployeePayrollEntry emp = null;
                    if (payType.ToUpper().Equals("H"))
                    {
                        emp = new HourlyEmployeeEntry(fields);
                    }
                    else if (payType.ToUpper().Equals("S"))
                    {
                        emp = new SalaryEmployeeEntry(fields);
                    }
                    else
                    {
                        Console.WriteLine("ERROR: Unknown PayType for Employee Id: " + fields[0] + " Name: " +
                            fields[2] + ", " + fields[1]);
                    }
                    payrollEntries[i] = emp;
                }
            }
            catch
            {
                payrollEntries = new EmployeePayrollEntry[1];
                payrollEntries[0] = new HourlyEmployeeEntry(new string[] { "NoValues", "NoValues", "NoValues", "H", "0.0", "1/1/2000", "NO", "0.0" });
            }
        }

        public void writeDictionary()
        {
            payrollDict = new Dictionary<string, EmployeePayrollEntry>();
            for (int i = 0; i < payrollEntries.Length; i++)
            {
                EmployeePayrollEntry payrollEntry = payrollEntries[i];
                payrollDict.Add(payrollEntry.Id, payrollEntry);
            }
        }

        public EmployeePayrollEntry GetByEmployeeId(string employeeId)
        {
            EmployeePayrollEntry entry = null;
            if (payrollDict != null && payrollDict.Count > 0)
            {
                try
                {
                    entry = payrollDict[employeeId];
                }
                catch (Exception e)
                {
                    //Console.WriteLine("No employee found by that id: " + e.Message);
                }
            }
            return entry;
        }
    }
}
