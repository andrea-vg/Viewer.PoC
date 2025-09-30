using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Viewer.PoC.Core.Logging;
using Viewer.PoC.Core.Reading;
using Viewer.PoC.Core.ShapeFactory;
using Viewer.PoC.Core.Transform;
using Viewer.PoC.Core.Transform.Bounding;
using Viewer.PoC.Model.Models;

namespace Viewer.PoC.Core.Services
{
    public class ShapeLoadingService(IShapeReader shapeReader, ICanvasTransform transform, IShapeProjector projector, ILogging logger) : IShapeLoadingService
    {
        public async Task<IReadOnlyList<DrawableShape>> LoadShapesAsync(string path, Dimensions canvasSize, IProgress<string> progress, ShapeBoundsService boundsService, CancellationToken token = default)
        {

            logger.Info($"Loading shapes from {path}");

            progress?.Report("Reading file...");
            var shapes = await shapeReader.ReadShapesAsync(path, token).ConfigureAwait(false);
            token.ThrowIfCancellationRequested();

            progress?.Report("Computing transform...");
            var matrix = transform.WorldToScreen(canvasSize, shapes, boundsService);
            token.ThrowIfCancellationRequested();

            progress?.Report("Projecting to screen...");
            var drawables = projector.Project(shapes, matrix).ToList();
            token.ThrowIfCancellationRequested();

            progress?.Report($"Ready ({drawables.Count} elements)");

            logger.Info($"Loaded {shapes.Count} shapes, projected to {canvasSize.Width}x{canvasSize.Height}");
            return drawables;
        }
    }
}