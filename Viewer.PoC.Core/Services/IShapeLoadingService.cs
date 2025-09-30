using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Viewer.PoC.Core.Transform.Bounding;
using Viewer.PoC.Model.Models;

namespace Viewer.PoC.Core.Services
{
    public interface IShapeLoadingService
    {
        Task<IReadOnlyList<DrawableShape>> LoadShapesAsync(string path, Dimensions dimension, System.IProgress<string> progress, ShapeBoundsService boundsService, CancellationToken cancellationToken = default);
    }
}