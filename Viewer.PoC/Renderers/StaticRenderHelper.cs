using System.Windows;
using Viewer.PoC.Model.Models;
using Services = Viewer.PoC.Core.Services;

namespace Viewer.PoC.UI.Renderers
{
    internal class StaticRenderHelper
    {
        internal static System.Windows.Media.Color ToMediaColor(Color c) => System.Windows.Media.Color.FromArgb(c.A, c.R, c.G, c.B);

        internal static void AttachSelection(FrameworkElement el, DrawableShape ds, Services.ISelectionService selection)
        {
            el.Tag = ds;
            el.MouseLeftButtonDown += (s, e) =>
            {
                if (ds.Domain != null)
                {
                    selection.Select(ds.Domain);
                }
                e.Handled = true;
            };
        }
    }
}