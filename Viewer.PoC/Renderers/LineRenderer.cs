using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Viewer.PoC.Model.Enums;
using Viewer.PoC.Model.Models;
using Services = Viewer.PoC.Core.Services;

namespace Viewer.PoC.UI.Renderers
{
    public class LineRenderer : IShapeRenderStrategy
    {
        public ShapeTypeEnum ShapeType => ShapeTypeEnum.Line;

        public UIElement Render(DrawableShape shape, Services.ISelectionService selection)
        {
            var p0 = shape.Coordinates[0];
            var p1 = shape.Coordinates[1];
            var line = new Line
            {
                X1 = p0.X,
                Y1 = p0.Y,
                X2 = p1.X,
                Y2 = p1.Y,
                Stroke = new SolidColorBrush(StaticRenderHelper.ToMediaColor(shape.Color)),
                StrokeThickness = 1
            };
            StaticRenderHelper.AttachSelection(line, shape, selection);
            return line;
        }
    }
}