using Viewer.PoC.Model.Enums;

namespace Viewer.PoC.Model.Models
{
    public interface IShape
    {
        ShapeTypeEnum ShapeType { get; }
        Color Color { get; }
    }
}