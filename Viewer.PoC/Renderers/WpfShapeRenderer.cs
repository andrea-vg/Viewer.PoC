using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Viewer.PoC.Model.Models;
using Services = Viewer.PoC.Core.Services;

namespace Viewer.PoC.UI.Renderers
{
    public class WpfShapeRenderer : IShapeRenderer
    {
        private readonly IEnumerable<IShapeRenderStrategy> _strategies;
        private readonly Services.ISelectionService _selection;

        public WpfShapeRenderer(IEnumerable<IShapeRenderStrategy> strategies, Services.ISelectionService selection)
        {
            _strategies = strategies;
            _selection = selection;
        }

        public IReadOnlyList<UIElement> Render(IEnumerable<DrawableShape> drawableShapes)
        {
            var list = new List<UIElement>();
            foreach (var shape in drawableShapes)
            {
                var strat = _strategies.FirstOrDefault(s => s.ShapeType == shape.ShapeType);
                if (strat != null)
                {
                    list.Add(strat.Render(shape, _selection));
                }
            }
            return list;
        }
    }
}