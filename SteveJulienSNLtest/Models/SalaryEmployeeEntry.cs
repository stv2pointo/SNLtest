namespace SteveJulienSNLtest.Models
{
    public class SalaryEmployeeEntry : EmployeePayrollEntry
    {
        public SalaryEmployeeEntry(string[] values) : base(values)
        {
        }
        public override double getPeriodGrossPay()
        {
            return salary / (Constants.WEEKS_PER_YEAR / Constants.WEEKS_PER_PAY_PERIOD);
        }
    }
}
