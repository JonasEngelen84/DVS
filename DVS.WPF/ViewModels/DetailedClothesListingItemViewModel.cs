using DVS.Domain.Models;

namespace DVS.WPF.ViewModels
{
    public class DetailedClothesListingItemViewModel(Clothes clothes, ClothesSize? clothesSize) : ViewModelBase
    {
        public Clothes Clothes { get; private set; } = clothes;
        public string ID => Clothes.ID;
        public string Name => Clothes.Name;
        public string Category => Clothes.Category.Name;
        public string Season => Clothes.Season.Name;

        public ClothesSize? ClothesSize { get; private set; } = clothesSize;
        public Guid? ClothesSizeGuidID => ClothesSize?.GuidID ?? null;
        public string? Size => ClothesSize?.Size.Size ?? null;
        public int? Quantity => ClothesSize?.Quantity ?? null;
        public string? Comment => ClothesSize?.Comment ?? "";

        public bool HasError;

        public void Update(Clothes clothes, ClothesSize? clothesSize)
        {
            Clothes = clothes;
            
            if (clothesSize != null)
            {
                ClothesSize = clothesSize;
            }

            OnPropertyChanged(nameof(ID));
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Category));
            OnPropertyChanged(nameof(Season));
            OnPropertyChanged(nameof(Quantity));
            OnPropertyChanged(nameof(Comment));
        }
    }
}
