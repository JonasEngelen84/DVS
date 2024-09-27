using System.Collections.ObjectModel;

namespace DVS.Domain.Models
{
    public class SizeModel
    {
        public Guid GuidID { get; }
        public string Size { get; private set; }
        public int Quantity { get; set;  }
        public bool IsSizeSystemEU { get; private set; }
        public bool IsSelected { get; set;  }

        public ObservableCollection<ClothesSize> ClothesSizes { get; set; }

        public SizeModel(Guid guidID, string size, bool system)
        {
            GuidID = guidID;
            Size = size;
            Quantity = 0;
            IsSizeSystemEU = system;
            IsSelected = false;

            ClothesSizes = [];
        }

        public SizeModel()
        {

        }
    }
}
