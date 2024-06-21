using DVS.Models;

namespace DVS.ViewModels
{
    public class DetailedClothesListingItem : ViewModelBase
    {
        public ClothesModel Clothes { get; private set; }

        public string ID { get; private set; }
        public string Name { get; private set; }
        public string Categorie { get; private set; }
        public string Season { get; private set; }
        public string Size {  get; private set; }
        public int Quantity { get; private set; }
        public string? Comment { get; private set; }

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


        public DetailedClothesListingItem(string iD,
                                          string name,
                                          string categorie,
                                          string season,
                                          string size,
                                          int quantity,
                                          string? comment)
        {
            ID = iD;
            Name = name;
            Categorie = categorie;
            Season = season;
            Size = size;
            Quantity = quantity;
            Comment = comment;
        }


        private void EditClothes()
        {
            
        }

        private void DeleteClothes()
        {
            
        }
    }

}
