using System.Windows.Input;

namespace DVS.ViewModels.Forms
{
    public class AddEditSeasonFormViewModel : ViewModelBase
    {
        public ICommand SubmitAddSeasonCommand { get; }
        public ICommand EditSeasonCommand { get; }
        public ICommand DeleteSeasonCommand { get; }
        public ICommand ClearSeasonListCommand { get; }
        public ICommand CloseAddSeasonCommand { get; } 

        public AddEditSeasonFormViewModel(ICommand submitAddSeasonCommand, ICommand editSeasonCommand,
            ICommand deleteSeasonCommand, ICommand clearSeasonListCommand, ICommand closeAddSeasonCommand)
        {
            SubmitAddSeasonCommand = submitAddSeasonCommand;
            EditSeasonCommand = editSeasonCommand;
            DeleteSeasonCommand = deleteSeasonCommand;
            ClearSeasonListCommand = clearSeasonListCommand;
            CloseAddSeasonCommand = closeAddSeasonCommand;
        }
    }
}
