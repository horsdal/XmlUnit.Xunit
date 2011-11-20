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
        
        public static XmlInputAssertionWrapper XsltTransformation(this XmlInput original, XmlInput xslt)
        {
            return new XmlInputAssertionWrapper(original, xslt);
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
