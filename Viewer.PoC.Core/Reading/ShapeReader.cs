using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Viewer.PoC.Core.ShapeFactory;
using Viewer.PoC.Model.Models;
using Viewer.PoC.Core.Exceptions;

namespace Viewer.PoC.Core.Reading
{
    public class ShapeReader(IEnumerable<IFileReader> fileReaders, IShapeFactory factory) : IShapeReader
    {
        public async Task<IReadOnlyList<IShape>> ReadShapesAsync(string path, CancellationToken token = default)
        {
            var ext = Path.GetExtension(path)?.ToLowerInvariant();

            var reader = fileReaders.FirstOrDefault(r => r.SupportedExtensions.Contains(ext)) ?? throw new ShapeFileReadException($"No reader registered for extension {ext}");
            var dtos = await reader.ReadDtosAsync(path, token).ConfigureAwait(false);

            var list = new List<IShape>();
            foreach (var dto in dtos)
            {
                token.ThrowIfCancellationRequested();
                list.Add(factory.Create(dto));
            }
            return (IReadOnlyList<IShape>)list;
        }
    }
}
