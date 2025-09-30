using System;
using Viewer.PoC.Model.Models;

namespace Viewer.PoC.Core.Services
{
    public class SelectionService : ISelectionService
    {
        private IShape _selected;
        public IShape Selected => _selected;
        public event EventHandler<IShape> SelectionChanged;

        public void Select(IShape shape)
        {
            if (!Equals(_selected, shape))
            {
                _selected = shape;
                SelectionChanged?.Invoke(this, shape);
            }
        }
    }
}