using NUnit.Framework;
using Viewer.PoC.Core.Services;
using Viewer.PoC.Model.Models;

namespace Viewer.PoC.uTests
{
    public class SelectionServiceTests
    {
        [Test]
        public void Select_SetsSelectedShape()
        {
            var service = new SelectionService();
            var shape = new LineShape(new Coordinates(0, 0), new Coordinates(1, 1), new Color(255, 0, 0, 0));
            service.Select(shape);
            Assert.That(service.Selected, Is.EqualTo(shape));
        }
    }
}