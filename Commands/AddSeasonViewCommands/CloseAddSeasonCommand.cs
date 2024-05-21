using DVS.Stores;
using DVS.ViewModels.View_ViewModels;

namespace DVS.Commands.AddSeasonViewCommands
{
    public class CloseAddSeasonCommand(ModalNavigationStore modalNavigationStore) : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override void Execute(object parameter)
        {
            AddClothesViewModel addClothesViewModel = new(_modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = addClothesViewModel;
        }
    }
}
