using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Viewer.PoC.Model.Enums;
using Viewer.PoC.Model.Models;
using Services = Viewer.PoC.Core.Services;

namespace Viewer.PoC.UI.Renderers
{
    public class TriangleRenderer : IShapeRenderStrategy
    {
        public ShapeTypeEnum ShapeType => ShapeTypeEnum.Triangle;

        public UIElement Render(DrawableShape drawableShape, Services.ISelectionService selection)
        {
            var poloygon = new Polygon
            {
                Points = new PointCollection(),
                Stroke = new SolidColorBrush(StaticRenderHelper.ToMediaColor(drawableShape.Color)),
                StrokeThickness = 1
            };
            foreach (var p in drawableShape.Coordinates)
            {
                poloygon.Points.Add(new Point(p.X, p.Y));
            }
            if (drawableShape.Filled)
            {
                poloygon.Fill = new SolidColorBrush(StaticRenderHelper.ToMediaColor(drawableShape.Color));
            }
            StaticRenderHelper.AttachSelection(poloygon, drawableShape, selection);
            return poloygon;
        }
    }
}