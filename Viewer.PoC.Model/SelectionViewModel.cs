using CommunityToolkit.Mvvm.ComponentModel;
using Viewer.PoC.Core.Services;
using Viewer.PoC.Model.Models;

namespace Viewer.PoC.ViewModel
{
    public partial class SelectionViewModel : ObservableObject
    {
        private readonly ISelectionService _selection;

        [ObservableProperty]
        private IShape current;

        public SelectionViewModel(ISelectionService selection)
        {
            _selection = selection;
            _selection.SelectionChanged += (s, shape) => Current = shape;
        }
    }
}