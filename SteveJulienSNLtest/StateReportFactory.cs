using SteveJulienSNLtest.Models;
using System.Collections.Generic;

namespace SteveJulienSNLtest
{
    public static class StateReportFactory
    {
        private static List<EmployeePayrollEntry>[] arrayOfLists;
        private static StateReport[] stateReports;
        private static string[] stateReportLines;
        private static string path = Constants.DEFAULT_STATE_REPORT_WRITE_PATH;
        private static int numStates;

        public static void run(string delimiter, EmployeePayrollEntry[] entries)
        {
            numStates = (int)States.STATE_COUNT;

            stateReports = new StateReport[numStates];

            sortPayrollEntriesIntoArrayOfListsByState(entries);

            createStateReportsFromArrayOfEntryLists();

            sortReports();

            formatStateReportLines(delimiter);

            write();
        }

        private static void sortPayrollEntriesIntoArrayOfListsByState(EmployeePayrollEntry[] entries)
        {
            arrayOfLists = Sorter.sortEntriesIntoArrayOfEntryListsByState(entries);
        }

        private static void createStateReportsFromArrayOfEntryLists()
        {
            for (int i = 0; i < arrayOfLists.Length; i++)
            {
                List<EmployeePayrollEntry> entryList = arrayOfLists[i];
                StateReport report = new StateReport(entryList);
                stateReports[i] = report;
            }
        }

        private static void sortReports()
        {
            stateReports = Sorter.sortStateReportByStateName(stateReports);
        }

        private static void formatStateReportLines(string delimiter)
        {
            int count = stateReports.Length;
            stateReportLines = new string[count];
            for (int i = 0; i < count; i++)
            {
                StateReport report = stateReports[i];
                stateReportLines[i] =
                    report.getName()                   + delimiter + " " +
                    report.getMedianTimeWorkedString() + delimiter + " " +
                    report.getMedianNetPayString()     + delimiter + " " +
                    report.getTaxesString();
            }
        }
        private static void write()
        {
            StringArrayToFileWriter.write(path, stateReportLines);
        }
    }
}
