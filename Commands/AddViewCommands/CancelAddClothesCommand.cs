using DVS.Stores;

namespace DVS.Commands.AddViewCommands
{
    internal class CancelAddClothesCommand(ModalNavigationStore modalNavigationStore) : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override void Execute(object parameter) => _modalNavigationStore.Close();
    }
}