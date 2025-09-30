using Viewer.PoC.Core.Extensions;

namespace Viewer.PoC.uTests
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [Test]
        public void SplitAndTrim_EmptyString_ReturnsEmptyArray()
        {
            var result = "".SplitAndTrim(';');
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void SplitAndTrim_WhitespaceString_ReturnsEmptyArray()
        {
            var result = "   ".SplitAndTrim(';');
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void SplitAndTrim_SingleValue_ReturnsSingleElement()
        {
            var result = "test".SplitAndTrim(';');
            Assert.That(result, Is.EqualTo(new[] { "test" }));
        }

        [Test]
        public void SplitAndTrim_MultipleValuesWithSpaces_TrimsEach()
        {
            var result = " test1 ; test2 ;test3 ".SplitAndTrim(';');
            Assert.That(result, Is.EqualTo(new[] { "test1", "test2", "test3" }));
        }

        [Test]
        public void SplitAndTrim_ConsecutiveSeparators_SkipsEmpty()
        {
            var result = "a;;b; ;c".SplitAndTrim(';');
            Assert.That(result, Is.EqualTo(new[] { "a", "b", "c" }));
        }

        [Test]
        public void NormalizeDecimalSeparator_MultipleCommas_AllReplaced()
        {
            var result = "1,23,45".NormalizeDecimalSeparator();
            Assert.That(result, Is.EqualTo("1.23.45"));
        }

        [Test]
        public void NormalizeDecimalSeparator_ReplacesComma()
        {
            var result = "42,42".NormalizeDecimalSeparator();
            Assert.That(result, Is.EqualTo("42.42"));
        }

        [Test]
        public void NormalizeDecimalSeparator_DoesNotChangeDot()
        {
            var result = "1.23".NormalizeDecimalSeparator();
            Assert.That(result, Is.EqualTo("1.23"));
        }

        [Test]
        public void NormalizeDecimalSeparator_EmptyString_ReturnsEmpty()
        {
            var result = "".NormalizeDecimalSeparator();
            Assert.That(result, Is.EqualTo(""));
        }
    }
}