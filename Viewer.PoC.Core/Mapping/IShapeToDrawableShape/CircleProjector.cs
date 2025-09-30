using System;
using Viewer.PoC.Core.Helpers;
using Viewer.PoC.Core.ShapeFactory;
using Viewer.PoC.Model.Enums;
using Viewer.PoC.Model.Models;

namespace Viewer.PoC.Core.Mapping.IShapeToDrawableShape
{
    public class CircleProjector : IShapeProjectorStrategy
    {
        public Type ShapeType => typeof(CircleShape);

        public DrawableShape Project(IShape shape, TransformationMatrix transformation)
        {
            var circle = (CircleShape)shape;
            return new DrawableShape(
                ShapeTypeEnum.Circle,
                [TransformHelper.Apply(transformation, circle.Center)],
                circle.Color, 
                circle.Filled, 
                circle.Radius * transformation.M11, 
                circle);
        }
    }
}