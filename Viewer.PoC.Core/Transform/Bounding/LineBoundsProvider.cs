using System.Collections.Generic;
using Viewer.PoC.Model.Enums;
using Viewer.PoC.Model.Models;

namespace Viewer.PoC.Core.Transform.Bounding
{
    public class LineBoundsProvider : IShapeBoundsProvider
    {
        public ShapeTypeEnum ShapeType => ShapeTypeEnum.Line;

        public IEnumerable<Coordinates> GetBoundingPoints(IShape shape)
        {
            var line = (LineShape)shape;
            yield return line.A;
            yield return line.B;
        }
    }
}