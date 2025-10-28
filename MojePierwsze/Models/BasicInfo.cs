using System;

namespace MojePierwsze.Models
{
    public class BasicInfo
    {
        public string Name { get; set; } = "";
        public DateTime? BirthDate { get; set; }
        public string Height { get; set; } = "";
        public string Weight { get; set; } = "";
        public string FavoriteToys { get; set; } = "";
        public string PhotoFileName { get; set; } = "";
    }
}
