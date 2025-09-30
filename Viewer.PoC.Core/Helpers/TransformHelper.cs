using Viewer.PoC.Model.Models;

namespace Viewer.PoC.Core.Helpers
{
    internal static class TransformHelper
    {
        internal static Coordinates Apply(TransformationMatrix t, Coordinates p)
        {
            return 
                new(
                p.X * t.M11 + p.Y * t.M21 + t.OffsetX,
                p.X * t.M12 + p.Y * t.M22 + t.OffsetY
            );
        }
    }
}