using DVS.Stores;
using DVS.ViewModels.View_ViewModels;

namespace DVS.Commands.ClothesCommands
{
    public class SubmitAddClothesCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;

        public SubmitAddClothesCommand(AddClothesViewModel addClothesViewModel, ModalNavigationStore modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter) => _modalNavigationStore.Close();
    }
}
