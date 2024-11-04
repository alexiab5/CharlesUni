using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace hm_ToSum_v2
{
    // prints error messages according to the specified error type
    internal class ErrorHandler
    {
        public static void printErrorMessage(string exceptionType)
        {
            if (exceptionType == "cmd exception")
                System.Console.WriteLine("Argument Error");
            else if (exceptionType == "file credentials exception")
                System.Console.WriteLine("File Error");
            else if (exceptionType == "file format exception")
                System.Console.WriteLine("Invalid File Format");
            else if (exceptionType == "integer conversion exception")
                System.Console.WriteLine("Invalid Integer Value");
            else if (exceptionType == "column name unfound")
                System.Console.WriteLine("Non-existent Column Name");
            else
                System.Console.WriteLine("Unknown Error");
        }
    }
}