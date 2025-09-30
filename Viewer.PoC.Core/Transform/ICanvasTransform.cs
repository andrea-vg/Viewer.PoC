using System.Collections.Generic;
using Viewer.PoC.Core.Transform.Bounding;
using Viewer.PoC.Model.Models;

namespace Viewer.PoC.Core.Transform
{
    public interface ICanvasTransform
    {
        TransformationMatrix WorldToScreen(Dimensions canvasSize, IEnumerable<IShape> shapes, ShapeBoundsService boundsService);
    }
}
