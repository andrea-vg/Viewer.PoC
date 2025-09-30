using System.Threading.Tasks;

namespace Viewer.PoC.Core.Services
{
    public interface IFileDialogService
    {
        Task<string> ShowOpenFileDialogAsync();
    }
}