using System;

namespace SteveJulienSNLtest.Models
{
    public static class EmployeeFetcher
    {
        public static void run(PayPeriodDataFactory factory)
        {
            string input = "";
            while (!input.Equals("q"))
            {
                Console.Write("Enter employee id: ");

                string id = Console.ReadLine();
                EmployeePayrollEntry employee = factory.GetByEmployeeId(id);
                string emp = "No employee by that id.";
                if (employee != null)
                {
                    emp =
                    "\nEmployee Id:  " + employee.Id + "\n" +
                    "First Name:   " + employee.firstName + "\n" +
                    "Last Name:    " + employee.lastName + "\n" +
                    "Pay Type:     " + employee.payType + "\n" +
                    "Salary:       $" + employee.salary.ToString("F") + "\n" +
                    "Start Date:   " + employee.startDate.ToString("MM/dd/yyyy") + "\n" +
                    "State of Res: " + employee.residenceState.getName() + "\n" +
                    "Hours worked: " + employee.currentHours.ToString("F");
                }
                Console.WriteLine(emp);
                Console.Write("\nHit q to quit, or hit enter to search again ");
                input = Console.ReadLine();
                input = input.ToLower();
            }
        }
    }
}
