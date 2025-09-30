using System;
using System.Collections.Generic;
using System.Text;
using Viewer.PoC.Model.Enums;
using Viewer.PoC.Model.Models;

namespace Viewer.PoC.Core.Transform.Bounding
{
    public class CircleBoundsProvider : IShapeBoundsProvider
    {
        public ShapeTypeEnum ShapeType => ShapeTypeEnum.Circle;

        public IEnumerable<Coordinates> GetBoundingPoints(IShape shape)
        {
            var circle = (CircleShape)shape;
            yield return new Coordinates(circle.Center.X - circle.Radius, circle.Center.Y - circle.Radius);
            yield return new Coordinates(circle.Center.X + circle.Radius, circle.Center.Y + circle.Radius);
        }
    }
}