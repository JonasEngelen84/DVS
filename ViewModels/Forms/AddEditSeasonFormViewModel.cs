using System.Windows.Input;

namespace DVS.ViewModels.Forms
{
    public class AddEditSeasonFormViewModel(ICommand submitAddSeasonCommand, ICommand editSeasonCommand,
        ICommand deleteSeasonCommand, ICommand clearSeasonListCommand, ICommand closeAddSeasonCommand) : ViewModelBase
    {
        public ICommand SubmitAddSeasonCommand { get; } = submitAddSeasonCommand;
        public ICommand EditSeasonCommand { get; } = editSeasonCommand;
        public ICommand DeleteSeasonCommand { get; } = deleteSeasonCommand;
        public ICommand ClearSeasonListCommand { get; } = clearSeasonListCommand;
        public ICommand CloseAddSeasonCommand { get; } = closeAddSeasonCommand;
    }
}
