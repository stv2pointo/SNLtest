using SteveJulienSNLtest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteveJulienSNLtest
{
    public class Top15PercentEarnersReport
    {
        private PayrollEntry[] top15percentEarners;
        private string[] topEarnerStrings;
        private string path;

        public Top15PercentEarnersReport(string userInputPath, PayrollEntry[] sortedEntries)
        {
            path = string.IsNullOrEmpty(userInputPath) ? Constants.DEFAULT_TOP_EARNERS_WRITE_PATH : userInputPath;
            setTop15PercentEarners(sortedEntries);
            setTopEarnersStrings();
        }

        private void setTop15PercentEarners(PayrollEntry[] sortedEntries)
        {
            int count = (int)(sortedEntries.Length * 0.15);
            top15percentEarners = new PayrollEntry[count];
            for(int i=0;i<count;i++)
            {
                top15percentEarners[i] = sortedEntries[i];
            }

        }

        private void setTopEarnersStrings()
        {
            Sorter sorter = new Sorter();
            List<TopEarnerModel> topEarnersModels = 
                sorter.getSortedListGrossLastNameFirstName(top15percentEarners);
            topEarnerStrings = new string[topEarnersModels.Count];
            int index = 0;
            foreach(TopEarnerModel m in topEarnersModels)
            {
                topEarnerStrings[index] = m.firstName + " " + m.lastName + " " + m.numYearsWorked + " " + m.grossPay.ToString("F");
                index++;
            }
        }

        public void writeTopEarnersReportToFile()
        {
            StringArrayToFileWriter.write(path, topEarnerStrings);
        }
        
    }

}
