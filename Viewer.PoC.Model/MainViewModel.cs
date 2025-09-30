using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Viewer.PoC.Core.Services;
using Viewer.PoC.Core.ShapeFactory;
using Viewer.PoC.Core.Transform;
using Viewer.PoC.Model.Models;
using Viewer.PoC.Core.Transform.Bounding;

namespace Viewer.PoC.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IShapeLoadingService _loader;
        private readonly IShapeProjector _projector;
        private readonly ICanvasTransform _canvasTransform;
        private List<IShape> _lastDomainShapes = []; 
        private readonly ShapeBoundsService _boundsService;

        public ObservableCollection<DrawableShape> DrawableShapes { get; } = [];

        [ObservableProperty]
        private string status;

        public MainViewModel(
            IShapeLoadingService loader,
            IShapeProjector projector,
            ICanvasTransform canvasTransform,
            ShapeBoundsService boundsService)
        {
            _loader = loader;
            _projector = projector;
            _canvasTransform = canvasTransform;
            _boundsService = boundsService;
        }

        public async Task LoadAsync(string path, Dimensions canvasSize, Func<IReadOnlyList<DrawableShape>, Task> renderCallback, CancellationToken cancellationToken)
        {
            var progress = new Progress<string>(s => Status = s);

            try
            {
                _lastDomainShapes = new();
                var drawables = await _loader.LoadShapesAsync(path, canvasSize, progress, _boundsService, cancellationToken).ConfigureAwait(false);

                DrawableShapes.Clear();
                foreach (var d in drawables)
                {
                    DrawableShapes.Add(d);
                }
                _lastDomainShapes = drawables.Select(d => d.Domain).Where(s => s != null).ToList();

                await renderCallback(drawables);
                Status = $"Loaded {drawables.Count} elements";
            }
            catch (OperationCanceledException)
            {
                Status = "Loading canceled.";
            }
        }

        public async Task ReprojectShapes(Dimensions newCanvasSize, Func<IReadOnlyList<DrawableShape>, Task> renderCallback)
        {
            if (_lastDomainShapes.Count == 0) return;

            var matrix = _canvasTransform.WorldToScreen(newCanvasSize, _lastDomainShapes, _boundsService);
            var drawables = _projector.Project(_lastDomainShapes, matrix).ToList();

            DrawableShapes.Clear();
            foreach (var d in drawables)
                DrawableShapes.Add(d);

            await renderCallback(drawables);
        }
    }
}