using System;
using System.Collections.Generic;
using System.Linq;
using Viewer.PoC.Core.Exceptions;
using Viewer.PoC.Core.Logging;
using Viewer.PoC.Model.Dtos;
using Viewer.PoC.Model.Enums;
using Viewer.PoC.Model.Models;

namespace Viewer.PoC.Core.ShapeFactory
{
    public class ShapeFactory : IShapeFactory
    {
        private readonly Dictionary<ShapeTypeEnum, IShapeCreator> _creators;
        private readonly ILogging _logger;

        public ShapeFactory(IEnumerable<IShapeCreator> creators, ILogging logger)
        {
            _creators = creators.ToDictionary(c => c.ShapeType, c => c);
            _logger = logger;
        }

        public IShape Create(IShapeDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }
            if (!Enum.TryParse<ShapeTypeEnum>(dto.Type, true, out var type))
            {
                throw new ShapeTypeNotSupportedException(dto.Type);
            }

            if (!_creators.TryGetValue(type, out var creator))
            {
                throw new ShapeCreationException($"No creator registered for {type}");
            }

            return creator.Create(dto, _logger);
        }
    }
}