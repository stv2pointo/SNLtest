using SteveJulienSNLtest.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteveJulienSNLtest
{
    public static class InputFileValidator
    {
        public static string getInputPath()
        {
            if (isDefaultInputPrompt())
            {
                return Constants.DEFAULT_INPUT_PATH;
            }

            int tries = 0;
            string input = promptUser("Enter path of input file: ");

            while (!isValidFilename(input) || !isFileOfEmployeeRecords(input))
            {
                tries++;
                input = promptUser("Enter path of input file: ");
                if (tries == 3)
                {
                    Console.WriteLine("Failed to set path, proceeding with default file (Employees.txt).\n");
                    input = @"..\..\Resources\Employees.txt";
                    break;
                }
            }
            return input;
        }

        private static string promptUser(string prompt)
        {
            string input = null;
            while (string.IsNullOrEmpty(input))
            {
                Console.Write(prompt);
                input = Console.ReadLine();
            }
            return input;
        }

        private static bool isValidFilename(string path)
        {
            bool isValid = false;
            if (path.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {

                isValid = true;
            }
            else
            {
                Console.Write("Invalid file name.\nEnter path of input file: ");
            }
            return isValid;
        }

        private static bool isFileOfEmployeeRecords(string path)
        {
            try
            {
                string line1 = File.ReadLines(path).First();
                string[] fields = line1.Split(',');
                EmployeePayrollEntry testEntry = new HourlyEmployee(fields);
            }
            catch
            {
                Console.WriteLine("File entered is not in the valid payroll format.");
                Console.WriteLine("File must have: Id, firstName, lastName, payType, salary, startDate, residenceState, hoursWorked.");
                Console.WriteLine("Example:\n1,JANE,DOE,H,29.77,9/5/05,NM,70");
                return false;
            }
            return true;
        }
        private static bool isDefaultInputPrompt()
        {
            string input = "";
            while (!input.Equals("y") && !input.Equals("n"))
            {
                Console.Write("Use default input file? (Employees.txt) Enter y/n ");
                input = Console.ReadLine();
                input = input.ToLower();
            }
            return (input.Equals("y")) ? true : false;
        }
    }
}
