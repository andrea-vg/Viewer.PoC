namespace Viewer.PoC.Core.Dtos
{
    public class CircleDto : IShapeDto
    {
        public string Type { get; set; }
        public string Center { get; set; }
        public double Radius { get; set; }
        public bool Filled { get; set; }
        public string Color { get; set; }
    }
}