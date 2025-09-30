using System.Collections.Generic;
using Viewer.PoC.Model.Enums;
using Viewer.PoC.Model.Models;

namespace Viewer.PoC.Core.Transform.Bounding
{
    public class TriangleBoundsProvider : IShapeBoundsProvider
    {
        public ShapeTypeEnum ShapeType => ShapeTypeEnum.Triangle;

        public IEnumerable<Coordinates> GetBoundingPoints(IShape shape)
        {
            var triangle = (TriangleShape)shape;
            yield return triangle.A;
            yield return triangle.B;
            yield return triangle.C;
        }
    }
}