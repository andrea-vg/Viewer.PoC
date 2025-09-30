using System.Collections.Generic;
using Viewer.PoC.Model.Enums;
using Viewer.PoC.Model.Models;

namespace Viewer.PoC.Core.Transform.Bounding
{
    public interface IShapeBoundsProvider
    {
        public ShapeTypeEnum ShapeType { get; }
        public IEnumerable<Coordinates> GetBoundingPoints(IShape shape);
    }
}
