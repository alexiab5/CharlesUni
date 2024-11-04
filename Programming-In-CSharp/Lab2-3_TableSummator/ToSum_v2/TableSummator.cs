using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;

namespace hm_ToSum_v2
{
    public class TableSummator : ITokenProcessor
    {
        private int _totalSum;
        private int _columnIndex;
        private string _columnName;
        private int _noColumns;
        private ITokenReader _tokenReader;

        public TableSummator(ITokenReader tokenReader, string columnName)
        {
            _tokenReader = tokenReader;
            _columnName = columnName;
            _totalSum = 0;
            _columnIndex = 0;
            _noColumns = 0;
        }

        private void ProcessTableHeader()
        {
            Token currentToken;
            int wordIndex = 0;
            while ((currentToken = _tokenReader.ReadToken()).Type != TokenType.EndOfInput)
            {
                if(currentToken.Type == TokenType.Word)
                {
                    wordIndex++;
                }
                if (currentToken.Type == TokenType.EndOfLine)
                {
                    if (wordIndex == 0)
                    {
                        throw new Exception("file format exception");
                    }
                    if (_columnIndex == 0)
                    {
                        throw new Exception("column name unfound");
                    }
                    _noColumns = wordIndex;
                    break;
                }
                else if (currentToken.Word != null && currentToken.Word.Equals(_columnName) && _columnIndex == 0)
                {
                    _columnIndex = wordIndex;
                }
            }
            if (_noColumns == 0) // empty file
            {
                throw new Exception("file format exception");
            }
        }

        public void ProcessAllTokens()
        {
            ProcessTableHeader();
            Token token;
            int currentColumnIndex = 0;
            while ((token = _tokenReader.ReadToken()).Type != TokenType.EndOfInput)
            {
                switch (token.Type)
                {
                    case TokenType.Word:
                        currentColumnIndex++;
                        if (currentColumnIndex == _columnIndex)
                        {
                            ProcessToken(token);
                        }
                        break;
                    case TokenType.EndOfLine:
                        ProcessEndOfLine(currentColumnIndex);
                        currentColumnIndex = 0;
                        break;
                }
            }
        }

        private void ProcessToken(Token token)
        {
            int value;
            try
            {
                if (token.Word == null)
                    throw new Exception();
                value = int.Parse(token.Word);
            }
            catch
            {
                throw new Exception("integer conversion exception");
            }
            AddToSum(value);
        }

        private void AddToSum(int value)
        {
            _totalSum += value;
        }

        private void ProcessEndOfLine(int currentColumnsCount)
        {
            if (currentColumnsCount != _noColumns)
                throw new Exception("file format exception");
        }

        /* Writes the report in the format:
        * name
        * ----
        * value
        * Throws an Exception if the file does not have access rights.
       */
        public void WriteReport(TextWriter textWriter)
        {
            textWriter.WriteLine(_columnName);
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < _columnName.Length; i++)
            {
                s.Append("-");
            }
            textWriter.WriteLine(s.ToString());
            textWriter.Write(_totalSum);
        }
    }
}
