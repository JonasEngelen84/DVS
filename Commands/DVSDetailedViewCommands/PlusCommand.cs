using DVS.Stores;

namespace DVS.Commands.DVSDetailedViewCommands
{
    public class PlusCommand : CommandBase
    {
        private readonly SelectedClothesStore _selectedClothesStore;
        private readonly SelectedEmployeeClothesStore _selectedEmployeeClothesStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public PlusCommand(
            SelectedClothesStore selectedClothesStore,
            SelectedEmployeeClothesStore selectedEmployeeClothesStore,
            ModalNavigationStore modalNavigationStore)
        {
            _selectedClothesStore = selectedClothesStore;
            _selectedEmployeeClothesStore = selectedEmployeeClothesStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {

        }
    }
}
