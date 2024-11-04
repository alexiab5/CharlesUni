using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace hm_ParagraphCounting
{
    // prints error messages according to the specified error type
    internal class ErrorHandler
    {
        public static void printErrorMessage(string exceptionType)
        {
            if (exceptionType == "cmd exception")
                System.Console.WriteLine("Argument Error");
            else if (exceptionType == "file exception")
                System.Console.WriteLine("File Error");
            else
                System.Console.WriteLine("Unknown Error");
        }
    }
}