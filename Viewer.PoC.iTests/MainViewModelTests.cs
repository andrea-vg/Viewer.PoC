using Viewer.PoC.Core;
using Viewer.PoC.Core.Logging;
using Viewer.PoC.Core.Mapping.DtoToShape;
using Viewer.PoC.Core.Mapping.IShapeToDrawableShape;
using Viewer.PoC.Core.Reading;
using Viewer.PoC.Core.Services;
using Viewer.PoC.Core.ShapeFactory;
using Viewer.PoC.Core.Transform;
using Viewer.PoC.Core.Transform.Bounding;
using Viewer.PoC.Model.Models;
using Viewer.PoC.ViewModel;

namespace Viewer.PoC.iTests
{
    public class MainViewModelTests
    {
        [Test]
        public async Task LoadAsync_UpdatesDrawableShapesAndStatus()
        {
            var testFile = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestFiles", "valid.json");
            var logger = new ConsoleLogger();
            var creators = new List<IShapeCreator>
            {
                new LineCreator(),
                new CircleCreator(),
                new TriangleCreator()
            };
            var boundsProviders = new List<IShapeBoundsProvider>
            {
                new LineBoundsProvider(),
                new CircleBoundsProvider(),
                new TriangleBoundsProvider()
            };
            var boundings = new ShapeBoundsService(boundsProviders);
            var factory = new ShapeFactory(creators, logger);
            var fileReaders = new List<IFileReader> { new JsonFileReader() };

            var shapeReader = new ShapeReader(fileReaders, factory);
            var transform = new CanvasTransform();
            var projector = new ShapeProjector([new LineProjector(), new CircleProjector(), new TriangleProjector()]);

            var loadingService = new ShapeLoadingService(shapeReader, transform, projector, logger);

            var mainVm = new MainViewModel(loadingService, projector, transform, boundings);

            await mainVm.LoadAsync(testFile, new Dimensions(100, 100), async _ => await Task.CompletedTask, CancellationToken.None);

            Assert.That(mainVm.DrawableShapes, Is.Not.Empty);
            Assert.That(mainVm.Status, Does.Contain("Loaded"));
        }
    }
}