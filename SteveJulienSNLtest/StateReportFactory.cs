using SteveJulienSNLtest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteveJulienSNLtest
{
    public class StateReportFactory
    {
        private List<PayrollEntry>[] arrayOfLists;
        private StateReport[] stateReports;
        private string[] stateReportLines;
        private string path;
        //private int numStatesInEntries;
        private int numStates;
        public StateReportFactory(string userInputPath, PayrollEntry[] entries)
        {
            path = string.IsNullOrEmpty(userInputPath) ? Constants.DEFAULT_STATE_REPORT_WRITE_PATH : userInputPath;
            run(entries);
        }

        private void run(PayrollEntry[] entries)
        {
            numStates = (int)States.STATE_COUNT;

            stateReports = new StateReport[numStates];

            sortPayrollEntriesIntoArrayOfListsByState(entries);

            createStateReportsFromArrayOfEntryLists();

            sortReports();

            createStateReportLines();

            write();
        }

        private void sortPayrollEntriesIntoArrayOfListsByState(PayrollEntry[] entries)
        {
            arrayOfLists = Sorter.sortEntriesIntoArrayOfEntryListsByState(entries);
        }

        private void createStateReportsFromArrayOfEntryLists()
        {
            for (int i = 0; i < arrayOfLists.Length; i++)
            {
                List<PayrollEntry> entryList = arrayOfLists[i];
                StateReport report = new StateReport(entryList);
                stateReports[i] = report;
            }
        }

        private void sortReports()
        {
            stateReports = Sorter.sortStateReportByStateName(stateReports);
        }

        private void createStateReportLines()
        {
            int count = stateReports.Length;
            stateReportLines = new string[count];
            for (int i = 0; i < count; i++)
            {
                StateReport report = stateReports[i];
                stateReportLines[i] =
                    report.getName() + " " +
                    report.getMedianTimeWorkedString() + " " +
                    report.getMedianNetPayString() + " " +
                    report.getTaxesString();
            }
        }

        private void write()
        {
            StringArrayToFileWriter.write(path, stateReportLines);
        }
    }
}
