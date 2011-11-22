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

        public static XmlInputXsltAssertionWrapper XsltTransformation(this XmlInput original, XmlInput xslt)
        {
            return new XmlInputXsltAssertionWrapper(original, xslt);
        }

        public static void ShouldBeValidXml(this XmlInput actual)
        {
            XmlAssertion.AssertXmlValid(actual);
        }

        public static XmlInputXPathAssertionWrapper XPath(this XmlInput original, string xPath)
        {
            return new XmlInputXPathAssertionWrapper(original, xPath);
        }
    }

    public class XmlInputXPathAssertionWrapper
    {
        private XmlInput original;
        private string xPath;

        public XmlInputXPathAssertionWrapper(XmlInput original, string xPath)
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

    public class XmlInputXsltAssertionWrapper
    {
        private XmlInput original;
        private XmlInput operation;

        public XmlInputXsltAssertionWrapper(XmlInput original, XmlInput operation)
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
