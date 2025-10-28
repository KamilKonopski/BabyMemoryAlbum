using MojePierwsze.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows;

namespace MojePierwsze.ViewModels
{
    internal class AddEntryViewModel : INotifyPropertyChanged
    {
        private const string DataFolderName = "Data";
        private const string EntriesFileName = "albumEntries.json";
        private bool _isEditing = false;
        private int _editingIndex = -1;

        private AlbumEntry _entry = new AlbumEntry();

        public string Title
        {
            get { return _entry.Title; }
            set { _entry.Title = value; OnPropertyChanged(); }
        }

        public DateTime Date
        {
            get { return _entry.Date; }
            set { _entry.Date = value; OnPropertyChanged(); }
        }

        public string Place
        {
            get { return _entry.Place; }
            set { _entry.Place = value; OnPropertyChanged(); }
        }

        public string Description
        {
            get { return _entry.Description; }
            set { _entry.Description = value; OnPropertyChanged(); }
        }

        public string PhotoPath
        {
            get { return _entry.PhotoPath; }
            set { _entry.PhotoPath = value; OnPropertyChanged(); }
        }

        private string DataFilePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DataFolderName, EntriesFileName);

        public AddEntryViewModel()
        {
            EnsureFolders();
        }

        private void EnsureFolders()
        {
            string dataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DataFolderName);
            if (!Directory.Exists(dataDir))
                Directory.CreateDirectory(dataDir);
        }

        public void LoadExistingEntry(AlbumEntry entry)
        {
            _isEditing = true;

            // zapamiętujemy indeks edytowanego wpisu
            try
            {
                var entries = LoadEntries();
                _editingIndex = entries.FindIndex(e => e.Id == entry.Id);
            }
            catch { _editingIndex = -1; }

            _entry = entry;
            OnPropertyChanged(string.Empty);
        }

        private List<AlbumEntry> LoadEntries()
        {
            if (!File.Exists(DataFilePath)) return new List<AlbumEntry>();
            string json = File.ReadAllText(DataFilePath);
            return JsonSerializer.Deserialize<List<AlbumEntry>>(json) ?? new List<AlbumEntry>();
        }

        public int? SaveEntry()
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                MessageBox.Show("Tytuł wpisu nie może być pusty!");
                return null;
            }

            try
            {
                var entries = LoadEntries();

                if (_isEditing && _editingIndex >= 0 && _editingIndex < entries.Count)
                {
                    entries[_editingIndex] = _entry;
                    MessageBox.Show("Wpis został zaktualizowany!");
                }
                else
                {
                    entries.Add(_entry);
                    MessageBox.Show("Wpis został dodany!");
                    _editingIndex = entries.Count - 1;
                }

                string newJson = JsonSerializer.Serialize(entries, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(DataFilePath, newJson);

                return _editingIndex;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas zapisu wpisu: " + ex.Message);
                return null;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}