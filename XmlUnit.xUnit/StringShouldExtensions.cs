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

        public static StringAssertionWrapper XPath(this string actual, string xPath)
        {
            return new StringAssertionWrapper(actual, xPath);
        }

        public static StringAssertionWrapper XsltTransformation(this string original, string xslt)
        {
            return new StringAssertionWrapper(original, xslt);
        }
    }

    public class StringAssertionWrapper
    {
        private string original;
        private string operation;

        public StringAssertionWrapper(string original, string operation)
        {
            this.original = original;
            this.operation = operation;
        }

        public void ShouldExist()
        {
            XmlAssertion.AssertXPathExists(operation, original);
        }

        public void ShouldEvaluateTo(string expected)
        {
            XmlAssertion.AssertXPathEvaluatesTo(operation, original, expected);
        }

        public void ShouldResultIn(string expected)
        {
            XmlAssertion.AssertXslTransformResults(operation, original, expected);
        }
    }
}
