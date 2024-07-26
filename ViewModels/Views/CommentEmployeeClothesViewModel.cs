using DVS.Commands.CommentCommands;
using DVS.Stores;
using DVS.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.ViewModels.Views
{
    public class CommentEmployeeClothesViewModel : ViewModelBase
    {
        public CommentEmployeeClothesFormViewModel CommentEmployeeClothesFormViewModel { get; }
        public ICommand CloseComment { get; }

        public CommentEmployeeClothesViewModel(ModalNavigationStore modalNavigationStore, EmployeeStore employeeStore,
            SelectedDetailedEmployeeClothesItemStore selectedDetailedEmployeeClothesItemStore)
        {
            ICommand SubmitComment = new SubmitCommentEmployeeClothesCommand(this, employeeStore, modalNavigationStore);
            CloseComment = new CloseCommentCommand(modalNavigationStore);

            //CommentEmployeeClothesFormViewModel = new(selectedDetailedEmployeeClothesItemStore, SubmitComment);
        }
    }
}
