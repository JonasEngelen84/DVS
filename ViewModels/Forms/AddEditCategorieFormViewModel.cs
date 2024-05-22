using System.Windows.Input;

namespace DVS.ViewModels.Forms
{
    public class AddEditCategorieFormViewModel : ViewModelBase
    {
        public ICommand SubmitAddCategorieCommand { get; }
        public ICommand EditCategorieCommand { get; }
        public ICommand DeleteCategorieCommand { get; }
        public ICommand ClearCategorieListCommand { get; }
        public ICommand CloseAddCategorieCommand { get; } 

        public AddEditCategorieFormViewModel(ICommand submitAddCategorieCommand, ICommand editCategorieCommand,
            ICommand deleteCategorieCommand, ICommand clearCategorieListCommand, ICommand closeAddCategorieCommand)
        {
            SubmitAddCategorieCommand = submitAddCategorieCommand;
            EditCategorieCommand = editCategorieCommand;
            DeleteCategorieCommand = deleteCategorieCommand;
            ClearCategorieListCommand = clearCategorieListCommand;
            CloseAddCategorieCommand = closeAddCategorieCommand;
    }
    }
}
