using Viewer.PoC.Model.Enums;

namespace Viewer.PoC.Model.Models
{
    public class TriangleShape(Coordinates a, Coordinates b, Coordinates c, Color color, bool filled) : IFilledShape
    {
        public ShapeTypeEnum ShapeType => ShapeTypeEnum.Triangle;
        public bool Filled => filled;
        public Color Color => color;
        public Coordinates A => a;
        public Coordinates B => b;
        public Coordinates C => c;
    }
}