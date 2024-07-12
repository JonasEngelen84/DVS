using DVS.Commands;
using DVS.Commands.AddEditClothesCommands;
using DVS.Stores;
using DVS.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.ViewModels.Views
{
    public class AddEditClothesViewModel : ViewModelBase
    {
        public AddEditClothesFormViewModel AddEditClothesFormViewModel { get; }
        public ICommand CloseModalCommand { get; }

        public AddEditClothesViewModel(ModalNavigationStore modalNavigationStore,
                                       CategoryStore categoryStore,
                                       SeasonStore seasonStore,
                                       ClothesStore clothesStore)
        {
            CloseModalCommand = new CloseModalCommand(modalNavigationStore);
            ICommand addClothesCommand = new AddClothesCommand(this, clothesStore, modalNavigationStore);
            ICommand editClothesCommand = new EditClothesCommand(this, clothesStore , modalNavigationStore);

            ICommand openAddEditCategoriesCommand = new OpenAddEditCategoriesCommand(modalNavigationStore,
                                                                                     categoryStore,
                                                                                     seasonStore,
                                                                                     clothesStore);

            ICommand openAddEditSeasonsCommand = new OpenAddEditSeasonsCommand(modalNavigationStore,
                                                                               categoryStore,
                                                                               seasonStore,
                                                                               clothesStore);

            AddEditClothesFormViewModel = new(categoryStore,
                                              seasonStore,
                                              clothesStore,
                                              openAddEditCategoriesCommand,
                                              openAddEditSeasonsCommand,
                                              addClothesCommand,
                                              editClothesCommand);
        }
    }
}
