using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Viewer.PoC.Model.Dtos;

namespace Viewer.PoC.Core.Reading
{
    public interface IFileReader
    {
        IReadOnlyCollection<string> SupportedExtensions { get; }
        Task<IReadOnlyList<IShapeDto>> ReadDtosAsync(string path, CancellationToken token = default);
    }
}