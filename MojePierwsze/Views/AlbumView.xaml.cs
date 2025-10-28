using System.Windows;
using System.Windows.Controls;

namespace MojePierwsze.Views
{
    public partial class AlbumView : UserControl
    {
        private readonly MainWindow _mainWindow;

        public AlbumView(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void AddEntry_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateTo(new AddEntryView(_mainWindow));
        }

        private void BrowseAlbum_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateTo(new EntryView(_mainWindow));
        }

        private void BasicInfo_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateTo(new BasicInfoView(_mainWindow));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateTo(new HomeView(_mainWindow));
        }
    }
}
