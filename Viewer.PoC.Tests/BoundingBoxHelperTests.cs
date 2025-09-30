using Viewer.PoC.Core.Helpers;
using Viewer.PoC.Core.Transform.Bounding;
using Viewer.PoC.Model.Models;

namespace Viewer.PoC.uTests
{
    public class BoundingBoxHelperTests
    {
        private ShapeBoundsService shapeBoundsService = new ShapeBoundsService(new List<IShapeBoundsProvider>
        {
            new LineBoundsProvider(),
            new CircleBoundsProvider(),
            new TriangleBoundsProvider()
        });

        [Test]
        public void ComputeBoundingBox_ReturnsCorrectBounds_ForLine()
        {
            var shapes = new List<IShape>
            {
                new LineShape(new Coordinates(1, 2), new Coordinates(3, 4), new Color(255,0,0,0))
            };

            var bbox = BoundingBoxHelper.ComputeBoundingBox(shapes, shapeBoundsService);

            Assert.That(bbox.MinX, Is.EqualTo(1));
            Assert.That(bbox.MinY, Is.EqualTo(2));
            Assert.That(bbox.Width, Is.EqualTo(2));
            Assert.That(bbox.Height, Is.EqualTo(2));
        }

        [Test]
        public void ComputeBoundingBox_ReturnsNaN_ForEmpty()
        {
            var bbox = BoundingBoxHelper.ComputeBoundingBox(new List<IShape>(), shapeBoundsService);
            Assert.IsTrue(double.IsNaN(bbox.Width));
            Assert.IsTrue(double.IsNaN(bbox.Height));
        }
    }
}