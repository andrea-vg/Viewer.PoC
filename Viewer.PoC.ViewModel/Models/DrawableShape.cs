#nullable enable
using System.Collections.Generic;
using Viewer.PoC.Model.Enums;

namespace Viewer.PoC.Model.Models
{

    public class DrawableShape(ShapeTypeEnum shape, IEnumerable<Coordinates> coordinates, Color color, bool filled = false, double radius = 0, IShape? domainForSelection = null)
    {
        public ShapeTypeEnum ShapeType => shape;
        public List<Coordinates> Coordinates => new List<Coordinates>(coordinates);
        public double Radius => radius;
        public Color Color => color;
        public bool Filled => filled;
        public IShape? Domain => domainForSelection;
    }
}