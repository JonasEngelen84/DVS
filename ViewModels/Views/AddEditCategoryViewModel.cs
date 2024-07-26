using DVS.Commands.AddEditCategoryCommands;
using DVS.Models;
using DVS.Stores;
using DVS.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.ViewModels.Views
{
    public class AddEditCategoryViewModel : ViewModelBase
    {
        public AddEditCategoryFormViewModel AddEditCategoryFormViewModel { get; }
        public AddEditListingViewModel AddEditListingViewModel { get; }
        public ICommand CloseAddEditCategory { get; }

        public AddEditCategoryViewModel(ModalNavigationStore modalNavigationStore, CategoryStore categoryStore,
            SeasonStore seasonStore, ClothesModel clothes, AddClothesViewModel addClothesViewModel,
            EditClothesViewModel editClothesViewModel, AddEditListingViewModel addEditListingViewModel)
        {
            ICommand addCategory = new AddCategoryCommand(this, categoryStore);
            ICommand editCategory = new EditCategoryCommand(this, categoryStore);
            ICommand deleteCategory = new DeleteCategoryCommand(this, categoryStore);
            ICommand clearCategoryList = new ClearCategoryListCommand(this, categoryStore);

            AddEditListingViewModel = new(clothes, categoryStore, seasonStore);

            CloseAddEditCategory = new CloseAddEditCategoryCommand(
                modalNavigationStore, addClothesViewModel, editClothesViewModel);

            AddEditCategoryFormViewModel = new AddEditCategoryFormViewModel(addCategory,
                editCategory, deleteCategory, clearCategoryList, addEditListingViewModel)
            {
                AddNewCategory = "Neue Kategorie",
                EditSelectedCategory = "Kategorie wählen",
                SelectedCategory = new(null, "Kategorie wählen")
            };
        }
    }
}
