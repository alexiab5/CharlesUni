using hm_ToSum_v2;
using Newtonsoft.Json.Linq;

namespace ToSumTests
{
    public class FileFormatTests
    {
        [Fact]
        public void EmptyFile()
        {
            //Arrange
            string fileContents = "";
            var reader = new StringReader(fileContents);
            var writer = new StringWriter();
            char[] delimiters = { ' ', '\t', '\n' };
            var tokenReader = new TokenReaderByLines(reader, delimiters);
            var tokenProcessor = new TableSummator(tokenReader, "cena");
            //Act
            try
            {
                tokenProcessor.ProcessAllTokens();
            }
            //Assert
            catch (Exception ex)
            {
                Assert.Equal("file format exception", ex.Message);
            }
        }

        [Fact]
        public void LessColumnsThanHeader()
        {
            //Arrange
            string fileContents = """
                mesic   zbozi       typ         prodejce    mnozstvi    cena    trzba
                leden   brambory    tuzemske    Bartak      10895       12      130740
                leden   brambory    vlastni     15478       10      154780
                leden   jablka      dovoz       Adamec      1321        30      39630
                """;
            var reader = new StringReader(fileContents);
            var writer = new StringWriter();
            char[] delimiters = { ' ', '\t', '\n' };
            var tokenReader = new TokenReaderByLines(reader, delimiters);
            var tokenProcessor = new TableSummator(tokenReader, "cena");
            //Act
            try
            {
                tokenProcessor.ProcessAllTokens();
            }
            catch (Exception ex)
            {
                Assert.Equal("file format exception", ex.Message);
            }
        }

        [Fact]
        public void MoreColumnsThanHeader()
        {
            //Arrange
            string fileContents = """
                mesic   zbozi       typ         prodejce    mnozstvi    cena    trzba
                leden   brambory    tuzemske    Bartak      10895       12      130740
                leden   brambory    vlastni     15478       sf          10      154780
                leden   jablka      dovoz       Adamec      1321        30      39630  294823
                """;
            var reader = new StringReader(fileContents);
            var writer = new StringWriter();
            char[] delimiters = { ' ', '\t', '\n' };
            var tokenReader = new TokenReaderByLines(reader, delimiters);
            var tokenProcessor = new TableSummator(tokenReader, "cena");
            //Act
            try
            {
                tokenProcessor.ProcessAllTokens();
            }
            //Assert
            catch (Exception ex)
            {
                Assert.Equal("file format exception", ex.Message);
            }
        }


        [Fact]
        public void EmptyLines()
        {
            //Arrange
            string fileContents = """
                mesic   zbozi       typ         prodejce    mnozstvi    cena    trzba
                leden   brambory    tuzemske    Bartak      10895       12      130740


                leden   brambory    vlastni     15478       sf          10      154780

                leden   jablka      dovoz       Adamec      1321        30      39630
                

                """;
            var reader = new StringReader(fileContents);
            var writer = new StringWriter();
            char[] delimiters = { ' ', '\t', '\n' };
            var tokenReader = new TokenReaderByLines(reader, delimiters);
            var tokenProcessor = new TableSummator(tokenReader, "cena");
            //Act
            try
            {
                tokenProcessor.ProcessAllTokens();
            }
            //Assert
            catch (Exception ex)
            {
                Assert.Equal("file format exception", ex.Message);
            }
        }

        [Fact]
        public void OnlyWhitespaceLines()
        {
            //Arrange
            string fileContents = """
                mesic   zbozi       typ         prodejce    mnozstvi    cena    trzba
                leden   brambory    tuzemske    Bartak      10895       12      130740
                                        
                                                                    
                leden   brambory    vlastni     15478       sf          10      154780
                leden   jablka      dovoz       Adamec      1321        30      39630
                                                                    
                """;
            var reader = new StringReader(fileContents);
            var writer = new StringWriter();
            char[] delimiters = { ' ', '\t', '\n' };
            var tokenReader = new TokenReaderByLines(reader, delimiters);
            var tokenProcessor = new TableSummator(tokenReader, "cena");
            //Act
            try
            {
                tokenProcessor.ProcessAllTokens();
            }
            //Assert
            catch (Exception ex)
            {
                Assert.Equal("file format exception", ex.Message);
            }
        }

        [Fact]
        public void EmptyLinesAtTheEnd()
        {
            //Arrange
            string fileContents = """
                mesic   zbozi       typ         prodejce    mnozstvi    cena    trzba
                leden   brambory    tuzemske    Bartak      10895       12      130740                                                
                leden   brambory    vlastni     15478       sf          10      154780
                leden   jablka      dovoz       Adamec      1321        30      39630
                
                

                """;
            var reader = new StringReader(fileContents);
            var writer = new StringWriter();
            char[] delimiters = { ' ', '\t', '\n' };
            var tokenReader = new TokenReaderByLines(reader, delimiters);
            var tokenProcessor = new TableSummator(tokenReader, "cena");
            //Act
            try
            {
                tokenProcessor.ProcessAllTokens();
            }
            //Assert
            catch (Exception ex)
            {
                Assert.Equal("file format exception", ex.Message);
            }
        }

        [Fact]
        public void NonExistentColumnName()
        {
            //Arrange
            string fileContents = """
                mesic   zbozi       typ         prodejce    mnozstvi    cena    trzba
                leden   brambory    tuzemske    Bartak      10895       12      130740                                                
                leden   brambory    vlastni     15478       sf          10      154780
                leden   jablka      dovoz       Adamec      1321        30      39630
                """;
            var reader = new StringReader(fileContents);
            var writer = new StringWriter();
            char[] delimiters = { ' ', '\t', '\n' };
            var tokenReader = new TokenReaderByLines(reader, delimiters);
            var tokenProcessor = new TableSummator(tokenReader, "xd");
            //Act
            try
            {
                tokenProcessor.ProcessAllTokens();
            }
            //Assert
            catch (Exception ex)
            {
                Assert.Equal("column name unfound", ex.Message);
            }
        }
    }
}