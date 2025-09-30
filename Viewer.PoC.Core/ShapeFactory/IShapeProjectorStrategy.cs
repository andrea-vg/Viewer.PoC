using System;
using Viewer.PoC.Model.Models;

namespace Viewer.PoC.Core.ShapeFactory
{
    public interface IShapeProjectorStrategy
    {
        Type ShapeType { get; }
        DrawableShape Project(IShape shape, TransformationMatrix transformation);
    }
}