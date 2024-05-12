using DVS.Stores;
using DVS.ViewModels.AddViewModels;

namespace DVS.Commands
{
    internal class OpenAddEmployeeClothesCommand(ModalNavigationStore modalNavigationStore) : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override void Execute(object parameter)
        {
            AddViewModel addViewModel = new(_modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = addViewModel;
        }
    }
}
