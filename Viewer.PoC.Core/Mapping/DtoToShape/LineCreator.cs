using System;
using Viewer.PoC.Core.Logging;
using Viewer.PoC.Core.Parsing;
using Viewer.PoC.Core.ShapeFactory;
using Viewer.PoC.Model.Dtos;
using Viewer.PoC.Model.Enums;
using Viewer.PoC.Model.Models;

namespace Viewer.PoC.Core.Mapping.DtoToShape
{
    public class LineCreator : IShapeCreator
    {
        public ShapeTypeEnum ShapeType => ShapeTypeEnum.Line;

        public IShape Create(IShapeDto dto, ILogging logger)
        {
            var lineDto = dto as LineDto ?? throw new ArgumentException(nameof(dto));
            return new LineShape(
                CommonParser.ParseCoordinates(lineDto.A, logger), 
                CommonParser.ParseCoordinates(lineDto.B, logger), 
                CommonParser.ParseColor(lineDto.Color, logger));
        }
    }
}