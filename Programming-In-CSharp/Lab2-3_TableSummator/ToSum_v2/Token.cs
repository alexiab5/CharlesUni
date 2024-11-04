using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace hm_ToSum_v2
{
    public enum TokenType { Word, EndOfInput, EndOfLine, EndOfParagraph };
    public class Token
    {
        public required TokenType Type { get; init; }
        public required string? Word { get; init; }
        
    }
}
