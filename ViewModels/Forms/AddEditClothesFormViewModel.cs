using System.Windows.Input;

namespace DVS.ViewModels.Forms
{
    public class AddEditClothesFormViewModel(ICommand openAddEditCategoriesCommand, ICommand openAddEditSeasonsCommand,
        ICommand submitAddClothesCommand, ICommand closeModalCommand) : ViewModelBase
    {
        public ICommand OpenAddEditCategoriesCommand { get; } = openAddEditCategoriesCommand;
        public ICommand OpenAddEditSeasonsCommand { get; } = openAddEditSeasonsCommand;
        public ICommand SubmitAddClothesCommand { get; } = submitAddClothesCommand;
        public ICommand CloseModalCommand { get; } = closeModalCommand;
    }
}
