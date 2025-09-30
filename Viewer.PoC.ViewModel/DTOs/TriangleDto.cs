namespace Viewer.PoC.Model.Dtos
{
    public class TriangleDto : IShapeDto
    {
        public string Type { get; set; }
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public bool Filled { get; set; }
        public string Color { get; set; }
    }
}