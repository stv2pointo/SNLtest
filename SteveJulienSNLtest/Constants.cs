﻿using System.IO;
using System.Reflection;

namespace SteveJulienSNLtest.Models
{
    public static class Constants
    {
        public static string DEFAULT_INPUT_PATH =
            Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Resources\Employees.txt");
        public static string DEFAULT_STATE_REPORT_WRITE_PATH = @"C:\SteveJulienTest\StateReports.txt";
        public static string DEFAULT_PAYCHECKS_WRITE_PATH = @"C:\SteveJulienTest\Paychecks.txt";
        public static string DEFAULT_TOP_EARNERS_WRITE_PATH = @"C:\SteveJulienTest\TopEarners.txt";
        public static double WEEKS_PER_YEAR = 52.0;
        public static double WEEKS_PER_PAY_PERIOD = 2.0;
        public static double FEDERAL_TAX_RATE = 0.15;
        public static double OVER_80_FACTOR = 1.5;
        public static double OVER_90_FACTOR = 1.75;
    }

    // States enum is used to sort state report lists into the right array index
    public enum States
    {
        UT = 0,
        WY = 1,
        NV = 2,

        CO = 3,
        ID = 4,
        AZ = 5,
        OR = 6,

        WA = 7,
        NM = 8,
        TX = 9,
        STATE_COUNT = 10
    }

}
