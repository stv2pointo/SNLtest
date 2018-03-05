using SteveJulienSNLtest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteveJulienSNLtest
{
    public static class PayCheckReport
    {
        private static string[] paycheckStrings;
        private static string path = Constants.DEFAULT_PAYCHECKS_WRITE_PATH;

        public static void run(EmployeePayrollEntry[] sortedEntries)
        {
            paycheckStrings = new string[sortedEntries.Length];

            for (int i = 0; i < sortedEntries.Length; i++)
            {
                EmployeePayrollEntry e = sortedEntries[i];
                string text = e.Id;
                text += " " + e.firstName;
                text += " " + e.lastName;
                text += " " + e.getPeriodGrossPay().ToString("F");
                text += " " + e.getFederalTax().ToString("F");
                text += " " + e.getStateTax().ToString("F");
                text += " " + e.getNetPay().ToString("F");
                paycheckStrings[i] = text;
            }

            writePaychecksToFile();
        }
        private static void writePaychecksToFile()
        {
            StringArrayToFileWriter.write(path, paycheckStrings);
        }
    }
}
