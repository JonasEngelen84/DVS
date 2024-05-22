using System.Windows.Input;

namespace DVS.ViewModels.Forms
{
    public class AddEditClothesFormViewModel : ViewModelBase
    {
        public ICommand OpenAddEditCategoriesCommand { get; }
        public ICommand OpenAddEditSeasonsCommand { get; }
        public ICommand SubmitAddEditClothesCommand { get; }
        public ICommand CloseModalCommand { get; }

        public AddEditClothesFormViewModel(ICommand openAddEditCategoriesCommand, ICommand openAddEditSeasonsCommand,
            ICommand submitAddClothesCommand, ICommand closeModalCommand)
        {
            OpenAddEditCategoriesCommand = openAddEditCategoriesCommand;
            OpenAddEditSeasonsCommand = openAddEditSeasonsCommand;
            SubmitAddEditClothesCommand = submitAddClothesCommand;
            CloseModalCommand = closeModalCommand;
        }
    }
}
