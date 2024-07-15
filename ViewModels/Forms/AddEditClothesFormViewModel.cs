using DVS.Models;
using DVS.Stores;
using System.Windows.Input;

namespace DVS.ViewModels.Forms
{
    public class AddEditClothesFormViewModel : ViewModelBase
    {
        public AddEditListingViewModel AddEditListingViewModel { get; }
        private ClothesModel? Clothes { get; }

        private string _iD;
        public string ID
        {
            get => _iD;
            set
            {
                if (_iD != value)
                {
                    _iD = value;
                    OnPropertyChanged(nameof(ID));
                    OnPropertyChanged(nameof(CanSubmit));
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
                    OnPropertyChanged(nameof(CanSubmit));
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

        private CategoryModel _category;
        public CategoryModel Category
        {
            get => _category;
            set
            {
                if (_category != value)
                {
                    _category = value;
                    OnPropertyChanged(nameof(CanSubmit));
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
                    OnPropertyChanged(nameof(CanSubmit));
                }
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
                if (value != _isSubmitting)
                {
                    _isSubmitting = value;
                    OnPropertyChanged(nameof(IsSubmitting));
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

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        public bool CanSubmit
        {
            get
            {
                if (string.IsNullOrEmpty(ID) || ID == "ID" ||
                    string.IsNullOrEmpty(Name) || Name == "Name" ||
                    Category == null ||
                    Season == null)
                {
                    return false;
                }

                if (Clothes != null)
                {
                    if (ID == Clothes.ID &&
                        Name == Clothes.Name &&
                        Name == Clothes.Name &&
                        Category == Clothes.Category &&
                        Season == Clothes.Season)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public ICommand OpenAddEditCategoriesCommand { get; }
        public ICommand OpenAddEditSeasonsCommand { get; }
        public ICommand SubmitCommand { get; }


        public AddEditClothesFormViewModel(ClothesModel clothes, ICommand submitCommand,
            ICommand openAddEditCategoriesCommand, ICommand openAddEditSeasonsCommand,
            CategoryStore categoryStore, SeasonStore seasonStore)
        {
            AddEditListingViewModel = new(clothes, categoryStore, seasonStore);
            Clothes = clothes;
            OpenAddEditCategoriesCommand = openAddEditCategoriesCommand;
            OpenAddEditSeasonsCommand = openAddEditSeasonsCommand;
            SubmitCommand = submitCommand;
        }
    }
}
