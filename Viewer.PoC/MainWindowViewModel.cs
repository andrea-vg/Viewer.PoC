using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Viewer.PoC.Core.Exceptions;
using Viewer.PoC.Core.Services;
using Viewer.PoC.Model.Models;
using Viewer.PoC.UI.Renderers;
using Viewer.PoC.ViewModel;

namespace Viewer.PoC.UI
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private readonly IShapeRenderer _renderer;
        private readonly MainViewModel _mainVm;
        private readonly IFileDialogService _fileDialogService;
        private CancellationTokenSource _cancellationTokenSource;

        public ObservableCollection<UIElement> VisualElements { get; } = new ObservableCollection<UIElement>();
        public SelectionViewModel SelectionVM { get; }

        [ObservableProperty]
        private string status;

        private string _currentFilePath;
        private Dimensions _canvasSize = new Dimensions(700, 350); 
        private bool _isLoading = false;

        public MainWindowViewModel(IShapeRenderer renderer, MainViewModel mainVm, SelectionViewModel selectionVm, IFileDialogService fileDialogService)
        {
            _renderer = renderer;
            _mainVm = mainVm;
            SelectionVM = selectionVm;
            _fileDialogService = fileDialogService;
        }

        [RelayCommand]
        private async Task LoadFileAsync()
        {
            var filePath = await _fileDialogService.ShowOpenFileDialogAsync();
            if (filePath != null)
            {
                _currentFilePath = filePath;
                await LoadAndRenderAsync(filePath, _canvasSize);
            }
        }

        [RelayCommand] 
        private async Task CancelAsync()
        {
            Cancel();
            await Task.CompletedTask;
        }

        public async Task LoadAndRenderAsync(string path, Dimensions canvasSize)
        {
            if (_isLoading)
            {
                return;
            }
            _isLoading = true;
            try
            {
                Cancel();
                _cancellationTokenSource = new CancellationTokenSource();
                Status = "Loading...";
                VisualElements.Clear();

                await _mainVm.LoadAsync(path, canvasSize, async drawables =>
                {
                    await Application.Current.Dispatcher.InvokeAsync(() =>
                    {
                        VisualElements.Clear();
                        foreach (var elem in _renderer.Render(drawables))
                            VisualElements.Add(elem);
                    });
                    Status = $"Loaded {drawables.Count} elements";
                }, _cancellationTokenSource.Token);
            }
            catch(ShapeParseException ex)
            {
                Status = $"Error: {ex.Message}";
            }
            catch (Exception ex)
            {
                Status = $"Error: {ex.Message}";
            }
            finally
            {
                _cancellationTokenSource = null;
                _isLoading = false;
            }
        }

        [RelayCommand]
        private async Task CanvasSizeChangedAsync(Dimensions dims)
        {
            if (_isLoading) return;
            if (_currentFilePath != null && dims.Width > 0 && dims.Height > 0)
            {
                if (_canvasSize.Width != dims.Width || _canvasSize.Height != dims.Height)
                {
                    _canvasSize = dims;
                    await RenderAsync(_canvasSize);
                }
            }
        }

        private async Task RenderAsync(Dimensions canvasSize)
        {
            try
            {
                VisualElements.Clear();

                await _mainVm.ReprojectShapes(canvasSize, async drawables =>
                {
                    Status = "Resizing...";
                    await Application.Current.Dispatcher.InvokeAsync(() =>
                    {
                        VisualElements.Clear();
                        foreach (var elem in _renderer.Render(drawables))
                            VisualElements.Add(elem);
                    });
                    Status = $"Loaded and resized {drawables.Count} elements";
                });
            }
            catch (Exception ex)
            {
                Status = $"Error: {ex.Message}";
            }
        }

        private void Cancel()
        {
            _cancellationTokenSource?.Cancel();
            Status = "Loading canceled.";
        }
    }
}