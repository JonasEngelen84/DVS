using DVS.Stores;
using DVS.ViewModels.View_ViewModels;

namespace DVS.Commands.ClothesCommands
{
    public class OpenAddEditSeasonsCommand(ModalNavigationStore modalNavigationStore) : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override void Execute(object parameter)
        {
            AddEditSeasonViewModel addEditSeasonViewModel = new(_modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = addEditSeasonViewModel;
        }
    }
}
