using DVS.Commands.ClothesCommands;
using DVS.Stores;
using DVS.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.ViewModels.View_ViewModels
{
    public class EditClothesViewModel : ViewModelBase
    {
        private AddEditClothesFormViewModel AddEditClothesFormViewModel { get; }

        public EditClothesViewModel(ModalNavigationStore _modalNavigationStore)
        {
            ICommand openAddEditCategoriesCommand = new OpenAddEditCategoriesCommand(_modalNavigationStore);
            ICommand openAddEditSeasonsCommand = new OpenAddEditSeasonsCommand(_modalNavigationStore);
            //ICommand SubmitAddClothesCommand = new SubmitAddClothesCommand(this, _modalNavigationStore);
        }
    }
}
