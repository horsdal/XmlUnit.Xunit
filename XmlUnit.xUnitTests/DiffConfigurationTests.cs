namespace XmlUnit.XunitTests
{
    using System;
    using System.IO;
    using System.Xml;
    using XmlUnit.Xunit;
    using global::Xunit;

    public class DiffConfigurationTests
    {
        private const string xmlWithWhitespace = "<elemA>as if<elemB> \r\n </elemB>\t</elemA>";
        private const string xmlWithoutWhitespaceElement = "<elemA>as if<elemB/>\r\n</elemA>";
        private const string xmlWithWhitespaceElement = "<elemA>as if<elemB> </elemB></elemA>";
        private const string xmlWithoutWhitespace = "<elemA>as if<elemB/></elemA>";

        [Fact]
        public void DefaultConfiguredWithGenericDescription()
        {
            var diffConfiguration = new DiffConfiguration();
            Assert.Equal(DiffConfiguration.DEFAULT_DESCRIPTION,
                         diffConfiguration.Description);

            Assert.Equal(DiffConfiguration.DEFAULT_DESCRIPTION,
                         new XmlDiff("", "").OptionalDescription);
        }

        [Fact]
        public void DefaultConfiguredToUseValidatingParser()
        {
            var diffConfiguration = new DiffConfiguration();
            Assert.Equal(DiffConfiguration.DEFAULT_USE_VALIDATING_PARSER,
                         diffConfiguration.UseValidatingParser);

            bool exception = false;
            using (FileStream controlFileStream = File.Open(ValidatorTests.VALID_FILE,
                                                            FileMode.Open,
                                                            FileAccess.Read))
            using (FileStream testFileStream = File.Open(ValidatorTests.INVALID_FILE,
                                                         FileMode.Open,
                                                         FileAccess.Read))
            {
                try
                {
                    var diff = new XmlDiff(new StreamReader(controlFileStream),
                                           new StreamReader(testFileStream));
                    diff.Compare();
                }
                catch (Exception)
                {
                    // should be an XmlSchemaValidationException in .NET 2.0
                    // and later
                    exception = true;
                }
            }
            Assert.True(exception, "expected validation to fail");
        }

        [Fact]
        public void CanConfigureNotToUseValidatingParser()
        {
            var diffConfiguration = new DiffConfiguration(false);
            Assert.Equal(false, diffConfiguration.UseValidatingParser);

            FileStream controlFileStream = File.Open(ValidatorTests.VALID_FILE,
                                                     FileMode.Open, FileAccess.Read);
            FileStream testFileStream = File.Open(ValidatorTests.INVALID_FILE,
                                                  FileMode.Open, FileAccess.Read);
            try
            {
                var diff = new XmlDiff(new XmlInput(controlFileStream),
                                       new XmlInput(testFileStream),
                                       diffConfiguration);
                diff.Compare();
            }
            finally
            {
                controlFileStream.Close();
                testFileStream.Close();
            }
        }

        [Fact]
        public void DefaultConfiguredWithWhitespaceHandlingAll()
        {
            var diffConfiguration = new DiffConfiguration();
            Assert.Equal(WhitespaceHandling.All, diffConfiguration.WhitespaceHandling);

            PerformAssertion(xmlWithoutWhitespace, xmlWithWhitespaceElement, false);
            PerformAssertion(xmlWithoutWhitespace, xmlWithoutWhitespaceElement, false);
            PerformAssertion(xmlWithoutWhitespace, xmlWithWhitespace, false);
            PerformAssertion(xmlWithoutWhitespaceElement, xmlWithWhitespaceElement, false);
        }

        private void PerformAssertion(string control, string test, bool assertion)
        {
            var diff = new XmlDiff(control, test);
            PerformAssertion(diff, assertion);
        }

        private void PerformAssertion(string control, string test, bool assertion,
                                      DiffConfiguration xmlUnitConfiguration)
        {
            var diff = new XmlDiff(new XmlInput(control), new XmlInput(test),
                                   xmlUnitConfiguration);
            PerformAssertion(diff, assertion);
        }

        private void PerformAssertion(XmlDiff diff, bool assertion)
        {
            Assert.Equal(assertion, diff.Compare().Equal);
            Assert.Equal(assertion, diff.Compare().Identical);
        }

        [Fact]
        public void CanConfigureWhitespaceHandlingSignificant()
        {
            var xmlUnitConfiguration =
                new DiffConfiguration(WhitespaceHandling.Significant);
            PerformAssertion(xmlWithoutWhitespace, xmlWithWhitespaceElement,
                             true, xmlUnitConfiguration);
            PerformAssertion(xmlWithoutWhitespace, xmlWithoutWhitespaceElement,
                             true, xmlUnitConfiguration);
            PerformAssertion(xmlWithoutWhitespace, xmlWithWhitespace,
                             true, xmlUnitConfiguration);
            PerformAssertion(xmlWithoutWhitespaceElement, xmlWithWhitespaceElement,
                             true, xmlUnitConfiguration);
        }

        [Fact]
        public void CanConfigureWhitespaceHandlingNone()
        {
            var xmlUnitConfiguration =
                new DiffConfiguration(WhitespaceHandling.None);
            PerformAssertion(xmlWithoutWhitespace, xmlWithWhitespaceElement,
                             true, xmlUnitConfiguration);
            PerformAssertion(xmlWithoutWhitespace, xmlWithoutWhitespaceElement,
                             true, xmlUnitConfiguration);
            PerformAssertion(xmlWithoutWhitespace, xmlWithWhitespace,
                             true, xmlUnitConfiguration);
            PerformAssertion(xmlWithoutWhitespaceElement, xmlWithWhitespaceElement,
                             true, xmlUnitConfiguration);
        }
    }
}