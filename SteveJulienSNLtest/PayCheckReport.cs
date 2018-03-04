using SteveJulienSNLtest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteveJulienSNLtest
{
    public class PayCheckReport
    {
        private string[] paycheckStrings;
        private string path;

        public PayCheckReport(string userInputPath, PayrollEntry[] sortedPayrollEntries)
        {
            path = string.IsNullOrEmpty(userInputPath) ? Constants.DEFAULT_PAYCHECKS_WRITE_PATH : userInputPath;
            createPayrollLines(sortedPayrollEntries);
        }

        private void createPayrollLines(PayrollEntry[] sorted)
        {
            paycheckStrings = new string[sorted.Length];

            for (int i = 0; i < sorted.Length; i++)
            {
                PayrollEntry e = sorted[i];
                string text = e.Id;
                text += " " + e.firstName;
                text += " " + e.lastName;
                text += " " + e.getPeriodGrossPay().ToString("F");
                text += " " + e.getFederalTax().ToString("F");
                text += " " + e.getStateTax().ToString("F");
                text += " " + e.getNetPay().ToString("F");
                paycheckStrings[i] = text;
            }
        }
        public void writePaychecksToFile()
        {
            StringArrayToFileWriter.write(path, paycheckStrings);
        }
    }
}
