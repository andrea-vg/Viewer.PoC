using Microsoft.Win32;
using System.Threading.Tasks;
using Viewer.PoC.Core.Services;

namespace Viewer.PoC.UI
{
    public class FileDialogService : IFileDialogService
    {
        public Task<string> ShowOpenFileDialogAsync()
        {
            var dlg = new OpenFileDialog
            {
                Filter = "JSON files|*.json;|All files|*.*"
            };
            return Task.FromResult(dlg.ShowDialog() == true ? dlg.FileName : null);
        }
    }
}