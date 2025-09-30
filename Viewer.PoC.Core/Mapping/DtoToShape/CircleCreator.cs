using System;
using Viewer.PoC.Core.Logging;
using Viewer.PoC.Core.Parsing;
using Viewer.PoC.Core.ShapeFactory;
using Viewer.PoC.Model.Dtos;
using Viewer.PoC.Model.Enums;
using Viewer.PoC.Model.Models;

namespace Viewer.PoC.Core.Mapping.DtoToShape
{
    public class CircleCreator : IShapeCreator
    {
        public ShapeTypeEnum ShapeType => ShapeTypeEnum.Circle;

        public IShape Create(IShapeDto dto, ILogging logger)
        {
            var circleDto = dto as CircleDto ?? throw new ArgumentException(nameof(dto));
            return new CircleShape(
                CommonParser.ParseCoordinates(circleDto.Center, logger), 
                circleDto.Radius, 
                CommonParser.ParseColor(circleDto.Color, logger), 
                circleDto.Filled);
        }
    }
}