using DVS.Stores;
using DVS.ViewModels.Views;

namespace DVS.Commands.ClothesCommands
{
    public class EditClothesCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;

        public EditClothesCommand(AddEditClothesViewModel addClothesViewModel,
            ModalNavigationStore modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            _modalNavigationStore.Close();
        }
    }
}
