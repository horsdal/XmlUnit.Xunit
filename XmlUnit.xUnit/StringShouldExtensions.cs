namespace XmlUnit.Xunit
{
    using System;

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

        public static StringAssertionWrapper XPath(this string actual, string xPath)
        {
            return new StringAssertionWrapper(actual, xPath);
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
