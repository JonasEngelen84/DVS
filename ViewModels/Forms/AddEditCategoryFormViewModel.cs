﻿using DVS.Stores;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace DVS.ViewModels.Forms
{
    public class AddEditCategoryFormViewModel : ViewModelBase
    {
        private string? _addNewCategory;
        public string? AddNewCategory
        {
            get => _addNewCategory;
            set
            {
                _addNewCategory = value;
                OnPropertyChanged(nameof(AddNewCategory));
            }
        }
        
        private string? _editCategory;
        public string? EditCategory
        {
            get => _editCategory;
            set
            {
                _selectedCategoryStore.SelectedCategory = value;
                _editCategory = value;
                OnPropertyChanged(nameof(EditCategory));
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

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        //TODO: CanSubmit
        //public bool CanSubmit => !string.IsNullOrEmpty(Username);

        private readonly ObservableCollection<string> _categories;
        private readonly CollectionViewSource _categoryCollectionViewSource;
        public IEnumerable<string> Categories => _categoryCollectionViewSource.View.Cast<string>();
        
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

            EditCategory = "Kategorie wählen";

            _categories = [];
            _categoryCollectionViewSource = new CollectionViewSource { Source = _categories };
            _categoryCollectionViewSource.SortDescriptions.Add(new SortDescription("", ListSortDirection.Ascending));

            CategoryStore_CategoriesLoaded();
            _categoryStore.CategoriesLoaded += CategoryStore_CategoriesLoaded;
            _categoryStore.CategoryAdded += CategoryStore_CategoryAdded;
        }


        private void CategoryStore_CategoriesLoaded()
        {
            _categories.Clear();

            foreach (string category in _categoryStore.Categories)
            {
                AddCategory(category);
            }
        }

        private void CategoryStore_CategoryAdded(string category)
        {
            AddCategory(category);
        }

        private void Edit_Category()
        {

        }
        
        private void AddCategory(string categorie)
        {
            _categories.Add(categorie);
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
