using DVS.WPF.Commands.CommentCommands;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.WPF.ViewModels.Views
{
    public class CommentEmployeeClothesViewModel : ViewModelBase
    {
        public CommentEmployeeClothesFormViewModel CommentEmployeeClothesFormViewModel { get; }
        public ICommand CloseComment { get; }

        public CommentEmployeeClothesViewModel(ModalNavigationStore modalNavigationStore,
                                               EmployeeStore employeeStore,
                                               EmployeeClothesSizeStore employeeClothesSizesStore,
                                               SelectedEmployeeClothesSizeStore selectedDetailedEmployeeClothesItemStore,
                                               DVSListingViewModel dVSListingViewModel)
        {
            ICommand submitComment = new SubmitCommentEmployeeClothesCommand(this, employeeStore, employeeClothesSizesStore, modalNavigationStore);
            CloseComment = new CloseCommentCommand(modalNavigationStore, dVSListingViewModel);

            CommentEmployeeClothesFormViewModel = new(submitComment, selectedDetailedEmployeeClothesItemStore)
            {
                Comment = selectedDetailedEmployeeClothesItemStore.SelectedEmployeeClothesSize.Comment
            };
        }
    }
}
