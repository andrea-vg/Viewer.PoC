using System.Collections.Generic;
using System.Windows;
using Viewer.PoC.Model.Models;

namespace Viewer.PoC.UI.Renderers
{
    public interface IShapeRenderer
    {
        IReadOnlyList<UIElement> Render(IEnumerable<DrawableShape> shapes);
    }
}