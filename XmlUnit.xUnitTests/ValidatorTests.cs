namespace XmlUnit.XunitTests
{
    using System.IO;
    using XmlUnit.Xunit;
    using global::Xunit;

    public class ValidatorTests
    {
        public static readonly string VALID_FILE = ".\\..\\..\\etc\\BookXsdGenerated.xml";
        public static readonly string INVALID_FILE = ".\\..\\..\\etc\\invalidBook.xml";

        [Fact]
        public void XsdValidFileIsValid()
        {
            PerformAssertion(VALID_FILE, true);
        }

        private Validator PerformAssertion(string file, bool expected)
        {
            FileStream input = File.Open(file, FileMode.Open, FileAccess.Read);
            try
            {
                Validator validator = new Validator(new XmlInput(new StreamReader(input), ".\\..\\..\\"));
                Assert.Equal(expected, validator.IsValid);
                return validator;
            }
            finally
            {
                input.Close();
            }
        }

        [Fact]
        public void XsdInvalidFileIsNotValid()
        {
            Validator validator = PerformAssertion(INVALID_FILE, false);
            Assert.False(validator.IsValid);
            Assert.True(validator.ValidationMessage
                              .IndexOf("http://www.publishing.org") > -1,
                          validator.ValidationMessage);
            Assert.True(validator.ValidationMessage.IndexOf("Book") > -1,
                          validator.ValidationMessage);
        }
    }
}