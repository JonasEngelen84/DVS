using DVS.Models;
using DVS.Stores;
using System.Windows.Input;

namespace DVS.ViewModels.Forms
{
    public class CommentClothesSizeFormViewModel : ViewModelBase
    {
        private readonly SelectedDetailedClothesItemStore _selectedDetailedClothesItemStore;

        private DetailedClothesListingItemViewModel SelectedDetailedClothesItem => _selectedDetailedClothesItemStore.SelectedDetailedClothesItem;

        public bool HasSelectedDetailedClothesListingItem => SelectedDetailedClothesItem != null;
        public ClothesModel Clothes => SelectedDetailedClothesItem.Clothes;
        public string ID => SelectedDetailedClothesItem.ID;
        public string Name => SelectedDetailedClothesItem.Name;
        public string Category => SelectedDetailedClothesItem.Category;
        public string Season => SelectedDetailedClothesItem.Season;
        public string Size => SelectedDetailedClothesItem.Size;
        public int? Quantity => SelectedDetailedClothesItem.Quantity;

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

        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        public bool CanSubmit => !string.IsNullOrEmpty(Comment)
            || Comment != Clothes.Sizes?.FirstOrDefault(s => s.Size == Size).Comment;

        public ICommand SubmitComment { get; }

        public CommentClothesSizeFormViewModel(ICommand submitComment,
            SelectedDetailedClothesItemStore selectedDetailedClothesItemStore)
        {
            _selectedDetailedClothesItemStore = selectedDetailedClothesItemStore;
            SubmitComment = submitComment;

            _selectedDetailedClothesItemStore.SelectedDetailedClothesChanged += SelectedDetailedClothesItemStore_SelectedDetailedClothesChanged;
        }

        private void SelectedDetailedClothesItemStore_SelectedDetailedClothesChanged()
        {
            OnPropertyChanged(nameof(HasSelectedDetailedClothesListingItem));
            OnPropertyChanged(nameof(ID));
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Category));
            OnPropertyChanged(nameof(Season));
            OnPropertyChanged(nameof(Size));
            OnPropertyChanged(nameof(Quantity));
            OnPropertyChanged(nameof(Comment));
        }

        protected override void Dispose()
        {
            _selectedDetailedClothesItemStore.SelectedDetailedClothesChanged -= SelectedDetailedClothesItemStore_SelectedDetailedClothesChanged;

            base.Dispose();
        }
    }
}
