namespace XmlUnit.XunitTests
{
    using global::Xunit;
    using global::Xunit.Sdk;
    using XmlUnit.Xunit;
    using System.IO;

    public class XmlAssertionTests
    {
        [Fact]
        public void AssertStringEqualAndIdenticalToSelf()
        {
            string control = "<assert>true</assert>";
            string test = "<assert>true</assert>";
            XmlAssertion.AssertXmlIdentical(control, test);
            XmlAssertion.AssertXmlEquals(control, test);
        }

        [Fact]
        public void AssertDifferentStringsNotEqualNorIdentical()
        {
            string control = "<assert>true</assert>";
            string test = "<assert>false</assert>";
            XmlDiff xmlDiff = new XmlDiff(control, test);
            XmlAssertion.AssertXmlNotIdentical(xmlDiff);
            XmlAssertion.AssertXmlNotEquals(xmlDiff);
        }

        [Fact]
        public void AssertXmlIdenticalUsesOptionalDescription()
        {
            string description = "An Optional Description";
            bool caughtException = true;
            try
            {
                XmlDiff diff = new XmlDiff(new XmlInput("<a/>"), new XmlInput("<b/>"),
                                           new DiffConfiguration(description));
                XmlAssertion.AssertXmlIdentical(diff);
                caughtException = false;
            }
            catch (AssertException e)
            {
                Assert.True(e.Message.IndexOf(description) > -1);
            }
            Assert.True(caughtException);
        }

        [Fact]
        public void AssertXmlEqualsUsesOptionalDescription()
        {
            string description = "Another Optional Description";
            bool caughtException = true;
            try
            {
                XmlDiff diff = new XmlDiff(new XmlInput("<a/>"), new XmlInput("<b/>"),
                                           new DiffConfiguration(description));
                XmlAssertion.AssertXmlEquals(diff);
                caughtException = false;
            }
            catch (AssertException e)
            {
                Assert.True(e.Message.IndexOf(description) > -1);
            }
            Assert.True(caughtException);
        }

        [Fact]
        public void AssertXmlValidTrueForValidFile()
        {
            StreamReader reader = GetStreamReader(ValidatorTests.VALID_FILE);
            try
            {
                XmlAssertion.AssertXmlValid(reader, ".\\..\\..\\");
            }
            finally
            {
                reader.Close();
            }
        }

        [Fact]
        public void AssertXmlValidFalseForInvalidFile()
        {
            using (StreamReader reader = GetStreamReader(ValidatorTests.INVALID_FILE))
            {
                Assert.Throws<TrueException>(
                    () => XmlAssertion.AssertXmlValid(reader, ".\\..\\..\\")
                    );
            }
        }

        private StreamReader GetStreamReader(string file)
        {
            FileStream input = File.Open(file, FileMode.Open, FileAccess.Read);
            return new StreamReader(input);
        }

        private static readonly string MY_SOLAR_SYSTEM =
            "<solar-system><planet name='Earth' position='3' supportsLife='yes'/><planet name='Venus' position='4'/></solar-system>";

        [Fact]
        public void AssertXPathExistsWorksForExistentXPath()
        {
            XmlAssertion.AssertXPathExists("//planet[@name='Earth']",
                                           MY_SOLAR_SYSTEM);
        }

        [Fact]
        public void AssertXPathExistsFailsForNonExistentXPath()
        {
            Assert.Throws<TrueException>(
                () => XmlAssertion.AssertXPathExists("//star[@name='alpha centauri']",
                                                     MY_SOLAR_SYSTEM)
                );
        }

        [Fact]
        public void AssertXPathEvaluatesToWorksForMatchingExpression()
        {
            XmlAssertion.AssertXPathEvaluatesTo("//planet[@position='3']/@supportsLife",
                                                MY_SOLAR_SYSTEM,
                                                "yes");
        }

        [Fact]
        public void AssertXPathEvaluatesToWorksForNonMatchingExpression()
        {
            XmlAssertion.AssertXPathEvaluatesTo("//planet[@position='4']/@supportsLife",
                                                MY_SOLAR_SYSTEM,
                                                "");
        }

        [Fact]
        public void AssertXPathEvaluatesToWorksConstantExpression()
        {
            XmlAssertion.AssertXPathEvaluatesTo("true()",
                                                MY_SOLAR_SYSTEM,
                                                "True");
            XmlAssertion.AssertXPathEvaluatesTo("false()",
                                                MY_SOLAR_SYSTEM,
                                                "False");
        }

        [Fact]
        public void AssertXslTransformResultsWorksWithStrings()
        {
            string xslt = XsltTests.IDENTITY_TRANSFORM;
            string someXml = "<a><b>c</b><b/></a>";
            XmlAssertion.AssertXslTransformResults(xslt, someXml, someXml);
        }

        [Fact]
        public void AssertXslTransformResultsWorksWithXmlInput()
        {
            StreamReader xsl = GetStreamReader(".\\..\\..\\etc\\animal.xsl");
            XmlInput xslt = new XmlInput(xsl);
            StreamReader xml = GetStreamReader(".\\..\\..\\etc\\testAnimal.xml");
            XmlInput xmlToTransform = new XmlInput(xml);
            XmlInput expectedXml = new XmlInput("<dog/>");
            XmlAssertion.AssertXslTransformResults(xslt, xmlToTransform, expectedXml);
        }

        [Fact]
        public void AssertXslTransformResultsCatchesFalsePositive()
        {
            StreamReader xsl = GetStreamReader(".\\..\\..\\etc\\animal.xsl");
            XmlInput xslt = new XmlInput(xsl);
            StreamReader xml = GetStreamReader(".\\..\\..\\etc\\testAnimal.xml");
            XmlInput xmlToTransform = new XmlInput(xml);
            XmlInput expectedXml = new XmlInput("<cat/>");

            Assert.Throws<TrueException>(() =>
                   XmlAssertion.AssertXslTransformResults(xslt, xmlToTransform, expectedXml)
                );
        }
    }
}