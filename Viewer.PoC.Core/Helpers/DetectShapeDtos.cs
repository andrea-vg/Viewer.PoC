using System;
using System.Collections.Generic;
using System.Linq;
using Viewer.PoC.Model.Dtos;
using Viewer.PoC.Model.Enums;

namespace Viewer.PoC.Core.Helpers
{
    public static class DetectShapeDtos
    {
        private static readonly string DTO = "Dto";
        public static Dictionary<ShapeTypeEnum, Type> GetDtoTypes()
        {
            var shapeTypeEnumValues = Enum.GetValues(typeof(ShapeTypeEnum));
            var dtoTypes = typeof(IShapeDto).Assembly.GetTypes()
                .Where(t => typeof(IShapeDto).IsAssignableFrom(t) && t.IsClass && t.Name.EndsWith(DTO))
                .ToList();

            var map = new Dictionary<ShapeTypeEnum, Type>();
            foreach (ShapeTypeEnum shapeType in shapeTypeEnumValues)
            {
                var dtoType = dtoTypes.FirstOrDefault(t => string.Equals(t.Name, shapeType.ToString() + DTO, StringComparison.OrdinalIgnoreCase));
                if (dtoType != null)
                {
                    map[shapeType] = dtoType;
                }
            }
            return map;
        }
    }
}
