using Viewer.PoC.Core;
using Viewer.PoC.Core.Exceptions;
using Viewer.PoC.Core.Logging;
using Viewer.PoC.Core.Mapping.DtoToShape;
using Viewer.PoC.Core.Mapping.IShapeToDrawableShape;
using Viewer.PoC.Core.Reading;
using Viewer.PoC.Core.Services;
using Viewer.PoC.Core.ShapeFactory;
using Viewer.PoC.Core.Transform;
using Viewer.PoC.Core.Transform.Bounding;
using Viewer.PoC.Model.Models;

namespace Viewer.PoC.iTests
{
    public class ShapeLoadingServiceTests
    {
        readonly string validFile = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestFiles", "valid.json");
        readonly string invalidFile = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestFiles", "invalid.json");
        readonly string emptyFile = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestFiles", "empty.json");
        private ShapeLoadingService service;
        private ShapeBoundsService boundsService;

        [SetUp]
        public void Setup()
        {
            service = CreateService();
            boundsService = CreateBoundsService();
        }

        [Test]
        public async Task LoadAsync_LoadsShapesFromJsonFile()
        {
            var result = await service.LoadShapesAsync(validFile, new Dimensions(100, 100), null, boundsService, CancellationToken.None);

            Assert.That(result, Is.Not.Empty);
        }

        [Test]
        public void LoadAsync_InvalidJson_ThrowsShapeParseException()
        {
            Assert.ThrowsAsync<ShapeParseException>(async () =>
            {
                await service.LoadShapesAsync(invalidFile, new Dimensions(100, 100), null, boundsService, CancellationToken.None);
            });
        }

        [Test]
        public void LoadAsync_NonExistentFile_ThrowsShapeParseException()
        {
            var testFile = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestFiles", "notExisting.json");

            Assert.ThrowsAsync<ShapeParseException>(async () =>
            {
                await service.LoadShapesAsync(testFile, new Dimensions(100, 100), null, boundsService, CancellationToken.None);
            });
        }

        [Test]
        public async Task LoadAsync_EmptyJsonArray_ReturnsEmptyList()
        {
            var result = await service.LoadShapesAsync(emptyFile, new Dimensions(100, 100), null, boundsService, CancellationToken.None);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void LoadAsync_Cancellation_ThrowsException()
        {
            var cts = new CancellationTokenSource();
            cts.Cancel();

            var ex = Assert.CatchAsync(async () =>
            {
                await service.LoadShapesAsync(validFile, new Dimensions(100, 100), null, boundsService, cts.Token);
            });
            Assert.That(ex, Is.TypeOf<TaskCanceledException>().Or.TypeOf<OperationCanceledException>());
        }

        private static ShapeLoadingService CreateService()
        {
            var logger = new ConsoleLogger();
            var creators = new List<IShapeCreator>
            {
                new LineCreator(),
                new CircleCreator(),
                new TriangleCreator()
            };
            var factory = new ShapeFactory(creators, logger);
            var fileReaders = new List<IFileReader> { new JsonFileReader() };
            var shapeReader = new ShapeReader(fileReaders, factory);
            var transform = new CanvasTransform();
            var projector = new ShapeProjector([new LineProjector(), new CircleProjector(), new TriangleProjector()]);
            return new ShapeLoadingService(shapeReader, transform, projector, logger);
        }
        private static ShapeBoundsService CreateBoundsService()
        {
            var providers = new List<IShapeBoundsProvider>
            {
                new LineBoundsProvider(),
                new CircleBoundsProvider(),
                new TriangleBoundsProvider()
            };
            return new ShapeBoundsService(providers);
        }
    }
}