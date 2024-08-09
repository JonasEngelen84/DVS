using DVS.Domain.Models;
using System.Linq;

namespace DVS.WPF.ViewModels
{
    public class DetailedClothesListingItemViewModel(Clothes clothes, Guid? clothesSizeGuidID) : ViewModelBase
    {
        public Clothes Clothes { get; private set; } = clothes;
        public string ID => Clothes.ID;
        public string Name => Clothes.Name;
        public string Category => Clothes.Category.Name;
        public string Season => Clothes.Season.Name;

        public Guid? ClothesSizeGuidID { get; private set; } = clothesSizeGuidID;
        public string? Size => Clothes.Sizes.FirstOrDefault(y => y.GuidID == ClothesSizeGuidID)?.Size.Size ?? null;
        public int? Quantity => Clothes.Sizes.FirstOrDefault(y => y.GuidID == ClothesSizeGuidID)?.Size.Quantity ?? 0;
        public string? Comment => Clothes.Sizes.FirstOrDefault(y => y.GuidID == ClothesSizeGuidID)?.Comment ?? null;

        private bool _isDeleting;
        public bool IsDeleting
        {
            get => _isDeleting;
            set
            {
                if (_isDeleting != value)
                {
                    _isDeleting = value;
                    OnPropertyChanged(nameof(IsDeleting));
                }
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                if (value != _errorMessage)
                {
                    _errorMessage = value;
                    OnPropertyChanged(nameof(ErrorMessage));
                    OnPropertyChanged(nameof(HasErrorMessage));
                }
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        public void Update(Clothes clothes, Guid? clothesSizeGuidID)
        {
            Clothes = clothes;
            ClothesSizeGuidID = clothesSizeGuidID;

            OnPropertyChanged(nameof(ID));
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Category));
            OnPropertyChanged(nameof(Season));
            OnPropertyChanged(nameof(Quantity));
            OnPropertyChanged(nameof(Comment));
        }
    }
}
