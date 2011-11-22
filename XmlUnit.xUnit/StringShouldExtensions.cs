namespace XmlUnit.Xunit
{
    using System;
    using System.IO;

    public static class StringShouldExtensions
    {
        public static void ShouldBeXmlIdenticalTo(this string actual, string expected)
        {
            XmlAssertion.AssertXmlIdentical(actual, expected);
        }

        public static void ShouldBeXmlEqualTo(this string actual, string expected)
        {
            XmlAssertion.AssertXmlEquals(actual, expected);
        }

        public static void ShouldNotBeXmlIdenticalTo(this string actual, string expected)
        {
            var diff = new XmlDiff(expected, actual);
            XmlAssertion.AssertXmlNotIdentical(diff);
        }

        public static void ShouldNotBeXmlEqualTo(this string actual, string expected)
        {
            var diff = new XmlDiff(expected, actual);
            XmlAssertion.AssertXmlNotEquals(diff);
        }

        public static void ShouldBeValidXml(this string actual)
        {
            XmlAssertion.AssertXmlValid(actual);
        }

        public static StringAssertionWrapper XPath(this string orignal, string xPath)
        {
            return new StringAssertionWrapper(orignal, xPath);
        }

        public static StringAssertionWrapper AppliedTo(this string xPath, string original)
        {
            return new StringAssertionWrapper(original, xPath);
        }

        public static XmlInputXPathAssertionWrapper AppliedTo(this string xPath, XmlInput original)
        {
            return new XmlInputXPathAssertionWrapper(original, xPath);
        }

        public static TextReaderXPathAssertionWrapper AppliedTo(this string xPath, TextReader original)
        {
            return new TextReaderXPathAssertionWrapper(original, xPath);
        }

        public static StringAssertionWrapper XsltTransformation(this string original, string xslt)
        {
            return new StringAssertionWrapper(original, xslt);
        }

        public static StringAssertionWrapper WithBaseURI(this string reader, string baseUri)
        {
            return new StringAssertionWrapper(reader, baseUri);
        }
    }

    public class StringAssertionWrapper
    {
        private string sut;
        private string operation;

        public StringAssertionWrapper(string sut, string operation)
        {
            this.sut = sut;
            this.operation = operation;
        }

        public void ShouldExist()
        {
            XmlAssertion.AssertXPathExists(operation, sut);
        }

        public void ShouldEvaluateTo(string expected)
        {
            XmlAssertion.AssertXPathEvaluatesTo(operation, sut, expected);
        }

        public void ShouldResultIn(string expected)
        {
            XmlAssertion.AssertXslTransformResults(operation, sut, expected);
        }

        public void ShouldBeValidXml()
        {
            XmlAssertion.AssertXmlValid(sut, operation);
        }
    }
}
