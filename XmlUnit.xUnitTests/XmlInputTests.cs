namespace XmlUnit.XunitTests
{
    using System.IO;
    using System.Text;
    using System.Xml;
    using XmlUnit.Xunit;
    using global::Xunit;

    public class XmlInputTests
    {
        private static readonly string INPUT = "<abc><q>werty</q><u>iop</u></abc> ";
        private string expected;

        public XmlInputTests()
        {
            expected = ReadOuterXml(new XmlTextReader(new StringReader(INPUT)));
        }

        [Fact]
        public void StringInputTranslatesToXmlReader()
        {
            XmlInput input = new XmlInput(INPUT);
            string actual = ReadOuterXml(input.CreateXmlReader());
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TextReaderInputTranslatesToXmlReader()
        {
            XmlInput input = new XmlInput(new StringReader(INPUT));
            string actual = ReadOuterXml(input.CreateXmlReader());
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void StreamInputTranslatesToXmlReader()
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream, Encoding.Default);
            writer.WriteLine(INPUT);
            writer.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            XmlInput input = new XmlInput(stream);
            string actual = ReadOuterXml(input.CreateXmlReader());
            try
            {
                Assert.Equal(expected, actual);
            }
            finally
            {
                writer.Close();
            }
        }

        private string ReadOuterXml(XmlReader forReader)
        {
            try
            {
                forReader.MoveToContent();
                return forReader.ReadOuterXml();
            }
            finally
            {
                forReader.Close();
            }
        }

        [Fact]
        public void NotEqualsNull()
        {
            XmlInput input = new XmlInput(INPUT);
            Assert.Equal(false, input.Equals(null));
        }

        [Fact]
        public void NotEqualsADifferentClass()
        {
            XmlInput input = new XmlInput(INPUT);
            Assert.Equal(false, input.Equals(INPUT));
        }

        [Fact]
        public void EqualsSelf()
        {
            XmlInput input = new XmlInput(INPUT);
            Assert.Equal(input, input);
        }

        [Fact]
        public void EqualsCopyOfSelf()
        {
            XmlInput input = new XmlInput(INPUT);
            Assert.Equal(new XmlInput(INPUT), input);
        }

        [Fact]
        public void HashCodeEqualsHashCodeOfInput()
        {
            XmlInput input = new XmlInput(INPUT);
            Assert.Equal(INPUT.GetHashCode(), input.GetHashCode());
        }

        [Fact]
        public void HashCodeEqualsHashCodeOfCopy()
        {
            XmlInput input = new XmlInput(INPUT);
            Assert.Equal(new XmlInput(INPUT).GetHashCode(), input.GetHashCode());
        }
    }
}