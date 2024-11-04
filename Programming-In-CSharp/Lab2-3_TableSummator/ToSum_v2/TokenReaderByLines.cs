using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace hm_ToSum_v2
{
    public class TokenReaderByLines : ITokenReader
    {
        private readonly TextReader _reader;
        private string[]? _currentLine;
        private int _currentWordIndex;
        private char[] _tokenDelimiters;

        public TokenReaderByLines(TextReader reader, char[] tokenDelimiters)
        {
            _reader = reader;
            _currentLine = Array.Empty<string>();
            _currentWordIndex = -1;
            _tokenDelimiters = tokenDelimiters;
        }

        public Token ReadToken()
        {
            if(_currentWordIndex == -1)
            {
                var line = _reader.ReadLine();
                if (line == null)
                {
                    _currentLine = null;
                    _currentWordIndex = 0;
                    return new Token { Type = TokenType.EndOfInput, Word = null };
                }
                _currentLine = line.Split(_tokenDelimiters, StringSplitOptions.RemoveEmptyEntries);
                _currentWordIndex = 0;
            }
            if(_currentLine == null)
            {
                return new Token { Type = TokenType.EndOfInput, Word = null };
            }
            if (_currentWordIndex == _currentLine.Length)
            {
                var line = _reader.ReadLine();
                if(line == null)
                {
                    _currentLine = null;
                    _currentWordIndex = 0;
                    return new Token { Type = TokenType.EndOfLine, Word = null };
                }
                _currentLine = line.Split(_tokenDelimiters, StringSplitOptions.RemoveEmptyEntries);
                _currentWordIndex = 0;
                return new Token { Type = TokenType.EndOfLine, Word = null };
            }
            return new Token { Type = TokenType.Word, Word = _currentLine[_currentWordIndex++] };
        }
    }
}
