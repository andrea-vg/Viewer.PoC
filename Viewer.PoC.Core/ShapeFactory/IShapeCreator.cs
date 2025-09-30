using Viewer.PoC.Core.Logging;
using Viewer.PoC.Model.Dtos;
using Viewer.PoC.Model.Enums;
using Viewer.PoC.Model.Models;

namespace Viewer.PoC.Core.ShapeFactory
{
    public interface IShapeCreator
    {
        ShapeTypeEnum ShapeType { get; }
        public IShape Create(IShapeDto dto, ILogging logger);
    }
}