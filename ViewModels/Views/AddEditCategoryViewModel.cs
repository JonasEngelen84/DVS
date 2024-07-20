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
        public ICommand CloseAddEditCategoryCommand { get; }

        public AddEditCategoryViewModel(ModalNavigationStore modalNavigationStore, CategoryStore categoryStore,
            SeasonStore seasonStore, ClothesModel clothes, AddClothesViewModel addClothesViewModel,
            EditClothesViewModel editClothesViewModel, AddEditListingViewModel addEditListingViewModel)
        {
            ICommand addCategoryCommand = new AddCategoryCommand(this, categoryStore);
            ICommand editCategoryCommand = new EditCategoryCommand(this, categoryStore);
            ICommand deleteCategoryCommand = new DeleteCategoryCommand(this, categoryStore);
            ICommand clearCategoryListCommand = new ClearCategoryListCommand(this, categoryStore);

            AddEditListingViewModel = new(clothes, categoryStore, seasonStore);
                
            CloseAddEditCategoryCommand = new CloseAddEditCategoryCommand(
                modalNavigationStore, addClothesViewModel, editClothesViewModel);

            AddEditCategoryFormViewModel = new AddEditCategoryFormViewModel(addCategoryCommand,
                editCategoryCommand, deleteCategoryCommand, clearCategoryListCommand, addEditListingViewModel)
            {
                AddNewCategory = "Neue Kategorie",
                EditCategory = "Kategorie wählen",
                SelectedCategory = new(null, "Kategorie wählen")
            };
        }
    }
}
