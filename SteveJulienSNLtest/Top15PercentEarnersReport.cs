using SteveJulienSNLtest.Models;
using System.Collections.Generic;

namespace SteveJulienSNLtest
{
    public static class Top15PercentEarnersReport
    {
        private static EmployeePayrollEntry[] top15percentEarners;
        private static string[] topEarnerStrings;
        private static string path = Constants.DEFAULT_TOP_EARNERS_WRITE_PATH;

        public static void run(string delimiter, EmployeePayrollEntry[] sortedEntries)
        {
            setTop15PercentEarners(sortedEntries);
            formatTopEarnersStrings(delimiter);
            writeTopEarnersReportToFile();
        }
    
        private static void setTop15PercentEarners(EmployeePayrollEntry[] sortedEntries)
        {
            int count = (int)(sortedEntries.Length * 0.15);
            top15percentEarners = new EmployeePayrollEntry[count];
            for(int i=0;i<count;i++)
            {
                top15percentEarners[i] = sortedEntries[i];
            }
        }

        private static void formatTopEarnersStrings(string delimiter)
        {    
            List<TopEarnerModel> topEarnersModels = 
                Sorter.getSortedListGrossLastNameFirstName(top15percentEarners);
            topEarnerStrings = new string[topEarnersModels.Count];
            int index = 0;
            foreach(TopEarnerModel m in topEarnersModels)
            {
                topEarnerStrings[index] = 
                    m.firstName         + delimiter + " " + 
                    m.lastName          + delimiter + " " + 
                    m.numYearsWorked    + delimiter + " " + 
                    m.grossPay.ToString("F");
                index++;
            }
        }

        private static void writeTopEarnersReportToFile()
        {
            StringArrayToFileWriter.write(path, topEarnerStrings);
        }    
         
    }
}
