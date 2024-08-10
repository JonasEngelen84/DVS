using DVS.Domain.Models;
using System.Collections.ObjectModel;

namespace DVS.EntityFramework.DTOs
{
    public class SizeModelDTO
    {
        public Guid GuidID { get; set; }
        public string? Size { get; set; }
        public int Quantity { get; set; }
        public bool IsSizeSystemEU { get; set; }
        public bool IsSelected { get; set; }

        public ObservableCollection<ClothesSize> ClothesSizes { get; set; } = [];
    }
}
