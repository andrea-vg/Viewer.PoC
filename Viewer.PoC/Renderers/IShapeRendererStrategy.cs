using System.Windows;
using Viewer.PoC.Model.Enums;
using Viewer.PoC.Model.Models;

namespace Viewer.PoC.UI.Renderers
{
    public interface IShapeRenderStrategy
    {
        ShapeTypeEnum ShapeType { get; }
        UIElement Render(DrawableShape shape, Core.Services.ISelectionService selection);
    }
}