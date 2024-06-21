using DVS.Models;

namespace DVS.ViewModels
{
    public class DetailedClothesListingItem : ViewModelBase
    {
        public ClothesModel Clothes { get; private set; }

        public string ID => Clothes.ID;
        public string Name => Clothes.Name;
        public string Categorie => Clothes.Categorie;
        public string Season => Clothes.Season;
        public string Size {  get; private set; }
        public int Quantity { get; private set; }
        public string? Comment => Clothes.Comment;

        private bool _isDeleting;
        public bool IsDeleting
        {
            get
            {
                return _isDeleting;
            }
            set
            {
                _isDeleting = value;
                OnPropertyChanged(nameof(IsDeleting));
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

        private bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                _isExpanded = value;
                OnPropertyChanged(nameof(IsExpanded));
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);


        public DetailedClothesListingItem(ClothesModel clothes, string size, int quantity)
        {
            Clothes = clothes;
            Size = size;
            Quantity = quantity;
        }


        private void EditClothes()
        {
            
        }

        private void DeleteClothes()
        {
            
        }
    }

}
