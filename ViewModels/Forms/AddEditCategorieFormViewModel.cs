using System.Windows.Input;

namespace DVS.ViewModels.Forms
{
    public class AddEditCategorieFormViewModel : ViewModelBase
    {
        public ICommand SubmitAddCategorieCommand { get; }
        public ICommand SubmitEditCategorieCommand { get; }
        public ICommand DeleteCategorieCommand { get; }
        public ICommand ClearCategorieListCommand { get; }
        public ICommand CloseAddCategorieCommand { get; } 

        public AddEditCategorieFormViewModel(ICommand submitAddCategorieCommand, ICommand submitEditCategorieCommand,
            ICommand deleteCategorieCommand, ICommand clearCategorieListCommand, ICommand closeAddCategorieCommand)
        {
            SubmitAddCategorieCommand = submitAddCategorieCommand;
            SubmitEditCategorieCommand = submitEditCategorieCommand;
            DeleteCategorieCommand = deleteCategorieCommand;
            ClearCategorieListCommand = clearCategorieListCommand;
            CloseAddCategorieCommand = closeAddCategorieCommand;
        }
    }
}
