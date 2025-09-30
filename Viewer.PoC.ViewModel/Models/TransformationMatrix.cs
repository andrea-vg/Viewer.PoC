namespace Viewer.PoC.Model.Models
{
    public class TransformationMatrix(double m11, double m12, double m21, double m22, double offsetX, double offsetY)
    {
        public double M11 { get; } = m11; 
        public double M12 { get; } = m12;
        public double M21 { get; } = m21; 
        public double M22 { get; } = m22;
        public double OffsetX { get; } = offsetX; 
        public double OffsetY { get; } = offsetY;

        public static TransformationMatrix Identity => new(1, 0, 0, 1, 0, 0);
    }
}