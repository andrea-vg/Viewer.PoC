using Viewer.PoC.Core.Reading;
using Viewer.PoC.Core.Exceptions;

namespace Viewer.PoC.iTests
{
    public class JsonFileReaderTests
    {
        readonly string validJson = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestFiles", "valid.json");
        readonly string invalidJson = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestFiles", "invalid.json");

        [Test]
        public async Task ReadDtosAsync_ThrowsOnCancellation()
        {
            var reader = new JsonFileReader();
            var tokenSource = new CancellationTokenSource();
            tokenSource.Cancel();

            Assert.ThrowsAsync<TaskCanceledException>(async () =>
            {
                await reader.ReadDtosAsync(validJson, tokenSource.Token);
            });
        }

        [Test]
        public async Task ReadDtosAsync_ValidJson_ReturnsDtos()
        {
            var reader = new JsonFileReader();
            var dtos = await reader.ReadDtosAsync(validJson, CancellationToken.None);
            Assert.That(dtos, Is.Not.Empty);
        }

        [Test]
        public void ReadDtosAsync_InvalidJson_ThrowsException()
        {
            var reader = new JsonFileReader();
            Assert.ThrowsAsync<ShapeParseException>(async () =>
            {
                await reader.ReadDtosAsync(invalidJson, CancellationToken.None);
            });
        }

        [Test]
        public async Task ReadDtosAsync_ReturnsCorrectDtoTypes()
        {
            var reader = new JsonFileReader();
            var dtos = await reader.ReadDtosAsync(validJson, CancellationToken.None);
            Assert.That(dtos.All(d => d.Type == "line" || d.Type == "circle" || d.Type == "triangle"));
        }
    }
}