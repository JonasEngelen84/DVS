using DVS.Domain.Models;
using System.Windows.Input;

namespace DVS.WPF.ViewModels.Forms
{
    public class AddEditCategoryFormViewModel(ICommand addCategoryCommand,
                                              ICommand updateCategoryCommand,
                                              ICommand deleteCategoryCommand,
                                              AddEditListingViewModel addEditListingViewModel)
                                              : ViewModelBase
    {
        public AddEditListingViewModel AddEditListingViewModel { get; } = addEditListingViewModel;
        public ICommand AddCategory { get; } = addCategoryCommand;
        public ICommand UpdateCategory { get; } = updateCategoryCommand;
        public ICommand DeleteCategory { get; } = deleteCategoryCommand;

        private string _addNewCategory;
        public string AddNewCategory
        {
            get => _addNewCategory;
            set
            {
                _addNewCategory = value;
                OnPropertyChanged(nameof(AddNewCategory));
                OnPropertyChanged(nameof(CanAdd));
            }
        }
        
        private string _updateSelectedCategory;
        public string UpdateSelectedCategory
        {
            get => _updateSelectedCategory;
            set
            {
                _updateSelectedCategory = value;
                OnPropertyChanged(nameof(UpdateSelectedCategory));
                OnPropertyChanged(nameof(CanEdit));
            }
        }
        
        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                if (value != null)
                {
                    _selectedCategory = value;
                    UpdateSelectedCategory = new(value.Name);
                    OnPropertyChanged(nameof(SelectedCategory));
                    OnPropertyChanged(nameof(CanDelete));
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

        public bool CanAdd =>
            !string.IsNullOrEmpty(AddNewCategory) &&
            !AddNewCategory.Equals("Neue Kategorie");

        public bool CanEdit =>
            !string.IsNullOrEmpty(UpdateSelectedCategory) &&
            !SelectedCategory.Name.Equals("Kategorie wählen") &&
            !SelectedCategory.Name.Equals(UpdateSelectedCategory);

        public bool CanDelete => !SelectedCategory.Name.Equals("Kategorie wählen");
        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);
    }
}
