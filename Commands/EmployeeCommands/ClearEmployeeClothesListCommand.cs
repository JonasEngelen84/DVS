using DVS.Stores;

namespace DVS.Commands.EmployeeCommands
{
    class ClearEmployeeClothesListCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;

        public ClearEmployeeClothesListCommand(ModalNavigationStore modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter) => _modalNavigationStore.Close();
    }
}
