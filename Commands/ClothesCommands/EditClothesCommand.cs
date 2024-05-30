using DVS.Stores;
using DVS.ViewModels.View_ViewModels;

namespace DVS.Commands.ClothesCommands
{
    public class EditClothesCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;

        public EditClothesCommand(EditClothesViewModel editClothesViewModel, ModalNavigationStore modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter) => _modalNavigationStore.Close(); 
    }
}
