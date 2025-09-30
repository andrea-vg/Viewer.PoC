using Moq;
using Viewer.PoC.Core.Services;
using Viewer.PoC.Core.ShapeFactory;
using Viewer.PoC.Core.Transform;
using Viewer.PoC.Core.Transform.Bounding;
using Viewer.PoC.Model.Enums;
using Viewer.PoC.Model.Models;
using Viewer.PoC.ViewModel;

namespace Viewer.PoC.uTests
{
    public class MainViewModelTests
    {
        private MainViewModel mainViewModel;
        private DrawableShape shape;
        private Mock<IShapeLoadingService> mockLoader;
        private Mock<IShapeProjector> mockProjector;
        private Mock<ICanvasTransform> mockTransform;


        private ShapeBoundsService shapeBoundsService;

        [SetUp]
        public void SetUp()
        {
            var domainShape = new Mock<IShape>().Object;
            shape = new DrawableShape(ShapeTypeEnum.Line, [new Coordinates(0, 0), new Coordinates(1, 1)], new Color(255, 0, 0, 0), domainForSelection: domainShape);

            shapeBoundsService = new ShapeBoundsService(new List<IShapeBoundsProvider>
            {
                new LineBoundsProvider(),
                new CircleBoundsProvider(),
                new TriangleBoundsProvider()
            });

            mockLoader = new Mock<IShapeLoadingService>();
            mockLoader
                .Setup(l => l.LoadShapesAsync(It.IsAny<string>(), It.IsAny<Dimensions>(), It.IsAny<IProgress<string>>(), shapeBoundsService, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<DrawableShape> { shape });

            mockProjector = new Mock<IShapeProjector>();
            mockProjector
                .Setup(p => p.Project(It.IsAny<IEnumerable<IShape>>(), It.IsAny<TransformationMatrix>()))
                .Returns(new List<DrawableShape> { shape });

            mockTransform = new Mock<ICanvasTransform>();

            mockTransform
                .Setup(t => t.WorldToScreen(It.IsAny<Dimensions>(), It.IsAny<IEnumerable<IShape>>(), shapeBoundsService))
                .Returns(TransformationMatrix.Identity);

            mainViewModel = new MainViewModel(mockLoader.Object, mockProjector.Object, mockTransform.Object, shapeBoundsService);

        }

        [Test]
        public async Task LoadAsync_UpdatesDrawableShapesAndStatus_Moq()
        {
            string status = string.Empty;
            mainViewModel.PropertyChanged += (s, e) => { if (e.PropertyName == "Status") status = mainViewModel.Status; };

            await mainViewModel.LoadAsync("test", new Dimensions(100, 100), async _ => await Task.CompletedTask, default);

            Assert.That(status, Is.EqualTo("Loaded 1 elements"));
            Assert.That(mainViewModel.DrawableShapes.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task ReprojectShapes_UpdatesDrawableShapes_Moq()
        {
            await mainViewModel.LoadAsync("test", new Dimensions(100, 100), async _ => await Task.CompletedTask, default);

            int count = 0;
            await mainViewModel.ReprojectShapes(new Dimensions(200, 200), async drawables =>
            {
                count = drawables.Count;
                await Task.CompletedTask;
            });

            Assert.That(count, Is.EqualTo(1));
            Assert.That(mainViewModel.DrawableShapes.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task LoadAsync_WithEmptyShapes_UpdatesStatusAndDrawableShapes()
        {
            mockLoader
                .Setup(l => l.LoadShapesAsync(It.IsAny<string>(), It.IsAny<Dimensions>(), It.IsAny<IProgress<string>>(), shapeBoundsService, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<DrawableShape>());

            mainViewModel = new MainViewModel(mockLoader.Object, mockProjector.Object, mockTransform.Object, shapeBoundsService);

            string status = string.Empty;
            mainViewModel.PropertyChanged += (s, e) => { if (e.PropertyName == "Status") status = mainViewModel.Status; };

            await mainViewModel.LoadAsync("test", new Dimensions(100, 100), async _ => await Task.CompletedTask, default);

            Assert.That(status, Is.EqualTo("Loaded 0 elements"));
            Assert.That(mainViewModel.DrawableShapes.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task LoadAsync_ClearsDrawableShapesBeforeLoading()
        {
            mainViewModel.DrawableShapes.Add(shape);
            mainViewModel.DrawableShapes.Add(shape);

            Assert.That(mainViewModel.DrawableShapes.Count, Is.EqualTo(2));

            await mainViewModel.LoadAsync("test", new Dimensions(100, 100), async _ => await Task.CompletedTask, default);

            Assert.That(mainViewModel.DrawableShapes.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task LoadAsync_SetsStatusToLoadingCanceled_OnCancellation()
        {
            var cts = new CancellationTokenSource();
            cts.Cancel();

            mockLoader
                .Setup(l => l.LoadShapesAsync(It.IsAny<string>(), It.IsAny<Dimensions>(), It.IsAny<IProgress<string>>(), shapeBoundsService, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new OperationCanceledException());

            mainViewModel = new MainViewModel(mockLoader.Object, mockProjector.Object, mockTransform.Object, shapeBoundsService);

            string status = string.Empty;
            mainViewModel.PropertyChanged += (s, e) => { if (e.PropertyName == "Status") status = mainViewModel.Status; };

            await mainViewModel.LoadAsync("test", new Dimensions(100, 100), async _ => await Task.CompletedTask, cts.Token);

            Assert.That(status, Is.EqualTo("Loading canceled."));
        }

        [Test]
        public void Initial_DrawableShapes_IsEmpty()
        {
            Assert.That(mainViewModel.DrawableShapes.Count, Is.EqualTo(0));
        }
    }
}