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
            }
        }

        private readonly CategoryStore _categoryStore;
        private readonly SelectedCategoryStore _selectedCategoryStore;

        //TODO: Dispose Collections?
        private readonly ObservableCollection<string> _categoryCollection;
        private readonly CollectionViewSource _categoryCollectionViewSource;
        public IEnumerable<string> CategoryCollection => _categoryCollectionViewSource.View.Cast<string>();

        public ICommand AddCategoryCommand { get; }
        public ICommand EditCategoryCommand { get; }
        public ICommand DeleteCategoryCommand { get; }
        public ICommand ClearCategoryListCommand { get; }
        public ICommand CloseAddEditCategoryCommand { get; } 

        public AddEditCategoryFormViewModel(
            CategoryStore categoryStore,
            SelectedCategoryStore selectedCategoryStore,
            ICommand addCategoryCommand,
            ICommand editCategoryCommand,
            ICommand deleteCategoryCommand,
            ICommand clearCategoryListCommand,
            ICommand closeAddEditCategoryCommand)
        {
            _categoryStore = categoryStore;
            _selectedCategoryStore = selectedCategoryStore;
            AddCategoryCommand = addCategoryCommand;
            EditCategoryCommand = editCategoryCommand;
            DeleteCategoryCommand = deleteCategoryCommand;
            ClearCategoryListCommand = clearCategoryListCommand;
            CloseAddEditCategoryCommand = closeAddEditCategoryCommand;

            EditCategory = "Kategorie wählen";

            _categoryCollection = ["Sweatshirt", "Hose", "Pullover", "Kopfbedeckung", "Jacke", "Schuhwerk", "Hemd"];
            _categoryCollectionViewSource = new CollectionViewSource { Source = _categoryCollection };
            _categoryCollectionViewSource.SortDescriptions.Add(new SortDescription("", ListSortDirection.Ascending));

            _categoryStore.CategoriesLoaded += CategoryStore_CategoriesLoaded;

        }


        protected override void Dispose()
        {
            _categoryStore.CategoriesLoaded -= CategoryStore_CategoriesLoaded;

            base.Dispose();
        }


        private void CategoryStore_CategoriesLoaded()
        {
            _categoryCollection.Clear();

            foreach(string category in _categoryStore.Categories)
            {
                AddCategory(category);
            }
        }

        private void Edit_Category()
        {

        }
        
        private void AddCategory(string categorie)
        {
            _categoryCollection.Add(categorie);
            _categoryCollectionViewSource.View.Refresh();
            AddNewCategory = "";
            OnPropertyChanged(nameof(CategoryCollection));
        }

    }
}
