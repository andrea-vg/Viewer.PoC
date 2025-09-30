using Viewer.PoC.Model.Enums;

namespace Viewer.PoC.Model.Models
{
    public class LineShape (Coordinates a, Coordinates b, Color color): IShape
    {
        public ShapeTypeEnum ShapeType => ShapeTypeEnum.Line;
        public Coordinates A => a;
        public Coordinates B => b;
        public Color Color => color;
    }
}