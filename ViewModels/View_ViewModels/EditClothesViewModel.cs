using DVS.Commands.AddClothesViewCommands;
using DVS.Stores;
using DVS.ViewModels.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            ICommand SubmitAddClothesCommand = new SubmitAddClothesCommand(this, _modalNavigationStore);
        }
    }
}
