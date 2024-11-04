using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Immutable;

namespace hm_ParagraphCounting
{
    public class ParagraphCounter
    {
        private TextReader _reader;
        private TextWriter _writer;

        public ParagraphCounter(TextReader reader, TextWriter writer)
        {
            _reader = reader;
            _writer = writer;
        }

        public void WriteParagraphsWordCount()
        {
            if (_reader == null)
                return;
            string? currentLine;
            int currentWordCount = 0;
            while ((currentLine = _reader.ReadLine()) != null)
            {
                int wc = WordCounter.CountWordsString(currentLine);
                if (wc == 0)
                {                    
                    if (currentWordCount != 0)
                    {
                        _writer.WriteLine(currentWordCount);
                        currentWordCount = 0;
                    }
                }
                else
                {
                    currentWordCount += WordCounter.CountWordsString(currentLine);
                }
            }
            //processing the last paragraph (if it exists)
            if (currentWordCount != 0)
            {
                _writer.WriteLine(currentWordCount);
            }
        }
    }
}
