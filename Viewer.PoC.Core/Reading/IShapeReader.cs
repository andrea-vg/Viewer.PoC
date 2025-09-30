using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Viewer.PoC.Model.Models;

namespace Viewer.PoC.Core.Reading
{
    public interface IShapeReader
    {
        Task<IReadOnlyList<IShape>> ReadShapesAsync(string path, CancellationToken token = default);
    }
}
