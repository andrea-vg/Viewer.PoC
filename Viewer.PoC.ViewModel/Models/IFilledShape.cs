namespace Viewer.PoC.Model.Models
{
    internal interface IFilledShape : IShape
    {
        bool Filled { get; }
    }
}