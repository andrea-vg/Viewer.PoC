using Viewer.PoC.Core.Reading;
using Viewer.PoC.Core.Exceptions;

namespace Viewer.PoC.uTests
{
    public class JsonFileReaderTests
    {
        private static string CreateTempFile(string content)
        {
            var path = Path.GetTempFileName();
            File.WriteAllText(path, content);
            return path;
        }

        [Test]
        public async Task ReadDtosAsync_ThrowsOnCancellation()
        {
            var reader = new JsonFileReader();
            var tokenSource = new CancellationTokenSource();
            tokenSource.Cancel();

            Assert.ThrowsAsync<TaskCanceledException>(async () =>
            {
                await reader.ReadDtosAsync("sample.json", tokenSource.Token);
            });
        }

        [Test]
        public async Task ReadDtosAsync_ValidJson_ReturnsDtos()
        {
            string json = @"[
                { ""type"": ""Line"", ""Type"": ""Line"", ""Type"": ""Line"", ""X1"": 0, ""Y1"": 0, ""X2"": 1, ""Y2"": 1 },
                { ""type"": ""Circle"", ""X"": 5, ""Y"": 5, ""Radius"": 2 }
            ]";
            var path = CreateTempFile(json);
            var reader = new JsonFileReader();

            var dtos = await reader.ReadDtosAsync(path, CancellationToken.None);

            Assert.That(dtos, Is.Not.Empty);
            Assert.That(dtos[0].Type.ToLower(), Is.EqualTo("line"));
            Assert.That(dtos[1].Type.ToLower(), Is.EqualTo("circle"));
        }

        [Test]
        public void ReadDtosAsync_InvalidJson_ThrowsShapeParseException()
        {
            string json = @"{ not valid json }";
            var path = CreateTempFile(json);
            var reader = new JsonFileReader();

            Assert.ThrowsAsync<ShapeParseException>(async () =>
            {
                await reader.ReadDtosAsync(path, CancellationToken.None);
            });
        }

        [Test]
        public void ReadDtosAsync_UnknownType_ThrowsShapeParseException()
        {
            string json = @"[
                { ""type"": ""UnknownShape"", ""test"": 1 }
            ]";
            var path = CreateTempFile(json);
            var reader = new JsonFileReader();

            Assert.ThrowsAsync<ShapeParseException>(async () =>
            {
                await reader.ReadDtosAsync(path, CancellationToken.None);
            });
        }
    }
}