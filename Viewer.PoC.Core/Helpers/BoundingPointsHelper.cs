using System.Collections.Generic;
using Viewer.PoC.Model.Models;

namespace Viewer.PoC.Core.Helpers
{
    internal class BoundingPointsHelper
    {
        internal static IEnumerable<Coordinates> GetBoundingPoints(IShape shape)
        {
            switch (shape)
            {
                case LineShape line:
                    yield return line.A;
                    yield return line.B;
                    break;
                case CircleShape circle:
                    yield return new Coordinates(circle.Center.X - circle.Radius, circle.Center.Y - circle.Radius);
                    yield return new Coordinates(circle.Center.X + circle.Radius, circle.Center.Y + circle.Radius);
                    break;
                case TriangleShape triangle:
                    yield return triangle.A;
                    yield return triangle.B;
                    yield return triangle.C;
                    break;
            }
        }
    }
}
