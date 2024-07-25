using DVS.Stores;

namespace DVS.Commands.AddEditClothesCommands
{
    public class CloseAddEditClothesCommand(ModalNavigationStore modalNavigationStore) : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override void Execute(object parameter)
        {
            _modalNavigationStore.Close();
        }
    }
}
