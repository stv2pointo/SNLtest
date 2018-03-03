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
        private double taxRate;
        public ResidenceState(string name)
        {
            setName(name);
            setTaxRate(name);
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public void setTaxRate(string name)
        {
            string[] five = new string[] { "UT", "WY", "NV" };
            string[] sixHalf = new string[] { "CO", "ID", "ID" , "OR"};
            string[] seven = new string[] { "WA", "NM", "TX" };
            string nameUpper = name.ToUpper();
            if (five.Contains(nameUpper))
            {
                taxRate = .05;
            }
            else if (sixHalf.Contains(nameUpper))
            {
                taxRate = .065;
            }
            else if (seven.Contains(nameUpper))
            {
                taxRate = .07;
            }
            //else
            //{
            //    throw new NoKnownResidenceStateException("Unknown state: " + name);
            //}
        }

        public string getName()
        {
            return name;
        }
        public double getTaxRate()
        {
            return taxRate;
        }
    }
    
}
