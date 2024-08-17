using System.Collections.ObjectModel;

namespace DVS.Domain.Models
{
    public class SizeModel(Guid guidID, string size, bool system)
    {
        public Guid GuidID { get; } = guidID;
        public string Size { get; } = size;
        public bool IsSizeSystemEU { get; } = system;
        public int Quantity { get; set;  } = 0;
        public bool IsSelected { get; set;  } = false;

        public ObservableCollection<ClothesSize> ClothesSizes { get; set; } = [];
    }
}
