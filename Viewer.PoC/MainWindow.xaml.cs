using System.Windows;

namespace Viewer.PoC.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowViewModel mainWindowVM)
        {
            InitializeComponent();
            DataContext = mainWindowVM;
        }
    }
}