using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteveJulienSNLtest.Models
{
   
    public abstract class Employee
    {
        public string Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string payType { get; set; }
        public double salary { get; set; }
        public DateTime startDate { get; set; }
        public ResidenceState residenceState { get; set; }
        public double currentHours { get; set; }

        // TODO: Handle number Exceptions during construction
        public Employee(string[] values)
        {
            this.Id = values[0];
            this.firstName = values[1];
            this.lastName = values[2];
            this.payType = values[3];
            this.salary = Convert.ToDouble(values[4]);
            this.startDate = Convert.ToDateTime(values[5]);
            this.residenceState = new ResidenceState(values[6]);
            this.currentHours = Convert.ToDouble(values[7]);
        }
        abstract public double getPeriodGrossPay();
        public double getNetPay()
        {
            return getPeriodGrossPay() - ( getFederalTax() + getStateTax() );
        }
        public double getFederalTax()
        {
            return getPeriodGrossPay() * PayrollConstants.FEDERAL_TAX_RATE;
        }
        public double getStateTax()
        {
            return getPeriodGrossPay() * residenceState.getStateTaxRate();
        } 

        
    }


}
