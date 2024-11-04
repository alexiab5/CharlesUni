using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace hm_ParagraphCounting
{
    public class WordCounter
    {
        private TextReader _reader;
        private TextWriter _writer;

        public WordCounter(TextReader reader, TextWriter writer)
        {
            _reader = reader;
            _writer = writer;
        }

        public static int CountWordsString(string inputString)
        {
            char[] delimiters = { ' ', '\t', '\n' };
            string[] arr = inputString.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            return arr.Length;
        }

        public int CountWords()
        {
            if(_reader == null) 
                return 0;
            string? currentLine;
            int wordCount = 0;
            while ((currentLine = _reader.ReadLine()) != null)
            {
                wordCount += CountWordsString(currentLine);
            }
            return wordCount;
        }

        public void PrintWordCount()
        {
            _writer.WriteLine(CountWords());
        }
    }
}