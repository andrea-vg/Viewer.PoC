using System.Collections.Generic;
using Viewer.PoC.Model.Models;

namespace Viewer.PoC.Core.ShapeFactory
{
    public interface IShapeProjector
    {
        IEnumerable<DrawableShape> Project(IEnumerable<IShape> shapes, TransformationMatrix transform);

    }
}
