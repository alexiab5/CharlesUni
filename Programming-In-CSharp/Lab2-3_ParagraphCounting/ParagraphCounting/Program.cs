using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace hm_ParagraphCounting
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (!Validator.ValidateCommandLineArguments(args))
            {
                ErrorHandler.printErrorMessage("cmd exception");
                return;
            }

            string fileName = args[0];
            if (!Validator.ValidateFile(fileName))
            {
                ErrorHandler.printErrorMessage("file exception");
                return;
            }

            TextReader reader = new StreamReader(fileName);
            TextWriter writer = new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true };

            ParagraphCounter paragraphCounter = new ParagraphCounter(reader, writer);
            paragraphCounter.WriteParagraphsWordCount();
        }
    }
}