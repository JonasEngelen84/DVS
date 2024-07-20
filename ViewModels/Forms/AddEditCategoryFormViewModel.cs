using DVS.Models;
using System.Windows.Input;

namespace DVS.ViewModels.Forms
{
    public class AddEditCategoryFormViewModel(ICommand addCategoryCommand, ICommand editCategoryCommand,
        ICommand deleteCategoryCommand, ICommand clearCategoryListCommand,
        AddEditListingViewModel addEditListingViewModel) : ViewModelBase
    {
        public AddEditListingViewModel AddEditListingViewModel { get; } = addEditListingViewModel;
        public ICommand AddCategoryCommand { get; } = addCategoryCommand;
        public ICommand EditCategoryCommand { get; } = editCategoryCommand;
        public ICommand DeleteCategoryCommand { get; } = deleteCategoryCommand;
        public ICommand ClearCategoryListCommand { get; } = clearCategoryListCommand;

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
        
        private string _editCategory;
        public string EditCategory
        {
            get => _editCategory;
            set
            {
                _editCategory = value;
                OnPropertyChanged(nameof(EditCategory));
                OnPropertyChanged(nameof(CanEdit));
            }
        }
        
        private CategoryModel _selectedCategory;
        public CategoryModel SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                if (value != null)
                {
                    _selectedCategory = value;
                    EditCategory = new(value.Name);
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
            !string.IsNullOrEmpty(EditCategory) &&
            !SelectedCategory.Name.Equals("Kategorie wählen") &&
            !SelectedCategory.Name.Equals(EditCategory);

        public bool CanDelete => !SelectedCategory.Name.Equals("Kategorie wählen");
        public bool CanDeleteAll => !AddEditListingViewModel.Categories.IsEmpty;
        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);
    }
}
