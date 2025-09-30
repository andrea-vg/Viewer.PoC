using System;
using System.Collections.Generic;
using System.Linq;
using Viewer.PoC.Model.Enums;
using Viewer.PoC.Model.Models;

namespace Viewer.PoC.Core.Transform.Bounding
{
    public class ShapeBoundsService
    {
        private readonly Dictionary<ShapeTypeEnum, IShapeBoundsProvider> _providers;

        public ShapeBoundsService(IEnumerable<IShapeBoundsProvider> providers)
        {
            _providers = providers.ToDictionary(p => p.ShapeType, p => p);
        }

        public IEnumerable<Coordinates> GetBoundingPoints(IShape shape)
        {
            if (!_providers.TryGetValue(shape.ShapeType, out var provider))
            {
                throw new NotSupportedException($"No bounds provider for {shape.ShapeType}");
            }
            return provider.GetBoundingPoints(shape);
        }
    }
}