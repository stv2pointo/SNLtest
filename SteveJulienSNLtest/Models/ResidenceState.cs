using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteveJulienSNLtest.Models
{
    public class ResidenceState
    {
        private string name;
        private double stateTaxRate;
        public ResidenceState(string name)
        {
            setName(name);
            setStateTaxRate(name);
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public void setStateTaxRate(string name)
        {
            string[] five = new string[] { "UT", "WY", "NV" };
            string[] sixHalf = new string[] { "CO", "ID", "AZ" , "OR"};
            string[] seven = new string[] { "WA", "NM", "TX" };
            string nameUpper = name.ToUpper();
            if (five.Contains(nameUpper))
            {
                stateTaxRate = .05;
            }
            else if (sixHalf.Contains(nameUpper))
            {
                stateTaxRate = .065;
            }
            else if (seven.Contains(nameUpper))
            {
                stateTaxRate = .07;
            }
            else
            {
                Console.WriteLine("ERROR: No tax rate is set for " + name);
            }
        }

        public string getName()
        {
            return name;
        }
        public double getStateTaxRate()
        {
            return stateTaxRate;
        }
    }
    
}
