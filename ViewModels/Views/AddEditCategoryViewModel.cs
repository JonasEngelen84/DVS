using DVS.Commands.AddEditCategoryCommands;
using DVS.Stores;
using DVS.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.ViewModels.Views
{
    public class AddEditCategoryViewModel : ViewModelBase
    {
        public AddEditCategoryFormViewModel AddEditCategoryFormViewModel { get; }
        public ICommand CloseAddEditCategoryCommand { get; }

        public AddEditCategoryViewModel(ModalNavigationStore modalNavigationStore,
                                        CategoryStore categoryStore,
                                        SeasonStore seasonStore,
                                        ClothesStore clothesStore)
        {

            ICommand addCategoryCommand = new AddCategoryCommand(this, categoryStore);
            ICommand editCategoryCommand = new EditCategoryCommand(this, categoryStore);
            ICommand deleteCategoryCommand = new DeleteCategoryCommand(this, categoryStore);
            ICommand clearCategoryListCommand = new ClearCategoryListCommand(this, categoryStore);

            AddEditCategoryFormViewModel = new AddEditCategoryFormViewModel(categoryStore,
                                                                            addCategoryCommand,
                                                                            editCategoryCommand,
                                                                            deleteCategoryCommand,
                                                                            clearCategoryListCommand);

            CloseAddEditCategoryCommand = new CloseAddEditCategoryCommand(modalNavigationStore,
                                                                          categoryStore,
                                                                          seasonStore,
                                                                          clothesStore);
        }
    }
}
