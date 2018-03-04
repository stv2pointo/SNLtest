using SteveJulienSNLtest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteveJulienSNLtest
{
    public class StateReport
    {
        private string name;
        private double medianTimeWorked;
        private double medianNetPay;
        private double taxes;

        public StateReport(List<PayrollEntry> entries)
        {
            setValues(entries);
        }
        private void setValues(List<PayrollEntry> entries)
        {
            if(entries != null && entries.Count > 0)
            {
                name = entries.First().residenceState.getName();
                List<double> times = new List<double>();
                List<double> pays = new List<double>();
                List<double> taxList = new List<double>();
                foreach (PayrollEntry entry in entries)
                {
                    times.Add(entry.currentHours);
                    pays.Add(entry.getNetPay());   
                    taxList.Add(entry.getStateTax());
                }
                medianTimeWorked = calculateMedianOfList(times);
                medianNetPay = calculateMedianOfList(pays);
                taxes = calculateSumOfList(taxList);
            }
            else
            {
                setEmptyValues();
            }
        }

        private void setEmptyValues()
        {
            name = "";
            medianTimeWorked = 0.0;
            medianNetPay = 0.0;
            taxes = 0.0;
        }

        private double calculateMedianOfList(List<double> list)
        {
            double median = 0.0;
            list.Sort();
            int count = list.Count;
            if (count > 0 && count % 2 == 0)
            {
                int indexLeftOfCenter = count / 2;
                
                median = list.ElementAt(indexLeftOfCenter -1 ) + list.ElementAt(indexLeftOfCenter);
                median /= 2;
            }
            else
            {
                int center = (count / 2);
                median = list.ElementAt(center);
            }
            return median;
        }

        private double calculateSumOfList(List<double> taxList)
        {
            double taxSum = 0.0;
            foreach(double t in taxList)
            {
                taxSum += t;
            }
            return taxSum;
        }

        public string getName()
        {
            return name;
        }

        public string getMedianTimeWorkedString()
        {
            string median;
            try
            {
                median = medianTimeWorked.ToString("F");
            }
            catch
            {
                median = "";
            }
            if (string.IsNullOrEmpty(getName()))
            {
                median = "";
            }
            return median;
        }
        public string getMedianNetPayString()
        {
            string median;
            try
            {
                median = medianNetPay.ToString("F");
            }
            catch
            {
                median = "";
            }
            if (string.IsNullOrEmpty(getName()))
            {
                median = "";
            }
            return median;
        }
        public string getTaxesString()
        {
            string tax;
            try
            {
                tax = taxes.ToString("F");
            }
            catch
            {
                tax = "";
            }
            if (string.IsNullOrEmpty(getName())){
                tax = "";
            }
            return tax;
        }

    }

   



}
