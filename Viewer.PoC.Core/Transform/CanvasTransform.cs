using System.Collections.Generic;
using Viewer.PoC.Core.Helpers;
using Viewer.PoC.Core.Transform.Bounding;
using Viewer.PoC.Model.Models;

namespace Viewer.PoC.Core.Transform
{
    public class CanvasTransform : ICanvasTransform
    {
        public TransformationMatrix WorldToScreen(Dimensions canvasSize, IEnumerable<IShape> shapes, ShapeBoundsService boundsService)
        {
            return BoundingBoxHelper.GetWorldToScreen(canvasSize, shapes, boundsService);
        }
    }
}
