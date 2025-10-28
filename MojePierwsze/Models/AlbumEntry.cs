using System;

namespace MojePierwsze.Models
{
    public class AlbumEntry
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;
        public string Place { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PhotoPath {  get; set; } = string.Empty;
    }
}
