using System;
using Viewer.PoC.Core.Helpers;
using Viewer.PoC.Core.ShapeFactory;
using Viewer.PoC.Model.Enums;
using Viewer.PoC.Model.Models;

namespace Viewer.PoC.Core.Mapping.IShapeToDrawableShape
{
    public class TriangleProjector : IShapeProjectorStrategy
    {
        public Type ShapeType => typeof(TriangleShape);

        public DrawableShape Project(IShape shape, TransformationMatrix transformation)
        {
            var triangle = (TriangleShape)shape;
            return new DrawableShape(ShapeTypeEnum.Triangle,
                [TransformHelper.Apply(transformation, triangle.A),TransformHelper.Apply(transformation, triangle.B),TransformHelper.Apply(transformation, triangle.C)],
                triangle.Color, 
                triangle.Filled, 
                0, 
                triangle);
        }
    }
}