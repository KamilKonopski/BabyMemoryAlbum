using MojePierwsze.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MojePierwsze.Views
{
    public partial class BasicInfoView : UserControl
    {
        private readonly MainWindow _mainWindow;
        private readonly BasicInfoViewModel _viewModel;

        public BasicInfoView(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;

            _viewModel = new BasicInfoViewModel();
            this.DataContext = _viewModel;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateTo(new AlbumView(_mainWindow));
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.IsEditing = true;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SaveData();
            _viewModel.IsEditing = false;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.LoadData();
            _viewModel.IsEditing = false;
        }

        private void ChangePhoto_Click(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is BasicInfoViewModel vm && !vm.IsEditing)
                return;

            var dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "Pliki obrazów|*.jpg;*.jpeg;*.png;*.bmp";
            if (dlg.ShowDialog() == true)
            {
                string fileName = System.IO.Path.GetFileName(dlg.FileName);
                string destPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Photos", fileName);
                System.IO.File.Copy(dlg.FileName, destPath, true);
                _viewModel.PhotoFileName = fileName;
            }
        }

        private void BirthDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BirthDatePicker.SelectedDate.HasValue)
                _viewModel.BirthDate = BirthDatePicker.SelectedDate.Value;
        }
    }
}