using System.Collections.ObjectModel;

namespace DVS.Domain.Models
{
    public class ClothesSize : ObservableEntity
    {
        public Guid GuidId { get; set; }
        public string ClothesId { get; set; }
        public Clothes Clothes { get; set; }
        public string Size { get; set; }

        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged();
                }
            }
        }

        private string? _comment;
        public string? Comment
        {
            get => _comment;
            set
            {
                if (_comment != value)
                {
                    _comment = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<EmployeeClothesSize> EmployeeClothesSizes { get; set; }

        public ClothesSize(
            Guid guidId,
            Clothes clothes,
            string size,
            int quantity,
            string comment)
        {
            GuidId = guidId;
            Clothes = clothes;
            ClothesId = clothes.Id;
            Size = size;
            Quantity = quantity;
            Comment = comment;

            EmployeeClothesSizes = [];
        }

        public ClothesSize() {}
    }
}
