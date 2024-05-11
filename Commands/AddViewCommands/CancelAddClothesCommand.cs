using DVS.Stores;
using DVS.ViewModels.AddViewModels;

namespace DVS.Commands.AddViewCommands
{
    internal class CancelAddClothesCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;

        public CancelAddClothesCommand(ModalNavigationStore modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            _modalNavigationStore.Close();
        }
    }
}