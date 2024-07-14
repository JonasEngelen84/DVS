using DVS.Commands.DVSHeadViewCommands;
using DVS.Models;
using DVS.Stores;
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
                    ID = _clothes?.ID;
                    Name = _clothes?.Name;
                    Category = _clothes?.Category;
                    Season = _clothes?.Season;
                    Comment = _clothes?.Comment;
                    OnPropertyChanged(nameof(Clothes));
                }
            }
        }

        private string _iD;
        public string ID
        {
            get => _iD;
            set
            {
                if (value != _iD)
                {
                    _iD = value;
                    OnPropertyChanged(nameof(ID));
                }
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        private CategoryModel _category;
        public CategoryModel Category
        {
            get => _category;
            set
            {
                if (value != _category)
                {
                    _category = value;
                    OnPropertyChanged(nameof(Category));
                }
            }
        }

        private SeasonModel _season;
        public SeasonModel Season
        {
            get => _season;
            set
            {
                if (_season != value)
                {
                    _season = value;
                    OnPropertyChanged(nameof(Season));
                }
            }
        }

        private string _comment;
        public string Comment
        {
            get => _comment;
            set
            {
                if (value != _comment)
                {
                    _comment = value;
                    OnPropertyChanged(nameof(Comment));
                }
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
                if (value != _isDeleting)
                {
                    _isDeleting = value;
                    OnPropertyChanged(nameof(IsDeleting));
                }
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
                if (value != _errorMessage)
                {
                    _errorMessage = value;
                    OnPropertyChanged(nameof(ErrorMessage));
                    OnPropertyChanged(nameof(HasErrorMessage));
                }
            }
        }

        private bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                if (value != _isExpanded)
                {
                    _isExpanded = value;
                    OnPropertyChanged(nameof(IsExpanded));
                }
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        public ICommand OpenEditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ClearSizesCommand { get; set; }
        public ICommand PrintClothesCommand { get; set; }


        public ClothesListingItemViewModel(ClothesModel clothes,
                                           ModalNavigationStore modalNavigationStore,
                                           CategoryStore categoryStore,
                                           SeasonStore seasonStore,
                                           ClothesStore clothesStore)
        {
            Clothes = clothes;

            OpenEditCommand = new OpenEditClothesCommand(clothes,
                                                         modalNavigationStore,
                                                         categoryStore,
                                                         seasonStore,
                                                         clothesStore);

            DeleteCommand = new DeleteClothesCommand();
            ClearSizesCommand = new ClearSizesCommand();
            PrintClothesCommand = new OpenPrintClothesCommand();
        }
    }
}
