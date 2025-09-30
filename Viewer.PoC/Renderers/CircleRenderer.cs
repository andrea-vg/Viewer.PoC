using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Viewer.PoC.Model.Enums;
using Viewer.PoC.Model.Models;
using Services = Viewer.PoC.Core.Services;

namespace Viewer.PoC.UI.Renderers
{
    public class CircleRenderer : IShapeRenderStrategy
    {
        public ShapeTypeEnum ShapeType => ShapeTypeEnum.Circle;

        public UIElement Render(DrawableShape ds, Services.ISelectionService selection)
        {
            var c = ds.Coordinates[0];
            var r = ds.Radius;
            var ellipse = new Ellipse
            {
                Width = r * 2,
                Height = r * 2,
                Stroke = new SolidColorBrush(StaticRenderHelper.ToMediaColor(ds.Color)),
                StrokeThickness = 1
            };
            if (ds.Filled)
            {
                ellipse.Fill = new SolidColorBrush(StaticRenderHelper.ToMediaColor(ds.Color));
            }
            Canvas.SetLeft(ellipse, c.X - r);
            Canvas.SetTop(ellipse, c.Y - r);
            StaticRenderHelper.AttachSelection(ellipse, ds, selection);
            return ellipse;
        }
    }
}