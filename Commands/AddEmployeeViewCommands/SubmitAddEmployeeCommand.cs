using DVS.Stores;
using DVS.ViewModels.AddEditViewModels;

namespace DVS.Commands.AddEmployeeViewCommands
{
    class SubmitAddEmployeeCommand(AddEmployeeViewModel addEmployeeViewModel, ModalNavigationStore modalNavigationStore) : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override void Execute(object parameter) => _modalNavigationStore.Close();
    }
}
