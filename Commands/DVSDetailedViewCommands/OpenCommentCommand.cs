using DVS.Stores;
using DVS.ViewModels;
using DVS.ViewModels.Views;
using System.Windows;

namespace DVS.Commands.DVSDetailedViewCommands
{
    public class OpenCommentCommand : CommandBase
    {
        private readonly DVSListingViewModel _dVSDetailedClothesListingView;
        private readonly DVSListingViewModel _dVSDetailedEmployeesListingView;
        private readonly ModalNavigationStore _modalNavigationStore;

        public OpenCommentCommand(DVSListingViewModel dVSDetailedClothesListingView,
                                  DVSListingViewModel dVSDetailedEmployeesListingView,
                                  ModalNavigationStore modalNavigationStore)
        {
            _dVSDetailedClothesListingView = dVSDetailedClothesListingView;
            _dVSDetailedEmployeesListingView = dVSDetailedEmployeesListingView;
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
        //    AddEditCommentViewModel addEditCommentViewModel = new();

        //    if (_dVSListingViewModel.SelectedDetailedEmployeeItem != null)
        //    {


        //        _dVSListingViewModel.SelectedDetailedEmployeeItem = null;
        //        _modalNavigationStore.CurrentViewModel = AddEditCommentViewModel;
        //    }
        //    else if (_dVSListingViewModel.SelectedDetailedClothesItem != null)
        //    {


        //        _dVSListingViewModel.SelectedDetailedClothesItem = null;
        //        _modalNavigationStore.CurrentViewModel = AddEditCommentViewModel;
        //    }
        //    else
        //    {
        //        string messageBoxText = $"Es wurde kein Objekt ausgwählt!";
        //        MessageBoxButton button = MessageBoxButton.OK;
        //        MessageBoxImage icon = MessageBoxImage.Warning;
        //        _ = MessageBox.Show(messageBoxText, null, button, icon);
        //    }
        }
    }
}
