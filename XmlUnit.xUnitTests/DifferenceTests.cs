namespace XmlUnit.XunitTests
{
    using XmlUnit.Xunit;
    using global::Xunit;

    public class DifferenceTests
    {
        private Difference minorDifference;

        public DifferenceTests()
        {
            DifferenceType id = DifferenceType.ATTR_SEQUENCE_ID;
            Assert.False(Differences.isMajorDifference(id));
            minorDifference = new Difference(id);
        }

        [Fact]
        public void ToStringContainsId()
        {
            string commentDifference = minorDifference.ToString();
            string idValue = "type: " + (int) DifferenceType.ATTR_SEQUENCE_ID;
            Assert.True(commentDifference.IndexOfAny(idValue.ToCharArray()) > 0,
                        "contains " + idValue);
        }
    }
}