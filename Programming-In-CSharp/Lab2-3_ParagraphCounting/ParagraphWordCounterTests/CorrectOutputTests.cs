using hm_ParagraphCounting;

namespace ParagraphWordCounterTests
{
    public class CorrectOutputTests
    {
        [Fact]
        public void EmptyFile()
        {
            //Arrange
            string fileContents = "";
            var reader = new StringReader(fileContents);
            var writer = new StringWriter();
            ParagraphCounter paragraphCounter = new ParagraphCounter(reader, writer);
            //Act
            paragraphCounter.WriteParagraphsWordCount();
            //Assert
            Assert.Equal(string.Empty, writer.ToString());
        }

        [Fact]
        public void SingleParagraph()
        {
            //Arrange
            string fileContents = """
                If a train station is where the train stops, what is a work station?
                """;
            var reader = new StringReader(fileContents);
            var writer = new StringWriter();
            ParagraphCounter paragraphCounter = new ParagraphCounter(reader, writer);
            //Act
            paragraphCounter.WriteParagraphsWordCount();
            //Assert
            Assert.Equal("14" + writer.NewLine, writer.ToString());
        }

        [Fact]
        public void WhitespacesOnly()
        {
            //Arrange
            // tabs, spaces and empty lines
            string fileContents = """
                                


                                                      
                                                
                            
                """;
            var reader = new StringReader(fileContents);
            var writer = new StringWriter();
            ParagraphCounter paragraphCounter = new ParagraphCounter(reader, writer);
            //Act
            paragraphCounter.WriteParagraphsWordCount();
            //Assert
            Assert.Equal(string.Empty, writer.ToString());
        }

        [Fact]
        public void SingleLineParagraphsSeparatedByEmptyLines()
        {
            //Arrange
            string fileContents = """
                If a train station is where the train stops, what is a work station?

                A work station? Yes!



                third paragraph.
                """;
            var reader = new StringReader(fileContents);
            var writer = new StringWriter();
            ParagraphCounter paragraphCounter = new ParagraphCounter(reader, writer);
            //Act
            paragraphCounter.WriteParagraphsWordCount();
            //Assert
            Assert.Equal("14" + writer.NewLine + "4" + writer.NewLine + "2" + writer.NewLine, writer.ToString());
        }

        [Fact]
        public void MultipleLineParagraphsSeparatedByEmptyLines()
        {
            //Arrange
            string fileContents = """
                If a train station is where the train stops, what is a work station?

                A work station? 
                Yes!



                third paragraph.
                still the third paragraph!!
                """;
            var reader = new StringReader(fileContents);
            var writer = new StringWriter();
            ParagraphCounter paragraphCounter = new ParagraphCounter(reader, writer);
            //Act
            paragraphCounter.WriteParagraphsWordCount();
            //Assert
            Assert.Equal("14" + writer.NewLine + "4" + writer.NewLine + "6" + writer.NewLine, writer.ToString());
        }

        [Fact]
        public void WhiteSpacesBetweenParagraphsAndEmptyLines()
        {
            //Arrange
            string fileContents = """



                If a train station is where the train stops, what is a work station?
                                    
                                  
                A work station? 
                Yes!                                 
                """;
            var reader = new StringReader(fileContents);
            var writer = new StringWriter();
            ParagraphCounter paragraphCounter = new ParagraphCounter(reader, writer);
            //Act
            paragraphCounter.WriteParagraphsWordCount();
            //Assert
            Assert.Equal("14" + writer.NewLine + "4" + writer.NewLine, writer.ToString());
        }

        [Fact]
        public void EmptyLinesAtTheEnd()
        {
            //Arrange
            string fileContents = """
                If a train station is where the train stops, what is a work station?
                                    
                                  
                A work station? 
                Yes!
                                

                                                             
                third paragraph.
                still the third paragraph!!



                """;
            var reader = new StringReader(fileContents);
            var writer = new StringWriter();
            ParagraphCounter paragraphCounter = new ParagraphCounter(reader, writer);
            //Act
            paragraphCounter.WriteParagraphsWordCount();
            //Assert
            Assert.Equal("14" + writer.NewLine + "4" + writer.NewLine + "6" + writer.NewLine, writer.ToString());
        }

        [Fact]
        public void EmptyLinesAtTheBegining()
        {
            //Arrange
            string fileContents = """
                If a train station is where the train stops, what is a work station?

                A work station? Yes!



                third paragraph.
                """;
            var reader = new StringReader(fileContents);
            var writer = new StringWriter();
            ParagraphCounter paragraphCounter = new ParagraphCounter(reader, writer);
            //Act
            paragraphCounter.WriteParagraphsWordCount();
            //Assert
            Assert.Equal("14" + writer.NewLine + "4" + writer.NewLine + "2" + writer.NewLine, writer.ToString());
        }

        [Fact]
        public void WordsSeparatedByMixedWhitespaces()
        {
            //Arrange
            string fileContents = """
                If a train          station is           where the train stops, what is a       work station?

                A work station? Yes!



                third paragraph.
                            kjbkwfcbq?              nksrwrw
                            qbfkq
                """;
            var reader = new StringReader(fileContents);
            var writer = new StringWriter();
            ParagraphCounter paragraphCounter = new ParagraphCounter(reader, writer);
            //Act
            paragraphCounter.WriteParagraphsWordCount();
            //Assert
            Assert.Equal("14" + writer.NewLine + "4" + writer.NewLine + "5" + writer.NewLine, writer.ToString());
        }

        [Fact]
        public void ComplexParagraphs()
        {
            // all of the above combined: paragraphs on multiple lines, empty lines, only whitespace lines, empty lines at the begining etc.
            //Arrange
            string fileContents = """


                If a train          station is           where the train stops, what is a       work station?

                A work station? Yes!
                llllll  lnnnn aaaaaa  nkkk          


                third paragraph.
                            kjbkwfcbq?              nksrwrw
                            qbfkq


                bbbb
                                                        

                aaa
                bbb??


                            
                """;
            var reader = new StringReader(fileContents);
            var writer = new StringWriter();
            ParagraphCounter paragraphCounter = new ParagraphCounter(reader, writer);
            //Act
            paragraphCounter.WriteParagraphsWordCount();
            //Assert
            Assert.Equal("14" + writer.NewLine + "8" + writer.NewLine + "5" + writer.NewLine + "1" + writer.NewLine + "2" + writer.NewLine, writer.ToString());
        }
    }
}
