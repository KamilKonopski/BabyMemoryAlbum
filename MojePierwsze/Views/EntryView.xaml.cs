using MojePierwsze.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace MojePierwsze.Views
{
    public partial class EntryView : UserControl
    {
        private readonly MainWindow _mainWindow;
        private readonly EntryViewModel _viewModel;

        public EntryView(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _viewModel = new EntryViewModel();
            this.DataContext = _viewModel;
        }

        public EntryView(MainWindow mainWindow, int startIndex)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _viewModel = new EntryViewModel();
            this.DataContext = _viewModel;

            if (_viewModel.HasEntries && startIndex >= 0 && startIndex < _viewModel.Entries.Count)
            {
                _viewModel.CurrentIndex = startIndex;
            }
        }

        private void AnimateEntryChange()
        {
            var sb = (System.Windows.Media.Animation.Storyboard)this.Resources["FadeInStoryboard"];
            this.BeginStoryboard(sb);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateTo(new AlbumView(_mainWindow));
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.GoNext();
            AnimateEntryChange();
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.GoPrevious();
            AnimateEntryChange();
        }

        private void AddEntry_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateTo(new AddEntryView(_mainWindow));
        }

        private void EditEntry_Click(object sender, RoutedEventArgs e)
        {
            var current = _viewModel.CurrentEntry;
            if (current == null)
            {
                MessageBox.Show("Brak wpisu do edycji.");
                return;
            }

            var editView = new AddEntryView(_mainWindow);
            editView.SetEntryToEdit(current);
            _mainWindow.NavigateTo(editView);
        }
    }
}
