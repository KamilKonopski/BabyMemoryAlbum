using System.Windows;
using System.Windows.Controls;

namespace MojePierwsze.Views
{
    public partial class HomeView : UserControl
    {
        private MainWindow _mainWindow;

        public HomeView(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateTo(new AlbumView(_mainWindow));
        }
    }
}
