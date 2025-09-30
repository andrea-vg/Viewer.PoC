using System;
using Viewer.PoC.Core.Helpers;
using Viewer.PoC.Core.ShapeFactory;
using Viewer.PoC.Model.Enums;
using Viewer.PoC.Model.Models;

namespace Viewer.PoC.Core.Mapping.IShapeToDrawableShape
{
    public class LineProjector : IShapeProjectorStrategy
    {
        public Type ShapeType => typeof(LineShape);

        public DrawableShape Project(IShape shape, TransformationMatrix transformation)
        {
            var line = (LineShape)shape;
            return new DrawableShape(
                ShapeTypeEnum.Line,
                [TransformHelper.Apply(transformation, line.A), TransformHelper.Apply(transformation, line.B)],
                line.Color,
                false,
                0,
                line);
        }
    }
}