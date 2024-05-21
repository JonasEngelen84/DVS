using DVS.Stores;
using DVS.ViewModels.AddEditViewModels;

namespace DVS.Commands.AddCategorieViewCommands
{
    public class CloseAddCategorieCommand(ModalNavigationStore modalNavigationStore) : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override void Execute(object parameter)
        {
            AddClothesViewModel addClothesViewModel = new(_modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = addClothesViewModel;
        }
    }
}
