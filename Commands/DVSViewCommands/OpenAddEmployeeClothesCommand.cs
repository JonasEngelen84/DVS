using DVS.Stores;
using DVS.ViewModels.AddViewModels;

namespace DVS.Commands
{
    internal class OpenAddEmployeeClothesCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;

        public OpenAddEmployeeClothesCommand(ModalNavigationStore modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            AddViewModel addViewModel = new(_modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = addViewModel;
        }
    }
}
