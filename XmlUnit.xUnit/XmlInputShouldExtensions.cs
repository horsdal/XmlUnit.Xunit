namespace XmlUnit.Xunit
{
    using System;

    public static class XmlInputShouldExtensions
    {
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
