using Viewer.PoC.Core;
using Viewer.PoC.Core.Transform;
using Viewer.PoC.Core.Transform.Bounding;
using Viewer.PoC.Model.Models;

namespace Viewer.PoC.uTests
{
    public class CanvasTransformTests
    {
        private ShapeBoundsService shapeBoundsService = new ShapeBoundsService(new List<IShapeBoundsProvider>
        {
            new LineBoundsProvider(),
            new CircleBoundsProvider(),
            new TriangleBoundsProvider()
        });

        [Test]
        public void WorldToScreen_EmptyShapes_ReturnsIdentity()
        {
            var transform = new CanvasTransform();
            var canvasSize = new Dimensions(100, 100);
            var shapes = new List<IShape>();

            var matrix = transform.WorldToScreen(canvasSize, shapes, shapeBoundsService);

            Assert.That(matrix.M11, Is.EqualTo(TransformationMatrix.Identity.M11));
            Assert.That(matrix.M12, Is.EqualTo(TransformationMatrix.Identity.M12));
            Assert.That(matrix.M21, Is.EqualTo(TransformationMatrix.Identity.M21));
            Assert.That(matrix.M22, Is.EqualTo(TransformationMatrix.Identity.M22));
            Assert.That(matrix.OffsetX, Is.EqualTo(TransformationMatrix.Identity.OffsetX));
            Assert.That(matrix.OffsetY, Is.EqualTo(TransformationMatrix.Identity.OffsetY));
        }

        [Test]
        public void WorldToScreen_SingleShape_ReturnsNonIdentity()
        {
            var transform = new CanvasTransform();
            var canvasSize = new Dimensions(200, 200);
            var shapes = new List<IShape>
            {
                new LineShape(new Coordinates(0, 0), new Coordinates(10, 10), new Color(255, 0, 0, 0))
            };

            var matrix = transform.WorldToScreen(canvasSize, shapes, shapeBoundsService);

            Assert.That(matrix.M11, Is.Not.EqualTo(TransformationMatrix.Identity.M11));
        }
    }
}