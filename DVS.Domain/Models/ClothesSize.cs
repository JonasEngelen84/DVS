using System.Collections.ObjectModel;

namespace DVS.Domain.Models
{
    public class ClothesSize : ObservableEntity
    {
        public Guid Id { get; set; }
        public string ClothesId { get; set; }
        public string Size { get; set; }

        private Clothes _clothes;
        public Clothes Clothes
        {
            get => _clothes;
            set
            {
                if ( _clothes != value)
                {
                    _clothes = value;
                }
            }
        }

        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
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
                }
            }
        }

        public ObservableCollection<EmployeeClothesSize> EmployeeClothesSizes { get; set; }

        public ClothesSize(
            Guid id,
            Clothes clothes,
            string size,
            int quantity,
            string comment)
        {
            Id = id;
            Clothes = clothes;
            ClothesId = clothes.Id;
            Size = size;
            Quantity = quantity;
            Comment = comment;

            EmployeeClothesSizes = [];
        }

        public ClothesSize() { }
    }
}
