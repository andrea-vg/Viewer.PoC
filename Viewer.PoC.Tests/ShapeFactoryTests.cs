using Moq;
using Viewer.PoC.Core.Exceptions;
using Viewer.PoC.Core.Logging;
using Viewer.PoC.Core.Mapping.DtoToShape;
using Viewer.PoC.Core.ShapeFactory;
using Viewer.PoC.Model.Dtos;
using Viewer.PoC.Model.Enums;

namespace Viewer.PoC.uTests
{
    public class ShapeFactoryTests
    {
        private ShapeFactory CreateFactory()
        {
            var logger = new ConsoleLogger();
            var creators = new List<IShapeCreator>
            {
                new LineCreator(),
                new CircleCreator(),
                new TriangleCreator()
            };
            return new ShapeFactory(creators, logger);
        }

        [Test]
        public void CreateShape_LineDto_ReturnsLineShape()
        {
            var factory = CreateFactory();
            var dto = new LineDto
            {
                Type = "line",
                A = "-1,5; 3,4",
                B = "2,2; 5,7",
                Color = "255;0;0;0"
            };
            var shape = factory.Create(dto);
            Assert.That(shape.ShapeType, Is.EqualTo(ShapeTypeEnum.Line));
        }

        [Test]
        public void CreateShape_InvalidDto_ThrowsException()
        {
            var factory = CreateFactory();
            var invalidDto = new Mock<IShapeDto>().Object;
            Assert.Throws<ShapeTypeNotSupportedException>(() => factory.Create(invalidDto));
        }

        [Test]
        public void CreateShape_ValidTypeWithoutCreator_ThrowsShapeCreationException()
        {
            var logger = new ConsoleLogger();
            var creators = new List<IShapeCreator>();
            var factory = new ShapeFactory(creators, logger);

            var dto = new Mock<IShapeDto>();
            dto.Setup(d => d.Type).Returns("Line"); 
            Assert.Throws<ShapeCreationException>(() => factory.Create(dto.Object));
        }
    }
}