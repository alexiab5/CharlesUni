using hm_ToSum_v2;

namespace ToSumTests
{
    public class CorrectOutputTests
    {
        [Fact]
        public void OnlyHeader()
        {
            //Arrange
            string fileContents = """
                mesic   zbozi       typ         prodejce    mnozstvi    cena    trzba
                """;
            var reader = new StringReader(fileContents);
            var writer = new StringWriter();
            char[] delimiters = { ' ', '\t', '\n' };
            var tokenReader = new TokenReaderByLines(reader, delimiters);
            var tokenProcessor = new TableSummator(tokenReader, "cena");
            //Act
            tokenProcessor.ProcessAllTokens();
            tokenProcessor.WriteReport(writer);
            //Assert
            Assert.Equal("cena" + writer.NewLine + "----" + writer.NewLine + "0", writer.ToString());
        }

        [Fact]
        public void CorrectFileFormatAndColumnName()
        {
            //Arrange
            string fileContents = """
                mesic   zbozi       typ         prodejce    mnozstvi    cena    trzba
                leden   brambory    tuzemske    Bartak      10895       12      130740
                leden   brambory    vlastni     Celestyn    15478       10      154780
                leden   jablka      dovoz       Adamec      1321        30      39630
                """;
            var reader = new StringReader(fileContents);
            var writer = new StringWriter();
            char[] delimiters = { ' ', '\t', '\n' };
            var tokenReader = new TokenReaderByLines(reader, delimiters);
            var tokenProcessor = new TableSummator(tokenReader, "cena");
            //Act
            tokenProcessor.ProcessAllTokens();
            tokenProcessor.WriteReport(writer);
            //Assert
            Assert.Equal("cena" + writer.NewLine + "----" + writer.NewLine + "52", writer.ToString());
        }

        [Fact]
        public void InvalidIntegerValue()
        {
            //Arrange
            string fileContents = """
                mesic   zbozi       typ         prodejce    mnozstvi    cena    trzba
                leden   brambory    tuzemske    Bartak      10895       12      130740
                leden   brambory    vlastni     Celestyn    15478       10      adf
                leden   jablka      dovoz       Adamec      1321        30      39630
                """;
            var reader = new StringReader(fileContents);
            var writer = new StringWriter();
            char[] delimiters = { ' ', '\t', '\n' };
            var tokenReader = new TokenReaderByLines(reader, delimiters);
            var tokenProcessor = new TableSummator(tokenReader, "trzba");
            //Act
            try
            {
                tokenProcessor.ProcessAllTokens();
            }
            //Assert
            catch (Exception ex)
            {
                Assert.Equal("integer conversion exception", ex.Message);
            }
        }

        [Fact]
        public void MoreColumnsWithSameName()
        {
            //Arrange
            string fileContents = """
                mesic   zbozi       typ         prodejce    cena        trzba   cena
                leden   brambory    tuzemske    Bartak      10895       12      130740
                leden   brambory    vlastni     Celestyn    15478       10      adf
                leden   jablka      dovoz       Adamec      1321        30      39630
                asda    sdf         sdgsrg      rgs         4654        435     44363
                """;
            var reader = new StringReader(fileContents);
            var writer = new StringWriter();
            char[] delimiters = { ' ', '\t', '\n' };
            var tokenReader = new TokenReaderByLines(reader, delimiters);
            var tokenProcessor = new TableSummator(tokenReader, "cena");
            //Act
            tokenProcessor.ProcessAllTokens();
            tokenProcessor.WriteReport(writer);
            //Assert
            Assert.Equal("cena" + writer.NewLine + "----" + writer.NewLine + "32348", writer.ToString());
        }
    }
}
