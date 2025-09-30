using Viewer.PoC.ViewModel;
using Viewer.PoC.Model.Models;
using Viewer.PoC.Model.Enums;
using Viewer.PoC.Core.Services;
using Moq;

namespace Viewer.PoC.uTests
{
    public class SelectionViewModelTests
    {
        private class TestShape (ShapeTypeEnum shapeType, Color color): IShape
        {
            public ShapeTypeEnum ShapeType { get; set; } = shapeType;
            public Color Color { get; set; } = color;
        }

        [Test]
        public void SelectionViewModel_UpdatesCurrent_OnSelectionChanged_Moq()
        {
            var mockService = new Mock<ISelectionService>();
            var vm = new SelectionViewModel(mockService.Object);
            var shape = new TestShape(ShapeTypeEnum.Line, new Color(255, 1, 2, 3));

            mockService.Raise(s => s.SelectionChanged += null, mockService.Object, shape);

            Assert.That(vm.Current, Is.EqualTo(shape));
        }
    }
}