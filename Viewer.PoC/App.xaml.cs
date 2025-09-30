using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using Viewer.PoC.Core.Logging;
using Viewer.PoC.Core.Mapping.DtoToShape;
using Viewer.PoC.Core.Mapping.IShapeToDrawableShape;
using Viewer.PoC.Core.Reading;
using Viewer.PoC.Core.Services;
using Viewer.PoC.Core.ShapeFactory;
using Viewer.PoC.Core.Transform;
using Viewer.PoC.Core.Transform.Bounding;
using Viewer.PoC.UI.Renderers;
using Viewer.PoC.ViewModel;

namespace Viewer.PoC.UI
{
    public partial class App : Application
    {
        public static ServiceProvider _services;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();
            ConfigureServices(services);
            _services = services.BuildServiceProvider();

            var mainWindow = _services.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Logging
            services.AddSingleton<ILogging, ConsoleLogger>();

            // File handling
            services.AddSingleton<IFileReader, JsonFileReader>();
            //services.AddSingleton<IFileReader, XmlFileReader>();//future example
            services.AddSingleton<IFileDialogService, FileDialogService>();

            // Factory & creators
            services.AddSingleton<IShapeCreator, LineCreator>();
            services.AddSingleton<IShapeCreator, CircleCreator>();
            services.AddSingleton<IShapeCreator, TriangleCreator>();
            services.AddSingleton<IShapeFactory, ShapeFactory>();

            // Shape reader
            services.AddSingleton<IShapeReader, ShapeReader>();

            // Canvas transform and projector strategies
            services.AddSingleton<ICanvasTransform, CanvasTransform>();
            services.AddSingleton<IShapeProjectorStrategy, LineProjector>();
            services.AddSingleton<IShapeProjectorStrategy, CircleProjector>();
            services.AddSingleton<IShapeProjectorStrategy, TriangleProjector>();
            services.AddSingleton<IShapeProjector, ShapeProjector>();

            // Loading service
            services.AddSingleton<IShapeLoadingService, ShapeLoadingService>();

            // Bounding
            services.AddSingleton<IShapeBoundsProvider, LineBoundsProvider>();
            services.AddSingleton<IShapeBoundsProvider, CircleBoundsProvider>();
            services.AddSingleton<IShapeBoundsProvider, TriangleBoundsProvider>();
            services.AddSingleton<ShapeBoundsService>();

            // Selection
            services.AddSingleton<ISelectionService, SelectionService>();

            // Rendering strategies and composite renderer
            services.AddSingleton<IShapeRenderStrategy, LineRenderer>();
            services.AddSingleton<IShapeRenderStrategy, CircleRenderer>();
            services.AddSingleton<IShapeRenderStrategy, TriangleRenderer>();
            services.AddSingleton<IShapeRenderer, WpfShapeRenderer>();

            // ViewModels
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<SelectionViewModel>();
            services.AddSingleton<MainWindowViewModel>();

            // Window
            services.AddTransient<MainWindow>();
        }
    }
}