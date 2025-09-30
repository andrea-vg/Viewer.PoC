using Viewer.PoC.Core.Helpers;
using Viewer.PoC.Model.Enums;

namespace Viewer.PoC.uTests
{
    internal class DetectShapeDtosTests
    {
        [Test]
        public void GetDtoTypes_ReturnsTypesForKnownEnums()
        {
            var map = DetectShapeDtos.GetDtoTypes();
            Assert.That(map.ContainsKey(ShapeTypeEnum.Line), Is.True);
            Assert.That(map.ContainsKey(ShapeTypeEnum.Circle), Is.True);
            Assert.That(map.ContainsKey(ShapeTypeEnum.Triangle), Is.True);
            Assert.That(map[ShapeTypeEnum.Line], Is.Not.Null);
            Assert.That(map[ShapeTypeEnum.Circle], Is.Not.Null);
            Assert.That(map[ShapeTypeEnum.Triangle], Is.Not.Null);
        }
    }
}
