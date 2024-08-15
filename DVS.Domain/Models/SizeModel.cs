using System.Collections.ObjectModel;

namespace DVS.Domain.Models
{
    public class SizeModel(Guid guidID, string size, bool system)
    {
        public Guid GuidID { get; } = guidID;
        public string Size { get; } = size;
        public int Quantity { get; } = 0;
        public bool IsSizeSystemEU { get; } = system;
        public bool IsSelected { get; } = false;

        public ObservableCollection<ClothesSize> ClothesSizes { get; set; } = [];
    }
}
