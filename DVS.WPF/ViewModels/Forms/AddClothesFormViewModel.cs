using DVS.Domain.Models;
using System.Windows.Input;

namespace DVS.WPF.ViewModels.Forms
{
    public class AddClothesFormViewModel(
        ICommand submitCommand,
        ICommand openAddEditCategoriesCommand,
        ICommand openAddEditSeasonsCommand,
        SizesCategoriesSeasonsListingViewModel SizesCategoriesSeasonsListingViewModel)
        : ViewModelBase
    {
        public SizesCategoriesSeasonsListingViewModel SizesCategoriesSeasonsListingViewModel { get; } = SizesCategoriesSeasonsListingViewModel;

        public ICommand OpenAddEditCategories { get; } = openAddEditCategoriesCommand;
        public ICommand OpenAddEditSeasons { get; } = openAddEditSeasonsCommand;
        public ICommand SubmitClothes { get; } = submitCommand;

        private string _id;
        public string Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
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

        private string _comment = "";
        public string Comment
        {
            get => _comment;
            set
            {
                if (value != _comment)
                {
                    _comment = value;
                    OnPropertyChanged(nameof(Comment));
                    OnPropertyChanged(nameof(CanSubmit));
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
                }

                OnPropertyChanged(nameof(CanSubmit));
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
                }

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
                if (value != _isSubmitting)
                {
                    _isSubmitting = value;
                    OnPropertyChanged(nameof(IsSubmitting));
                }
            }
        }

        public bool HasError;

        public bool CanSubmit
        {
            get
            {
                if (string.IsNullOrEmpty(Id) || Id == "Id" ||
                    string.IsNullOrEmpty(Name) || Name == "Name" ||
                    Category == null ||
                    Season == null)
                {
                    return false;
                }

                return true;
            }
        }
    }
}
