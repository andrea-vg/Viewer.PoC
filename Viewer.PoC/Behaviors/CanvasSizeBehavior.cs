using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Input;
using Viewer.PoC.Model.Models;

namespace Viewer.PoC.UI.Behaviors
{
    public class CanvasSizeBehavior : Behavior<FrameworkElement>
    {
        public static readonly DependencyProperty CanvasSizeChangedCommandProperty =
            DependencyProperty.Register(
                nameof(CanvasSizeChangedCommand),
                typeof(ICommand),
                typeof(CanvasSizeBehavior),
                new PropertyMetadata(null));

        public ICommand CanvasSizeChangedCommand
        {
            get => (ICommand)GetValue(CanvasSizeChangedCommandProperty);
            set => SetValue(CanvasSizeChangedCommandProperty, value);
        }

        private double _lastWidth = -1;
        private double _lastHeight = -1;

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.LayoutUpdated += AssociatedObject_LayoutUpdated;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.LayoutUpdated -= AssociatedObject_LayoutUpdated;
        }

        private void AssociatedObject_LayoutUpdated(object sender, System.EventArgs e)
        {
            var width = AssociatedObject.ActualWidth;
            var height = AssociatedObject.ActualHeight;
           
            if (width > 0 && height > 0 && (width != _lastWidth || height != _lastHeight))
            {
                _lastWidth = width;
                _lastHeight = height;

                if (CanvasSizeChangedCommand?.CanExecute(null) == true)
                {
                    var dims = new Dimensions(width, height);
                    CanvasSizeChangedCommand.Execute(dims);
                }
            }
        }
    }
}