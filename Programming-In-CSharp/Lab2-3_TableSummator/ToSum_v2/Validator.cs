using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace hm_ToSum_v2
{
    internal class Validator
    {
        public static bool ValidateCommandLineArguments(string[] args)
        {
            if (args.Length < 3)
                return false;
            return true;
        }

        public static bool ValidateFile(string fileName, FileAccess accessMode)
        {
            try
            {
                using (var file = new FileStream(fileName, FileMode.Open, accessMode))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}