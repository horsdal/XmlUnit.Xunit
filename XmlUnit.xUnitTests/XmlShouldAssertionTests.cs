namespace XmlUnit.XunitTests
{
    using System.Xml;

    using global::Xunit;
    using global::Xunit.Sdk;
    using XmlUnit.Xunit;
    using System.IO;

    public class XmlShouldAssertionTests
    {
        [Fact]
        public void AssertStringEqualAndIdenticalToSelf()
        {
            string control = "<assert>true</assert>";
            string test = "<assert>true</assert>";
            test.ShouldBeXmlIdenticalTo(control);
            test.ShouldBeXmlEqualTo(control);
        }

        [Fact]
        public void AssertDifferentStringEqualAndIdenticalTrows()
        {
            string control = "<assert>true</assert>";
            string test = "<assert>false</assert>";
            Assert.Throws<TrueException>(() => test.ShouldBeXmlIdenticalTo(control));
            Assert.Throws<TrueException>(() => test.ShouldBeXmlEqualTo(control));
        }

        [Fact]
        public void AssertStringEqualAndIdenticalToSelfWithXmlInput()
        {
            var control = new XmlInput("<assert>true</assert>");
            var test = new XmlInput("<assert>true</assert>");
            test.ShouldBeXmlIdenticalTo(control);
            test.ShouldBeXmlEqualTo(control);
        }

        [Fact]
        public void AssertDifferentStringEqualAndIdenticalThrowsWithXmlInput()
        {
            var control = new XmlInput("<assert>true</assert>");
            var test = new XmlInput("<assert>false</assert>");
            Assert.Throws<TrueException>(() => test.ShouldBeXmlIdenticalTo(control));
            Assert.Throws<TrueException>(() => test.ShouldBeXmlEqualTo(control));
        }

        [Fact]
        public void AssertStringIdenticalToSelfWithTextReaders()
        {
            using (StreamReader reader = GetStreamReader(ValidatorTests.VALID_FILE))
            {
                reader.ShouldContainXmlIdenticalTo(reader);
            }
        }

        [Fact]
        public void AssertStringEqualToSelfWithTextReaders()
        {
            using (StreamReader reader = GetStreamReader(ValidatorTests.VALID_FILE))
            {
                reader.ShouldContainXmlEqualTo(reader);
            }
        }

        [Fact]
        public void AssertDifferentStringIdenticalWithTextReadersThrows()
        {
            using (StreamReader test = GetStreamReader(ValidatorTests.VALID_FILE))
            {
                using (StreamReader control = GetStreamReader(".\\..\\..\\etc\\test.blame.html"))
                {
                    Assert.Throws<TrueException>(() =>
                        test.ShouldContainXmlIdenticalTo(control)
                        );
                }
            }
        }

        [Fact]
        public void AssertDifferentStringEqualWithTextReadersThrows()
        {
            using (StreamReader test = GetStreamReader(ValidatorTests.VALID_FILE))
            {
                using (StreamReader control = GetStreamReader(".\\..\\..\\etc\\test.blame.html"))
                {
                    Assert.Throws<TrueException>(() => 
                        test.ShouldContainXmlEqualTo(control)
                        );
                }
            }
        }

        [Fact]
        public void AssertDifferentStringNotEqualWithTextReaders()
        {
            using (StreamReader test = GetStreamReader(ValidatorTests.VALID_FILE))
            {
                using (StreamReader control = GetStreamReader(".\\..\\..\\etc\\test.blame.html"))
                {
                    test.ShouldNotContainXmlEqualTo(control);
                }
            }
        }

        [Fact]
        public void AssertDifferentStringNotIdenticalWithTextReaders()
        {
            using (StreamReader test = GetStreamReader(ValidatorTests.VALID_FILE))
            {
                using (StreamReader control = GetStreamReader(".\\..\\..\\etc\\test.blame.html"))
                {
                    test.ShouldNotContainXmlIdenticalTo(control);
                }
            }
        }

        [Fact]
        public void AssertDifferentStringsNotEqualNorIdentical()
        {
            string control = "<assert>true</assert>";
            string test = "<assert>false</assert>";
            test.ShouldNotBeXmlIdenticalTo(control);
            test.ShouldNotBeXmlEqualTo(control);
        }

        [Fact]
        public void AssertEqualStringsNotEqualNorIdenticalThrows()
        {
            string control = "<assert>true</assert>";
            string test = "<assert>true</assert>";
            Assert.Throws<FalseException>(() => test.ShouldNotBeXmlIdenticalTo(control));
            Assert.Throws<FalseException>(() => test.ShouldNotBeXmlEqualTo(control));
        }

        [Fact]
        public void AssertDifferentStringNotEqualAndIdenticalWithXmlInput()
        {
            var control = new XmlInput("<assert>true</assert>");
            var test = new XmlInput("<assert>false</assert>");
            test.ShouldNotBeXmlIdenticalTo(control);
            test.ShouldNotBeXmlEqualTo(control);
        }

        [Fact]
        public void AssertXmlValidTrueForValidString()
        {
            "<assert>false</assert>".ShouldBeValidXml();
        }

        [Fact]
        public void AssertXmlValidForInvalidStringThrows()
        {
            Assert.Throws<XmlException>(() =>
                "<assert>false".ShouldBeValidXml()
                );
        }

        [Fact]
        public void AssertXmlValidTrueForValidXmlInput()
        {
           new XmlInput("<assert>false</assert>").ShouldBeValidXml();
        }

        [Fact]
        public void AssertXmlValidForInvalidXmlInputThrows()
        {
            Assert.Throws<XmlException>(() =>
                new XmlInput("<assert>false").ShouldBeValidXml()
                );
        }
        
        [Fact]
        public void AssertXmlValidWithBaseUriTrueForValidString()
        {
            "<assert>false</assert>".WithBaseURI("/").ShouldBeValidXml();
        }

        [Fact]
        public void AssertXmlValidWithBaseUriForInvalidStringThrows()
        {
            Assert.Throws<XmlException>(() =>
                "<assert>false".WithBaseURI("/").ShouldBeValidXml()
                );
        }

        [Fact]
        public void AssertXmlValidTrueForValidFile()
        {
            using (StreamReader reader = GetStreamReader(ValidatorTests.VALID_FILE))
            {
                reader.WithBaseURI(".\\..\\..\\").ShouldContainValidXml();
            }
        }

        [Fact]
        public void AssertXmlValidFalseForInvalidFile()
        {
            using (StreamReader reader = GetStreamReader(ValidatorTests.INVALID_FILE))
            {
                Assert.Throws<TrueException>(
                    () => reader.WithBaseURI(".\\..\\..\\").ShouldContainValidXml()
                    );
            }
        }

        [Fact]
        public void AssertXmlValidForFileNoBaseUri()
        {
            using (StreamReader control = GetStreamReader(".\\..\\..\\etc\\test.blame.html"))
            {
                control.ShouldContainValidXml();
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
            MY_SOLAR_SYSTEM.XPath("//planet[@name='Earth']").ShouldExist();
        }

        [Fact]
        public void AssertXPathExistsFailsForNonExistentXPath()
        {
            Assert.Throws<TrueException>(() =>
                MY_SOLAR_SYSTEM.XPath("//star[@name='alpha centauri']").ShouldExist()
                );
        }

        [Fact]
        public void AssertXPathEvaluatesToWorksForMatchingExpression()
        {
            MY_SOLAR_SYSTEM
                .XPath("//planet[@position='3']/@supportsLife")
                .ShouldEvaluateTo("yes");
        }

        [Fact]
        public void AssertXPathEvaluatesToThrowsForWrongExpectedOnMatchingExpression()
        {
            Assert.Throws<EqualException>(() =>
                MY_SOLAR_SYSTEM
                    .XPath("//planet[@position='3']/@supportsLife")
                    .ShouldEvaluateTo("no")
                );
        }
        
        [Fact]
        public void AssertXPathEvaluatesToWorksForWrongExpecteNonMatchingExpression()
        {
            Assert.Throws<EqualException>(() =>
                MY_SOLAR_SYSTEM
                    .XPath("//planet[@position='4']/@supportsLife")
                    .ShouldEvaluateTo("something")
                );
        }

        [Fact]
        public void AssertXPathEvaluatesToWorksForNonMatchingExpression()
        {
            MY_SOLAR_SYSTEM
                .XPath("//planet[@position='4']/@supportsLife")
                .ShouldEvaluateTo("");
        }

        [Fact]
        public void AssertXPathEvaluatesToWorksConstantExpression()
        {
            MY_SOLAR_SYSTEM
                .XPath("true()").ShouldEvaluateTo("True");
            MY_SOLAR_SYSTEM
                .XPath("false()").ShouldEvaluateTo("False");
        }

        [Fact]
        public void AssertXslTransformResultsWorksWithStrings()
        {
            string xslt = XsltTests.IDENTITY_TRANSFORM;
            string someXml = "<a><b>c</b><b/></a>";

            someXml.XsltTransformation(xslt).ShouldResultIn(someXml);
        }

        [Fact]
        public void AssertXslTransformResultsWorksWithXmlInput()
        {
            StreamReader xsl = GetStreamReader(".\\..\\..\\etc\\animal.xsl");
            XmlInput xslt = new XmlInput(xsl);
            StreamReader xml = GetStreamReader(".\\..\\..\\etc\\testAnimal.xml");
            XmlInput xmlToTransform = new XmlInput(xml);
            XmlInput expectedXml = new XmlInput("<dog/>");

            xmlToTransform.XsltTransformation(xslt).ShouldResultIn(expectedXml);
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
                xmlToTransform.XsltTransformation(xslt).ShouldResultIn(expectedXml)
                );
        }
    }
}