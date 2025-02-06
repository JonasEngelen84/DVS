using DVS.Domain.Models;

namespace DVS.WPF.ViewModels
{
    public class DetailedClothesListingItemViewModel : ViewModelBase
    {
        public Clothes Clothes { get; private set; }
        public string Id => Clothes.Id;
        public string Name => Clothes.Name;
        public string Category => Clothes.Category.Name;
        public string Season => Clothes.Season.Name;

        public ClothesSize ClothesSize { get; private set; }
        public Guid? ClothesSizeGuidId => ClothesSize?.GuidId ?? null;
        public string? Size => ClothesSize?.Size.Size ?? null;
        public string? Comment => ClothesSize?.Comment ?? "";

        private int _quantity;
        public int Quantity
        {
            get => _quantity;

            set
            {
                _quantity = value;

                OnPropertyChanged(nameof(Quantity));
            }
        }

        public DetailedClothesListingItemViewModel(Clothes clothes, ClothesSize? clothesSize)
        {
            Clothes = clothes;
            ClothesSize = clothesSize;
            _quantity = ClothesSize.Quantity;
        }

        public void Update(Clothes clothes, ClothesSize? clothesSize)
        {
            Clothes = clothes;
            
            if (clothesSize != null)
            {
                ClothesSize = clothesSize;
                _quantity = clothesSize.Quantity;
            }

            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Category));
            OnPropertyChanged(nameof(Season));
            OnPropertyChanged(nameof(Quantity));
            OnPropertyChanged(nameof(Comment));
        }
    }
}
