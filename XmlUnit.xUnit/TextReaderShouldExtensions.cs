namespace XmlUnit.Xunit
{
    using System;
    using System.IO;

    public static class TextReaderShouldExtensions
    {
        public static TextReaderValidationAssertionWrapper WithBaseURI(this TextReader reader, string baseUri)
        {
            return new TextReaderValidationAssertionWrapper(reader, baseUri);
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

        public static TextReaderXPathAssertionWrapper XPath(this TextReader original, string xPath)
        {
            return new TextReaderXPathAssertionWrapper(original, xPath);
        }
    }

    public class TextReaderXPathAssertionWrapper
    {
        private TextReader original;
        private string xPath;

        public TextReaderXPathAssertionWrapper(TextReader original, string xPath)
        {
            this.original = original;
            this.xPath = xPath;
        }

        public void ShouldExist()
        {
            XmlAssertion.AssertXPathExists(xPath, original);
        }

        public void ShouldEvaluateTo(string expected)
        {
            XmlAssertion.AssertXPathEvaluatesTo(xPath, original, expected);
        }
    }

    public class TextReaderValidationAssertionWrapper
    {
        private TextReader reader;
        private string baseUri;

        public TextReaderValidationAssertionWrapper(TextReader reader, string baseUri)
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
