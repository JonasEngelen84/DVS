using DVS.Domain.Models;
using DVS.WPF.Stores;
using System.Windows.Input;

namespace DVS.WPF.ViewModels.Forms
{
    public class CommentClothesSizeFormViewModel(ICommand submitComment,
                                                 SelectedDetailedClothesItemStore selectedDetailedClothesItemStore)
                                                 : ViewModelBase
    {
        private readonly SelectedDetailedClothesItemStore _selectedDetailedClothesItemStore = selectedDetailedClothesItemStore;

        private DetailedClothesListingItemViewModel SelectedDetailedClothesItem => _selectedDetailedClothesItemStore.SelectedDetailedClothesItem;

        public bool HasSelectedDetailedClothesListingItem => SelectedDetailedClothesItem != null;
        public Clothes Clothes => SelectedDetailedClothesItem.Clothes;
        public ClothesSize ClothesSize => SelectedDetailedClothesItem.ClothesSize;
        public string ID => SelectedDetailedClothesItem.Id;
        public string Name => SelectedDetailedClothesItem.Name;
        public string Category => SelectedDetailedClothesItem.Category;
        public string Season => SelectedDetailedClothesItem.Season;
        public string Size => SelectedDetailedClothesItem.Size;
        public int Quantity => (int)SelectedDetailedClothesItem.Quantity;

        private string? _comment;
        public string? Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                _comment = value;
                OnPropertyChanged(nameof(Comment));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }

        private bool _isSubmitting;
        public bool IsSubmitting
        {
            get
            {
                return _isSubmitting;
            }
            set
            {
                _isSubmitting = value;
                OnPropertyChanged(nameof(IsSubmitting));
            }
        }

        public bool CanSubmit => !Comment.Equals(ClothesSize.Comment);

        public bool HasError;

        public ICommand SubmitComment { get; } = submitComment;
    }
}
