using Viewer.PoC.Model.Dtos;
using Viewer.PoC.Model.Models;

namespace Viewer.PoC.Core.ShapeFactory
{
    public interface IShapeFactory
    {
        IShape Create(IShapeDto dto);
    }
}