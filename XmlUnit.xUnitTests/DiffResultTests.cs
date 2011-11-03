namespace XmlUnit.XunitTests
{
    using XmlUnit.Xunit;
    using System;
    using System.Xml;
    using global::Xunit;

    public class DiffResultTests
    {
        private DiffResult _result;
        private XmlDiff _diff;
        private Difference _majorDifference, _minorDifference;

        public DiffResultTests()
        {
            _result = new DiffResult();
            _diff = new XmlDiff("<a/>", "<b/>");
            _majorDifference = new Difference(DifferenceType.ELEMENT_TAG_NAME_ID, XmlNodeType.Element,
                                              XmlNodeType.Element);
            _minorDifference = new Difference(DifferenceType.ATTR_SEQUENCE_ID, XmlNodeType.Comment, XmlNodeType.Comment);
        }

        [Fact]
        public void NewDiffResultIsEqualAndIdentical()
        {
            Assert.Equal(true, _result.Identical);
            Assert.Equal(true, _result.Equal);
            Assert.Equal("Identical", _result.StringValue);
        }

        [Fact]
        public void NotEqualOrIdenticalAfterMajorDifferenceFound()
        {
            _result.DifferenceFound(_diff, _majorDifference);
            Assert.Equal(false, _result.Identical);
            Assert.Equal(false, _result.Equal);
            Assert.Equal(_diff.OptionalDescription
                         + Environment.NewLine
                         + _majorDifference.ToString(), _result.StringValue);
        }

        [Fact]
        public void NotIdenticalButEqualAfterMinorDifferenceFound()
        {
            _result.DifferenceFound(_diff, _minorDifference);
            Assert.Equal(false, _result.Identical);
            Assert.Equal(true, _result.Equal);
            Assert.Equal(_diff.OptionalDescription
                         + Environment.NewLine
                         + _minorDifference.ToString(), _result.StringValue);
        }
    }
}