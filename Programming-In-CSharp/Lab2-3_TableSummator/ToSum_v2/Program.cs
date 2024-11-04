using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace hm_ToSum_v2
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

            string inputFileName = args[0];
            if (!Validator.ValidateFile(inputFileName, FileAccess.Read))
            {
                ErrorHandler.printErrorMessage("file credentials exception");
                return;
            }

            string outputFileName = args[1];
            string columnName = args[2];

            using (var reader = new StreamReader(inputFileName))
            {
                char[] delimiters = { ' ', '\t', '\n' };
                ITokenReader tokenReader = new TokenReaderByLines(reader, delimiters);
                ITokenProcessor tokenProcessor = new TableSummator(tokenReader, columnName);
                try
                {
                    tokenProcessor.ProcessAllTokens();
                }
                catch (Exception ex)
                {
                    ErrorHandler.printErrorMessage(ex.Message);
                    return;
                }
                try
                {
                    using (StreamWriter writer = new StreamWriter(outputFileName))
                    {
                        tokenProcessor.WriteReport(writer);
                    }
                }
                catch (Exception ex)
                {
                    ErrorHandler.printErrorMessage("file credentials exception"); //!!
                    return;
                }
            }
        }
    }
}
   