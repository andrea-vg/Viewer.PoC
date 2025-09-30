namespace Viewer.PoC.Model.Dtos
{
    public class LineDto : IShapeDto
    {
        public string Type { get; set; }
        public string A { get; set; }
        public string B { get; set; }
        public string Color { get; set; }
    }
}