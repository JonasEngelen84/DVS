using DVS.Domain.Models;
using System.Windows.Input;

namespace DVS.WPF.ViewModels.Forms
{
    public class AddEditClothesFormViewModel(Clothes? clothes,
                                             ICommand submitCommand,
                                             ICommand openAddEditCategoriesCommand,
                                             ICommand openAddEditSeasonsCommand,
                                             AddEditListingViewModel addEditListingViewModel)
                                             : ViewModelBase
    {
        public AddEditListingViewModel AddEditListingViewModel { get; } = addEditListingViewModel;
        public Clothes? Clothes { get; } = clothes;
        public ICommand OpenAddEditCategories { get; } = openAddEditCategoriesCommand;
        public ICommand OpenAddEditSeasons { get; } = openAddEditSeasonsCommand;
        public ICommand SubmitClothes { get; } = submitCommand;

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

        private Category _category;
        public Category Category
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

        private Season _season;
        public Season Season
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

        public bool HasError;

        public bool CanSubmit
        {//TODO: canSubmitClothes auf true setzen wenn Größenliste verändert wird
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
    }
}
