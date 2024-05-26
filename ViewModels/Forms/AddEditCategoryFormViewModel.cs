using System.Collections.ObjectModel;
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

        private readonly ObservableCollection<string> _categoryCollection;
        private readonly CollectionViewSource _categoryCollectionViewSource;
        public IEnumerable<string> CategoryCollection => _categoryCollection;


        public ICommand SubmitAddCategoryCommand { get; }
        public ICommand SubmitEditCategoryCommand { get; }
        public ICommand DeleteCategoryCommand { get; }
        public ICommand ClearCategoryListCommand { get; }
        public ICommand CloseAddCategoryCommand { get; } 

        public AddEditCategoryFormViewModel(ICommand submitAddCategoryCommand, ICommand submitEditCategoryCommand,
            ICommand deleteCategoryCommand, ICommand clearCategoryListCommand, ICommand closeAddCategoryCommand)
        {
            SubmitAddCategoryCommand = submitAddCategoryCommand;
            SubmitEditCategoryCommand = submitEditCategoryCommand;
            DeleteCategoryCommand = deleteCategoryCommand;
            ClearCategoryListCommand = clearCategoryListCommand;
            CloseAddCategoryCommand = closeAddCategoryCommand;
            _categoryCollection = ["Sweatshirt", "Hose", "Pullover", "Kopfbedeckung", "Jacke", "Schuhwerk", "Hemd"];

            EditCategory = "Kategorie wählen";
        }

        public void AddCategory()
        {
            _categoryCollection.Add(AddNewCategory);
            AddNewCategory = "";
        }
    }
}
