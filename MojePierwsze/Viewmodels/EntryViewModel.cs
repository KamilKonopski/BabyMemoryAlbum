using MojePierwsze.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MojePierwsze.ViewModels
{
    internal class EntryViewModel : INotifyPropertyChanged
    {
        private const string DataFolderName = "Data";
        private const string EntriesFileName = "albumEntries.json";

        private List<AlbumEntry> _entries = new List<AlbumEntry>();
        private int _currentIndex = 0;
        private BitmapImage _currentPhoto;

        public EntryViewModel()
        {
            LoadEntries();
            UpdateCurrentPhoto();
        }

        public List<AlbumEntry> Entries
        {
            get { return _entries; }
            set { _entries = value; OnPropertyChanged(); OnPropertyChanged(nameof(HasEntries)); }
        }

        public AlbumEntry CurrentEntry
        {
            get
            {
                if (!HasEntries) return null;
                return _entries[_currentIndex];
            }
        }

        public BitmapImage CurrentPhoto
        {
            get { return _currentPhoto; }
            private set { _currentPhoto = value; OnPropertyChanged(); }
        }

        public int CurrentIndex
        {
            get { return _currentIndex; }
            set
            {
                if (value >= 0 && value < _entries.Count)
                {
                    _currentIndex = value;
                    OnPropertyChanged(nameof(CurrentEntry));
                    OnPropertyChanged(nameof(CurrentPositionText));
                    OnPropertyChanged(nameof(IsEven));
                    OnPropertyChanged(nameof(CanGoNext));
                    OnPropertyChanged(nameof(CanGoPrevious));
                    UpdateCurrentPhoto();
                }
            }
        }

        public bool HasEntries => _entries.Count > 0;
        public bool CanGoNext => _currentIndex < _entries.Count - 1;
        public bool CanGoPrevious => _currentIndex > 0;
        public bool IsEven => (_currentIndex + 1) % 2 == 0;

        public string CurrentPositionText => HasEntries ? $"{_currentIndex + 1} / {_entries.Count}" : "Brak wpisów";

        private string DataFilePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DataFolderName, EntriesFileName);

        public void LoadEntries()
        {
            try
            {
                if (!File.Exists(DataFilePath)) return;

                string json = File.ReadAllText(DataFilePath);
                var loaded = JsonSerializer.Deserialize<List<AlbumEntry>>(json);
                if (loaded != null)
                {
                    _entries = loaded;
                    OnPropertyChanged(nameof(Entries));
                    OnPropertyChanged(nameof(HasEntries));
                    OnPropertyChanged(nameof(CurrentEntry));
                    OnPropertyChanged(nameof(CurrentPositionText));
                    OnPropertyChanged(nameof(IsEven));
                    UpdateCurrentPhoto();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd odczytu wpisów: " + ex.Message);
            }
        }

        private void UpdateCurrentPhoto()
        {
            try
            {
                if (!HasEntries)
                {
                    CurrentPhoto = null;
                    return;
                }

                string path = CurrentEntry.PhotoPath;
                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                {
                    var bmp = new BitmapImage();
                    bmp.BeginInit();
                    bmp.CacheOption = BitmapCacheOption.OnLoad;
                    bmp.UriSource = new Uri(path, UriKind.Absolute);
                    bmp.EndInit();
                    bmp.Freeze();
                    CurrentPhoto = bmp;
                }
                else
                {
                    CurrentPhoto = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd wczytywania zdjęcia: " + ex.Message);
                CurrentPhoto = null;
            }
        }

        public void GoNext()
        {
            if (CanGoNext) CurrentIndex++;
        }

        public void GoPrevious()
        {
            if (CanGoPrevious) CurrentIndex--;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}