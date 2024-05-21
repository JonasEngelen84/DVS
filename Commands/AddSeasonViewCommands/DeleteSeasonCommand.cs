using DVS.Stores;
using DVS.ViewModels.View_ViewModels;

namespace DVS.Commands.AddSeasonViewCommands
{
    public class DeleteSeasonCommand(AddEditSeasonViewModel addEditSeasonViewModel, ModalNavigationStore modalNavigationStore) : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override void Execute(object parameter) => _modalNavigationStore.Close();
    }
}
