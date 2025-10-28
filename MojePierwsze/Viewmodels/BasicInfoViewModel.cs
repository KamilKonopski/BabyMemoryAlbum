using MojePierwsze.Models;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows.Media.Imaging;

namespace MojePierwsze.ViewModels
{
    internal class BasicInfoViewModel : INotifyPropertyChanged
    {
        private const string DataFolderName = "Data";
        private const string PhotosFolderName = "Photos";
        private const string InfoFileName = "basicInfo.json";

        private BasicInfo _info = new BasicInfo();
        private bool _isEditing;

        public BasicInfoViewModel()
        {
            EnsureFolders();
            LoadData();
        }

        private void EnsureFolders()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string dataDir = Path.Combine(baseDir, DataFolderName);
            string photosDir = Path.Combine(dataDir, PhotosFolderName);

            if (!Directory.Exists(dataDir))
                Directory.CreateDirectory(dataDir);
            if (!Directory.Exists(photosDir))
                Directory.CreateDirectory(photosDir);
        }

        public string Name { get { return _info.Name; } set { _info.Name = value; OnPropertyChanged(); } }
        public DateTime? BirthDate { get { return _info.BirthDate; } set { _info.BirthDate = value; OnPropertyChanged(); OnPropertyChanged("AgeText"); } }
        public string Height { get { return _info.Height; } set { _info.Height = value; OnPropertyChanged(); } }
        public string Weight { get { return _info.Weight; } set { _info.Weight = value; OnPropertyChanged(); } }
        public string FavoriteToys { get { return _info.FavoriteToys; } set { _info.FavoriteToys = value; OnPropertyChanged(); } }

        public string PhotoFileName
        {
            get { return _info.PhotoFileName; }
            set { _info.PhotoFileName = value ?? ""; OnPropertyChanged(); OnPropertyChanged("Photo"); }
        }

        public BitmapImage Photo
        {
            get
            {
                if (string.IsNullOrEmpty(PhotoFileName)) return null;
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DataFolderName, PhotosFolderName, PhotoFileName);
                if (!File.Exists(path)) return null;

                try
                {
                    var bmp = new BitmapImage();
                    bmp.BeginInit();
                    bmp.CacheOption = BitmapCacheOption.OnLoad;
                    bmp.UriSource = new Uri(path, UriKind.Absolute);
                    bmp.EndInit();
                    bmp.Freeze();
                    return bmp;
                }
                catch (Exception ex) {
                    System.Windows.MessageBox.Show("Błąd podczas wczytywania zdjęcia: " + ex.Message);
                    return null;
                }
            }
        }

        public string AgeText
        {
            get
            {
                if (!BirthDate.HasValue) return "Brak danych";
                var birth = BirthDate.Value;
                var today = DateTime.Today;
                int years = today.Year - birth.Year;
                int months = today.Month - birth.Month;
                if (today.Day < birth.Day) months--;
                if (months < 0) { years--; months += 12; }
                if (years <= 0) return string.Format("{0} miesięcy", months);
                return string.Format("{0} lat, {1} mies.", years, months);
            }
        }

        public bool IsEditing { get { return _isEditing; } set { _isEditing = value; OnPropertyChanged(); } }

        private string DataFilePath { get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DataFolderName, InfoFileName); } }
        private string PhotosFolderPath { get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DataFolderName, PhotosFolderName); } }

        public void SaveData()
        {
            try
            {
                string json = JsonSerializer.Serialize(_info, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(DataFilePath, json);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Błąd zapisu danych: " + ex.Message);
            }
        }

        public void LoadData()
        {
            try
            {
                if (!File.Exists(DataFilePath)) return;
                string json = File.ReadAllText(DataFilePath);
                var info = JsonSerializer.Deserialize<BasicInfo>(json);
                if (info == null) return;
                _info = info;
                OnPropertyChanged(string.Empty);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Błąd odczytu danych: " + ex.Message);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}