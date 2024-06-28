using DVS.Commands.CategoryCommands;
using DVS.Stores;
using DVS.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.ViewModels.Views
{
    public class AddEditCategoryViewModel : ViewModelBase
    {
        public AddEditCategoryFormViewModel AddEditCategoryFormViewModel { get; }
        public ICommand CloseAddEditCategoryCommand { get; }

        public AddEditCategoryViewModel(
            ModalNavigationStore modalNavigationStore,
            CategoryStore categoryStore,
            SeasonStore seasonStore,
            SelectedCategoryStore selectedCategoryStore,
            SelectedSeasonStore selectedSeasonStore,
            ClothesStore clothesStore)
        {
            ICommand addCategoryCommand = new AddCategoryCommand(this, categoryStore);
            ICommand editCategoryCommand = new EditCategoryCommand(this, modalNavigationStore, selectedCategoryStore);
            ICommand deleteCategoryCommand = new DeleteCategoryCommand(this, modalNavigationStore, selectedCategoryStore);
            ICommand clearCategoryListCommand = new ClearCategoryListCommand(this, modalNavigationStore);

            AddEditCategoryFormViewModel = new AddEditCategoryFormViewModel(
                categoryStore, selectedCategoryStore, addCategoryCommand,
                editCategoryCommand, deleteCategoryCommand, clearCategoryListCommand);

            CloseAddEditCategoryCommand = new CloseAddEditCategoryCommand(
                modalNavigationStore, categoryStore, seasonStore,
                selectedCategoryStore, selectedSeasonStore, clothesStore);
        }
    }
}
