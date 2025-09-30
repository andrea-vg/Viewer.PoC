using Moq;
using Viewer.PoC.Core.Exceptions;
using Viewer.PoC.Core.Reading;
using Viewer.PoC.Core.ShapeFactory;

namespace Viewer.PoC.uTests
{
    internal class ShapeReaderTests
    {
        [Test]
        public void ReadShapesAsync_ThrowsShapeFileReadException_WhenNoReaderSupportsExtension()
        {
            var mockFactory = new Mock<IShapeFactory>();
            var fileReaders = new List<IFileReader>();
            var shapeReader = new ShapeReader(fileReaders, mockFactory.Object);

            var ex = Assert.ThrowsAsync<ShapeFileReadException>(async () =>
            {
                await shapeReader.ReadShapesAsync("test.xml");
            });

            Assert.That(ex.Message, Does.Contain("No reader registered for extension .xml"));
        }
    }
}