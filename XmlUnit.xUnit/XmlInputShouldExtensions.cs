namespace XmlUnit.Xunit
{
    using System;

    public static class XmlInputShouldExtensions
    {
        public static void ShouldBeXmlIdenticalTo(this XmlInput actual, XmlInput expected)
        {
            XmlAssertion.AssertXmlIdentical(actual, expected);
        }

        public static void ShouldBeXmlEqualTo(this XmlInput actual, XmlInput expected)
        {
            XmlAssertion.AssertXmlEquals(actual, expected);
        }

        public static void ShouldNotBeXmlIdenticalTo(this XmlInput actual, XmlInput expected)
        {
            var diff = new XmlDiff(expected, actual);
            XmlAssertion.AssertXmlNotIdentical(diff);
        }

        public static void ShouldNotBeXmlEqualTo(this XmlInput actual, XmlInput expected)
        {
            var diff = new XmlDiff(expected, actual);
            XmlAssertion.AssertXmlNotEquals(diff);
        }

        public static XmlInputAssertionWrapper XsltTransformation(this XmlInput original, XmlInput xslt)
        {
            return new XmlInputAssertionWrapper(original, xslt);
        }

        public static void ShouldBeValidXml(this XmlInput actual)
        {
            XmlAssertion.AssertXmlValid(actual);
        }
    }

    public class XmlInputAssertionWrapper
    {
        private XmlInput original;
        private XmlInput operation;

        public XmlInputAssertionWrapper(XmlInput original, XmlInput operation)
        {
            this.original = original;
            this.operation = operation;
        }

        public void ShouldResultIn(XmlInput expectedXml)
        {
            XmlAssertion.AssertXslTransformResults(operation, original, expectedXml);
        }
    }
}
