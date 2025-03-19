using DVS.Domain.Models;

namespace DVS.WPF.ViewModels.ListingItems
{
    public class EmployeeClothesSizeListingItemViewModel(ClothesSize clothesSize, Guid? employeeClothesSizeGuidId) : ViewModelBase
    {
        public ClothesSize ClothesSize { get; } = clothesSize;
        public Guid? EmployeeClothesSizeGuidId { get; } = employeeClothesSizeGuidId;
        public Guid ClothesSizeId => ClothesSize.Id;
        public string ClothesId => ClothesSize.Clothes.Id;
        public string ClothesName => ClothesSize.Clothes.Name;
        public string Size => ClothesSize.Size;

        private int _quantity;
        public int Quantity
        {
            get => _quantity;

            set
            {
                if (_quantity != value)
                    _quantity = value;
                
                OnPropertyChanged(nameof(Quantity));
            }
        }

        private string? _comment;
        public string? Comment
        {
            get => _comment;

            set
            {
                if (_comment != value)
                    _comment = value;
                
                OnPropertyChanged(nameof(Comment));
            }
        }
    }
}
