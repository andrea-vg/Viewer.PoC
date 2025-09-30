using Viewer.PoC.Model.Enums;

namespace Viewer.PoC.Model.Models
{
    public class CircleShape (Coordinates center, double radius, Color color, bool filled): IFilledShape
    {
        public ShapeTypeEnum ShapeType => ShapeTypeEnum.Circle;
        public bool Filled => filled;
        public Color Color => color;
        public Coordinates Center => center;
        public double Radius => radius;
    }
}