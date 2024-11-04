using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace hm_ToSum_v2
{
    public interface ITokenProcessor
    {
        public void ProcessAllTokens();
        public void WriteReport(TextWriter textWriter);

    }
}
