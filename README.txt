# Intro #

XmlUnit.Xunit provides Xunit.NET based assertions tailored to testing XML. The project contains assertions for comparing XML, applying XPath and XSLT expression to XML under test. All assertions come in two flavors: A traditional set of static assertion methods, and a fluent "should" style assertions.

## Traditional Assertions ##
The traditional assertions in XmlUnit.Xunit er all static methods on the class XmlAssertion, and are used like this:

        [Fact]
        public void AssertStringEqualAndIdenticalToSelf()
        {
            string control = "<assert>true</assert>";
            string test = "<assert>true</assert>";
            XmlAssertion.AssertXmlIdentical(control, test);
            XmlAssertion.AssertXmlEquals(control, test);
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
        public void AssertXPathEvaluatesToWorksForMatchingExpression()
        {
            XmlAssertion.AssertXPathEvaluatesTo("//planet[@position='3']/@supportsLife",
                                                MY_SOLAR_SYSTEM,
                                                "yes");
        }
		
		[Fact]
        public void AssertXslTransformResultsWorksWithStrings()
        {
            string xslt = XsltTests.IDENTITY_TRANSFORM;
            string someXml = "<a><b>c</b><b/></a>";
            XmlAssertion.AssertXslTransformResults(xslt, someXml, someXml);
        }

## Fluent Assertions ##
The fluent assertions are all extension methods with names starting with Should, and are used like this:

        [Fact]
        public void AssertStringEqualAndIdenticalToSelf()
        {
            string control = "<assert>true</assert>";
            string test = "<assert>true</assert>";
            test.ShouldBeXmlIdenticalTo(control);
            test.ShouldBeXmlEqualTo(control);
        }

        private static readonly string MY_SOLAR_SYSTEM =
            "<solar-system><planet name='Earth' position='3' supportsLife='yes'/><planet name='Venus' position='4'/></solar-system>";

		[Fact]
        public void AssertXPathExestsWorksForXmlInput()
        {
            new XmlInput(MY_SOLAR_SYSTEM)
                .XPath("//planet[@name='Earth']")
                .ShouldExist();
        }
		
		[Fact]
        public void AssertXPathEvaluatesToWorksForMatchingExpression()
        {
            MY_SOLAR_SYSTEM
                .XPath("//planet[@position='3']/@supportsLife")
                .ShouldEvaluateTo("yes");
        }

        [Fact]
        public void AssertXPathExistsWorksWithXpathFirstWithXmlInput()
        {
            var sut = new XmlInput(MY_SOLAR_SYSTEM);
			
            "//planet[@name='Earth']".AppliedTo(sut).ShouldExist();
        }
		
		[Fact]
        public void AssertXPathEvaluatesToWorksWithXPathFirst()
        {
            "//planet[@position='3']/@supportsLife"
                .AppliedTo(MY_SOLAR_SYSTEM)
                .ShouldEvaluateTo("yes");
        }
		
        [Fact]
        public void AssertXslTransformResultsWorksWithStrings()
        {
            string xslt = XsltTests.IDENTITY_TRANSFORM;
            string someXml = "<a><b>c</b><b/></a>";

            someXml.XsltTransformation(xslt).ShouldResultIn(someXml);
        }

## Further Information ##
Is probably best gleened off [the tests] (https://github.com/horsdal/XmlUnit.Xunit/tree/master/XmlUnit.xUnitTests) in this project, especially the tests for [XmlAssertions] (https://github.com/horsdal/XmlUnit.Xunit/blob/master/XmlUnit.xUnitTests/XmlAssertionTests.cs) and the tests for [Should assertions] (https://github.com/horsdal/XmlUnit.Xunit/blob/master/XmlUnit.xUnitTests/XmlShouldAssertionTests.cs).

## Contribute ##
Please do! Fork, code, send pull request. :-)