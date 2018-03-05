using SteveJulienSNLtest.Models;

namespace SteveJulienSNLtest
{
    public static class PayCheckReport
    {
        private static string[] paycheckStrings;
        private static string path = Constants.DEFAULT_PAYCHECKS_WRITE_PATH;

        public static void run(string delimiter, EmployeePayrollEntry[] sortedEntries)
        {
            paycheckStrings = new string[sortedEntries.Length];

            for (int i = 0; i < sortedEntries.Length; i++)
            {
                EmployeePayrollEntry e = sortedEntries[i];
                string text = e.Id;
                text += delimiter + " " + e.firstName;
                text += delimiter + " " + e.lastName;
                text += delimiter + " " + e.getPeriodGrossPay().ToString("F");
                text += delimiter + " " + e.getFederalTax().ToString("F");
                text += delimiter + " " + e.getStateTax().ToString("F");
                text += delimiter + " " + e.getNetPay().ToString("F");
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
