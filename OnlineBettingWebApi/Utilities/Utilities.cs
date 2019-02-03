using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBettingWebApi.Utilities
{

    public class Util
    {
        public static readonly Dictionary<string, string> oddsTypeMap = new Dictionary<string, string>
        {
            {"1", "1"},
            {"X", "2"},
            {"2", "3"},
            {"X1", "4"},
            {"X2", "5"},
            {"12", "6"}
        };

        public static String DEBIT = "DEBIT";
        public static String CREDIT = "CREDIT";
    }
}
