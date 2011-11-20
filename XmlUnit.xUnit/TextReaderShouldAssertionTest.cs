namespace XmlUnit.Xunit
{
    using System;
    using System.IO;

    public static class TextReaderShouldAssertionTest
    {
        public static TextReaderAssertionWrapper WithBaseURI(this TextReader reader, string baseUri)
        {
            return new TextReaderAssertionWrapper(reader, baseUri);
        }
    }

    public class TextReaderAssertionWrapper
    {
        private TextReader reader;
        private string baseUri;

        public TextReaderAssertionWrapper(TextReader reader, string baseUri)
        {
            this.reader = reader;
            this.baseUri = baseUri;
        }

        public void ShouldContainValidXml()
        {
            XmlAssertion.AssertXmlValid(reader, baseUri);
        }
    }
}
