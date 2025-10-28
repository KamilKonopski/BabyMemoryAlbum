using Microsoft.Win32;
using MojePierwsze.Models;
using MojePierwsze.ViewModels;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MojePierwsze.Views
{
    public partial class AddEntryView : UserControl
    {
        private readonly MainWindow _mainWindow;
        private readonly AddEntryViewModel _viewModel;

       public AddEntryView(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;

            _viewModel  = new AddEntryViewModel();
            this.DataContext = _viewModel;
        }

        private void ChangePhoto_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Pliki graficzne (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";

            if (dlg.ShowDialog() == true)
            {
                try
                {
                    string dataDir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
                    string photosDir = System.IO.Path.Combine(dataDir, "Photos");

                    if (!Directory.Exists(photosDir))
                        Directory.CreateDirectory(photosDir);

                    string fileName = System.IO.Path.GetFileName(dlg.FileName);
                    string destPath = System.IO.Path.Combine(photosDir, fileName);
                    File.Copy(dlg.FileName, destPath, true);

                    _viewModel.PhotoPath = destPath;

                    BitmapImage bmp = new BitmapImage(new Uri(destPath, UriKind.Absolute));
                    ((Border)sender).Child = new Image { Source = bmp, Stretch = System.Windows.Media.Stretch.UniformToFill };
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd podczas wczytywania zdjęcia: " + ex.Message);
                }
            }
        }

        private void AddEntryButton_Click(object sender, RoutedEventArgs e)
        {
            int? index = _viewModel.SaveEntry();

            if (index.HasValue)
            {
                _mainWindow.NavigateTo(new EntryView(_mainWindow, index.Value));
            }
        }

        public void SetEntryToEdit(AlbumEntry entry)
        {
            _viewModel.LoadExistingEntry(entry);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateTo(new AlbumView(_mainWindow));
        }
    }
}
