using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteveJulienSNLtest.Models
{
    public class StateReportModel
    {
        public string name { get; set; }
        public double medianTimeWorked { get; set; }
        public double medianNetPay { get; set; }
        public double taxes { get; set; }
        public StateReportModel(EmployeePayrollEntry entry)
        {

        }
    }
}
