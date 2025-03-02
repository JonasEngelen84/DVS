using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.CommentCommands
{
    public class OpenCommentCommand(
        SelectedClothesSizeStore selectedClothesSizeStore,
        SelectedEmployeeClothesSizeStore selectedEmployeeClothesSizeStore,
        ModalNavigationStore modalNavigationStore,
        ClothesStore clothesStore,
        CategoryStore categoryStore,
        SeasonStore seasonStore,
        EmployeeStore employeeStore,
        ClothesSizeStore clothesSizeStore,
        EmployeeClothesSizeStore employeeClothesSizesStore,
        DVSListingViewModel dVSListingViewModel)
        : CommandBase
    {
        public override void Execute(object parameter)
        {
            //if (selectedClothesSizeStore.SelectedClothesSize != null)
            //{
            //    CommentClothesSizeViewModel commentClothesSizeViewModel = new(modalNavigationStore,
            //                                                                  clothesStore,
            //                                                                  categoryStore,
            //                                                                  seasonStore,
            //                                                                  clothesSizeStore,
            //                                                                  selectedClothesSizeStore,
            //                                                                  dVSListingViewModel);

            //    modalNavigationStore.CurrentViewModel = commentClothesSizeViewModel;
            //}
            //else if (selectedEmployeeClothesSizeStore.SelectedEmployeeClothesSize != null)
            //{
            //    CommentEmployeeClothesViewModel commentEmployeeClothesViewModel = new(modalNavigationStore,
            //                                                                          employeeStore,
            //                                                                          employeeClothesSizesStore,
            //                                                                          selectedEmployeeClothesSizeStore,
            //                                                                          dVSListingViewModel);

            //    modalNavigationStore.CurrentViewModel = commentEmployeeClothesViewModel;
            //}
            //else
            //{
            //    ShowErrorMessageBox("Bitte das gewünschte Element auswählen.", "Kommentieren");
            //}
        }
    }
}
