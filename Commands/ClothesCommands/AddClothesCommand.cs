using DVS.Stores;
using DVS.ViewModels.View_ViewModels;

namespace DVS.Commands.ClothesCommands
{
    public class AddClothesCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;

        public AddClothesCommand(AddClothesViewModel addClothesViewModel, ModalNavigationStore modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter) => _modalNavigationStore.Close();
    }
}
