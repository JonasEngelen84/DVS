using System.Windows.Input;

namespace DVS.ViewModels.Forms
{
    public class AddEditCategorieFormViewModel(ICommand submitAddCategorieCommand, ICommand editCategorieCommand,
        ICommand deleteCategorieCommand, ICommand clearCategorieListCommand, ICommand closeAddCategorieCommand) : ViewModelBase
    {
        public ICommand SubmitAddCategorieCommand { get; } = submitAddCategorieCommand;
        public ICommand EditCategorieCommand { get; } = editCategorieCommand;
        public ICommand DeleteCategorieCommand { get; } = deleteCategorieCommand;
        public ICommand ClearCategorieListCommand { get; } = clearCategorieListCommand;
        public ICommand CloseAddCategorieCommand { get; } = closeAddCategorieCommand;
    }
}
