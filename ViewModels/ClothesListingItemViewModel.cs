using DVS.Commands;
using DVS.Models;
using System.Windows.Input;

namespace DVS.ViewModels.ListViewItems
{
    public class ClothesListingItemViewModel : ViewModelBase
    {
        public ClothesModel Clothes { get; private set; }

        public string ID => Clothes.ID;
        public string Name => Clothes.Name;
        public string Categorie => Clothes.Categorie;
        public string Season => Clothes.Season;
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

        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }


        public ClothesListingItemViewModel(ClothesModel clothes)
        {
            Clothes = clothes;

            EditCommand = new OpenEditEmployeeCommand();
            DeleteCommand = new DeleteEmployeeCommand();
        }


        
    }
}
