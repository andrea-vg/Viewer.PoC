using System;
using Viewer.PoC.Model.Models;

namespace Viewer.PoC.Core.Services
{
    public interface ISelectionService
    {
        IShape Selected { get; }
        event EventHandler<IShape> SelectionChanged;
        void Select(IShape shape);
    }
}
