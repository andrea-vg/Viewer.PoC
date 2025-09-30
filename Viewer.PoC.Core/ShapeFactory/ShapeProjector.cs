using System;
using System.Collections.Generic;
using System.Linq;
using Viewer.PoC.Model.Models;

namespace Viewer.PoC.Core.ShapeFactory
{
    public class ShapeProjector(IEnumerable<IShapeProjectorStrategy> strategies) : IShapeProjector
    {
        private readonly Dictionary<Type, IShapeProjectorStrategy> _strategies = strategies.ToDictionary(s => s.ShapeType, s => s);

        public IEnumerable<DrawableShape> Project(IEnumerable<IShape> shapes, TransformationMatrix transform)
        {
            foreach (var s in shapes)
            {
                if (_strategies.TryGetValue(s.GetType(), out var strat))
                {
                    yield return strat.Project(s, transform);
                }
                else
                {
                    throw new NotSupportedException($"No projector for shape {s.GetType().Name}");
                }
            }
        }
    }
}