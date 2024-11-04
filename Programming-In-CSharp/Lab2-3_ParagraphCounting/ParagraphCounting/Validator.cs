using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace hm_ParagraphCounting
{
    internal class Validator
    {
        public static bool ValidateCommandLineArguments(string[] args)
        {
            if (args.Length != 1)
                return false;
            return true;
        }

        public static bool ValidateFile(string fileName)
        {
            try 
            {
                using (var file = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    return true;
                }
            }
            catch
            {
                return false; // the file does not exist or does not have reading permissions
            }
        }

    }
}