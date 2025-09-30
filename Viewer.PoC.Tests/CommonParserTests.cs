using Viewer.PoC.Core.Exceptions;
using Viewer.PoC.Core.Parsing;

namespace Viewer.PoC.uTests
{
    public class CommonParserTests
    {
        [Test]
        public void ParseCoordinates_ValidString_ReturnsCoordinates()
        {
            var coords = CommonParser.ParseCoordinates("1.5;2.5");
            Assert.That(coords.X, Is.EqualTo(1.5));
            Assert.That(coords.Y, Is.EqualTo(2.5));
        }

        [Test]
        public void ParseCoordinates_EmptyString_ReturnsZero()
        {
            var coords = CommonParser.ParseCoordinates("");
            Assert.That(coords.X, Is.EqualTo(0));
            Assert.That(coords.Y, Is.EqualTo(0));
        }

        [Test]
        public void ParseCoordinates_NotEnoughParts_ReturnsZero()
        {
            var coords = CommonParser.ParseCoordinates("1.5");
            Assert.That(coords.X, Is.EqualTo(0));
            Assert.That(coords.Y, Is.EqualTo(0));
        }


        [Test]
        public void ParseCoordinates_TooManyParts_ThrowsShapeParseException()
        {
            Assert.Throws<ShapeParseException>(() => CommonParser.ParseCoordinates("1.5.4;2.5.4"));
        }

        [Test]
        public void ParseCoordinates_InvalidNumbers_ThrowsShapeParseException()
        {
            Assert.Throws<ShapeParseException>(() => CommonParser.ParseCoordinates("test1;test2"));
        }

        [Test]
        public void ParseColor_ValidString_ReturnsColor()
        {
            var color = CommonParser.ParseColor("255;100;150;200");
            Assert.That(color.A, Is.EqualTo(255));
            Assert.That(color.R, Is.EqualTo(100));
            Assert.That(color.G, Is.EqualTo(150));
            Assert.That(color.B, Is.EqualTo(200));
        }

        [Test]
        public void ParseColor_EmptyString_ReturnsDefault()
        {
            var color = CommonParser.ParseColor("");
            Assert.That(color.A, Is.EqualTo(255));
            Assert.That(color.R, Is.EqualTo(0));
            Assert.That(color.G, Is.EqualTo(0));
            Assert.That(color.B, Is.EqualTo(0));
        }

        [Test]
        public void ParseColor_NotEnoughParts_ReturnsDefault()
        {
            var color = CommonParser.ParseColor("255;100");
            Assert.That(color.A, Is.EqualTo(255));
            Assert.That(color.R, Is.EqualTo(0));
            Assert.That(color.G, Is.EqualTo(0));
            Assert.That(color.B, Is.EqualTo(0));
        }
    }
}