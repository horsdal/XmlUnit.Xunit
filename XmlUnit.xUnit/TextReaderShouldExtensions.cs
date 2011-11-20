namespace XmlUnit.Xunit
{
    using System;
    using System.IO;

    public static class TextReaderShouldExtensions
    {
        public static TextReaderAssertionWrapper WithBaseURI(this TextReader reader, string baseUri)
        {
            return new TextReaderAssertionWrapper(reader, baseUri);
        }

        public static void ShouldContainXmlIdenticalTo(this TextReader actual, TextReader expected)
        {
            XmlAssertion.AssertXmlIdentical(actual, expected);
        }

        public static void ShouldContainXmlEqualTo(this TextReader actual, TextReader expected)
        {
            XmlAssertion.AssertXmlEquals(actual, expected);
        }

        public static void ShouldNotContainXmlIdenticalTo(this TextReader actual, TextReader expected)
        {
            var diff = new XmlDiff(expected, actual);
            XmlAssertion.AssertXmlNotIdentical(diff);
        }

        public static void ShouldNotContainXmlEqualTo(this TextReader actual, TextReader expected)
        {
            var diff = new XmlDiff(expected, actual);
            XmlAssertion.AssertXmlNotEquals(diff);
        }

        public static void ShouldContainValidXml(this TextReader actual)
        {
            XmlAssertion.AssertXmlValid(actual);
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
