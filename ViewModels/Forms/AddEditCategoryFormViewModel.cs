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
                _selectedCategoryStore.EditedCategory = value;
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
                    _selectedCategoryStore.SelectedCategory = value;
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
        public bool CanDeleteAll => _categories.Count > 0;
        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        private readonly ObservableCollection<CategoryModel> _categories;
        private readonly CollectionViewSource _categoryCollectionViewSource;
        public ICollectionView Categories => _categoryCollectionViewSource.View;

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

            AddNewCategory = "Neue Kategorie";
            EditCategory = "Kategorie wählen";
            SelectedCategory = new("Kategorie wählen");

            _categories = [];
            _categoryCollectionViewSource = new CollectionViewSource { Source = _categories };
            _categoryCollectionViewSource.SortDescriptions.Add(new SortDescription(nameof(CategoryModel.Name), ListSortDirection.Ascending));

            CategoryStore_CategoriesLoaded();
            _categoryStore.CategoriesLoaded += CategoryStore_CategoriesLoaded;
            _categoryStore.CategoryAdded += CategoryStore_CategoryAdded;
            _categoryStore.CategoryEdited += CategoryStore_CategoryEdited;
            _categoryStore.CategoryDeleted += CategoryStore_CategoryDeleted;
            _categoryStore.AllCategoriesDeleted += CategoryStore_AllCategoriesDeleted;
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
            OnPropertyChanged(nameof(CanDeleteAll));
        }

        private void CategoryStore_CategoryEdited(CategoryModel oldCategory, string editedCategory)
        {
            var categoryToUpdate = _categories.FirstOrDefault(y => y.Name == oldCategory.Name);

            if (categoryToUpdate != null)
            {
                categoryToUpdate.Name = editedCategory;
                _categoryCollectionViewSource.View.Refresh();
                OnPropertyChanged(nameof(CanEdit));
            }
            else
            {
                throw new InvalidOperationException("Umbenennen der Kategorie nicht möglich.");
            }
        }

        private void CategoryStore_CategoryDeleted(CategoryModel category)
        {
            var categoryToDelete = _categories.FirstOrDefault(y => y.Name == category.Name);

            if (categoryToDelete != null)
            {
                _categories.Remove(categoryToDelete);
                _categoryCollectionViewSource.View.Refresh();
                SelectedCategory = new("Kategorie wählen");
                EditCategory = SelectedCategory.Name;
                OnPropertyChanged(nameof(CanDeleteAll));
            }
            else
            {
                throw new InvalidOperationException("Löschen der Kategorie nicht möglich.");
            }
        }
        
        private void CategoryStore_AllCategoriesDeleted()
        {
            if (_categories != null)
            {
                _categories.Clear();
                SelectedCategory = new("Kategorie wählen");
                EditCategory = SelectedCategory.Name;
                OnPropertyChanged(nameof(CanDeleteAll));
            }
            else
            {
                throw new InvalidOperationException("Löschen aller Kategorien nicht möglich.");
            }
        }
        
        private void AddCategory(CategoryModel newCategory)
        {
            _categories.Add(newCategory);
            _categoryCollectionViewSource.View.Refresh();
            AddNewCategory = "Neue Kategorie";
            OnPropertyChanged(nameof(Categories));
        }

        protected override void Dispose()
        {
            _categoryStore.CategoriesLoaded -= CategoryStore_CategoriesLoaded;
            _categoryStore.CategoryAdded -= CategoryStore_CategoryAdded;
            _categoryStore.CategoryEdited -= CategoryStore_CategoryEdited;
            _categoryStore.CategoryDeleted -= CategoryStore_CategoryDeleted;
            _categoryStore.AllCategoriesDeleted += CategoryStore_AllCategoriesDeleted;

            base.Dispose();
        }
    }
}
