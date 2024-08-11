using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.AddEditSeasonCommands
{
    public class CloseAddEditSeasonCommand(ModalNavigationStore modalNavigationStore,
        AddClothesViewModel addClothesViewModel, UpdateClothesViewModel editClothesViewModel) : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;
        private readonly AddClothesViewModel _addClothesViewModel = addClothesViewModel;
        private readonly UpdateClothesViewModel _editClothesViewModel = editClothesViewModel;

        public override void Execute(object parameter)
        {
            _modalNavigationStore.CurrentViewModel =
                _addClothesViewModel != null ? _addClothesViewModel : _editClothesViewModel;
        }
    }
}
