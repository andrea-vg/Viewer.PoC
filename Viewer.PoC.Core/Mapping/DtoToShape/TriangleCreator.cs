using System;
using Viewer.PoC.Core.Logging;
using Viewer.PoC.Core.Parsing;
using Viewer.PoC.Core.ShapeFactory;
using Viewer.PoC.Model.Dtos;
using Viewer.PoC.Model.Enums;
using Viewer.PoC.Model.Models;

namespace Viewer.PoC.Core.Mapping.DtoToShape
{
    public class TriangleCreator : IShapeCreator
    {
        public ShapeTypeEnum ShapeType => ShapeTypeEnum.Triangle;

        public IShape Create(IShapeDto dto, ILogging logger)
        {
            var triangleDto = dto as TriangleDto ?? throw new ArgumentException(nameof(dto));
            return new TriangleShape(
                CommonParser.ParseCoordinates(triangleDto.A, logger), 
                CommonParser.ParseCoordinates(triangleDto.B, logger),
                CommonParser.ParseCoordinates(triangleDto.C, logger), 
                CommonParser.ParseColor(triangleDto.Color, logger), 
                triangleDto.Filled);
        }
    }
}