using DVS.Stores;

namespace DVS.Commands.DVSDetailedViewCommands
{
    public class PlusCommand : CommandBase
    {
        private readonly SelectedClothesStore _selectedClothesStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public PlusCommand(
            SelectedClothesStore selectedClothesStore,
            ModalNavigationStore modalNavigationStore)
        {
            _selectedClothesStore = selectedClothesStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {

        }
    }
}
