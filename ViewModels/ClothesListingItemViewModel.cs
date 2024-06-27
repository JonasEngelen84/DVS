using DVS.Commands;
using DVS.Models;
using System.Windows.Input;

namespace DVS.ViewModels.ListViewItems
{
    public class ClothesListingItemViewModel : ViewModelBase
    {
        private ClothesModel _clothes;
        public ClothesModel Clothes
        {
            get => _clothes;
            set
            {
                if (_clothes != value)
                {
                    _clothes = value;
                    OnPropertyChanged(nameof(Clothes));
                    ID = _clothes?.ID;
                    Name = _clothes?.Name;
                    Categorie = _clothes?.Categorie;
                    Season = _clothes?.Season;
                    Comment = _clothes?.Comment;
                }
            }
        }

        private string _iD;
        public string ID
        {
            get => _iD;
            set
            {
                _iD = value;
                OnPropertyChanged(nameof(ID));
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _categorie;
        public string Categorie
        {
            get => _categorie;
            set
            {
                _categorie = value;
                OnPropertyChanged(nameof(Categorie));
            }
        }

        private string _season;
        public string Season
        {
            get => _season;
            set
            {
                _season = value;
                OnPropertyChanged(nameof(Season));
            }
        }

        private string _comment;
        public string Comment
        {
            get => _comment;
            set
            {
                _comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }

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
