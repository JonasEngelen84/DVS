using DVS.Models;
using DVS.Stores;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace DVS.ViewModels.Forms
{
    public class AddEditCategoryFormViewModel : ViewModelBase
    {
        private CategoryModel? _addNewCategory;
        public CategoryModel? AddNewCategory
        {
            get => _addNewCategory;
            set
            {
                _addNewCategory = value;
                OnPropertyChanged(nameof(AddNewCategory));
                OnPropertyChanged(nameof(CanAdd));
            }
        }
        
        private CategoryModel? _editCategory;
        public CategoryModel? EditCategory
        {
            get => _editCategory?.Name;
            set
            {
                _selectedCategoryStore.SelectedCategory = value;
                _editCategory.Name = value;
                OnPropertyChanged(nameof(EditCategory));
                OnPropertyChanged(nameof(CanEdit));
                OnPropertyChanged(nameof(CanDelete));
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
            !string.IsNullOrEmpty(AddNewCategory.Name) &&
            !AddNewCategory.Name.Equals("Neue Kategorie");

        public bool CanEdit =>
            !string.IsNullOrEmpty(EditCategory.Name) &&
            !EditCategory.Name.Equals("Kategorie wählen") &&
            _selectedCategoryStore.SelectedCategory != EditCategory;

        public bool CanDelete => _selectedCategoryStore.SelectedCategory != null;
        public bool CanDeleteAll => _categories != null;
        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        private readonly ObservableCollection<CategoryModel> _categories;
        private readonly CollectionViewSource _categoryCollectionViewSource;
        public IEnumerable<CategoryModel> Categories => _categoryCollectionViewSource.View.Cast<CategoryModel>();
        
        private readonly CategoryStore _categoryStore;
        private readonly SelectedCategoryStore _selectedCategoryStore;

        public ICommand AddCategoryCommand { get; }
        public ICommand EditCategoryCommand { get; }
        public ICommand DeleteCategoryCommand { get; }
        public ICommand ClearCategoryListCommand { get; }


        public AddEditCategoryFormViewModel(
            CategoryStore categoryStore, SelectedCategoryStore selectedCategoryStore,
            ICommand addCategoryCommand, ICommand editCategoryCommand,
            ICommand deleteCategoryCommand, ICommand clearCategoryListCommand)
        {
            _categoryStore = categoryStore;
            _selectedCategoryStore = selectedCategoryStore;
            AddCategoryCommand = addCategoryCommand;
            EditCategoryCommand = editCategoryCommand;
            DeleteCategoryCommand = deleteCategoryCommand;
            ClearCategoryListCommand = clearCategoryListCommand;

            AddNewCategory = new CategoryModel("Neue Kategorie");
            EditCategory = new CategoryModel("Kategorie wählen");

            _selectedCategoryStore.SelectedCategory = null;

            _categories = [];
            _categoryCollectionViewSource = new CollectionViewSource { Source = _categories };
            _categoryCollectionViewSource.SortDescriptions.Add(new SortDescription(nameof(CategoryModel.Name), ListSortDirection.Ascending));

            CategoryStore_CategoriesLoaded();
            _categoryStore.CategoriesLoaded += CategoryStore_CategoriesLoaded;
            _categoryStore.CategoryAdded += CategoryStore_CategoryAdded;
        }


        private void CategoryStore_CategoriesLoaded()
        {
            _categories.Clear();

            foreach (CategoryModel category in _categoryStore.Categories)
            {
                _categories.Add(category);
            }
        }

        private void CategoryStore_CategoryAdded(CategoryModel category)
        {
            AddCategory(category);
        }

        private void Edit_Category()
        {

        }
        
        private void AddCategory(CategoryModel categoryModel)
        {
            _categories.Add(categoryModel);
            _categoryCollectionViewSource.View.Refresh();
            AddNewCategory = null;
            OnPropertyChanged(nameof(Categories));
        }

        protected override void Dispose()
        {
            _categoryStore.CategoriesLoaded -= CategoryStore_CategoriesLoaded;
            _categoryStore.CategoryAdded -= CategoryStore_CategoryAdded;

            base.Dispose();
        }
    }
}
