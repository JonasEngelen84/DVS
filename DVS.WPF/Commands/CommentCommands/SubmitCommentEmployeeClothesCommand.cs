using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.CommentCommands
{
    public class SubmitCommentEmployeeClothesCommand(CommentEmployeeClothesViewModel commentEmployeeClothesViewModel,
                                                     EmployeeStore employeeStore,
                                                     ModalNavigationStore modalNavigationStore)
                                                     : AsyncCommandBase
    {
        private readonly CommentEmployeeClothesViewModel _commentEmployeeClothesViewModel = commentEmployeeClothesViewModel;
        private readonly EmployeeStore _employeeStore = employeeStore;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override async Task ExecuteAsync(object parameter)
        {
            CommentEmployeeClothesFormViewModel commentEmployeeClothesFormViewModel = _commentEmployeeClothesViewModel.CommentEmployeeClothesFormViewModel;
            
            commentEmployeeClothesFormViewModel.ErrorMessage = null;
            commentEmployeeClothesFormViewModel.IsSubmitting = true;

            Employee employeeToEdit = new(commentEmployeeClothesFormViewModel.Employee.GuidID,
                                          commentEmployeeClothesFormViewModel.EmployeeID,
                                          commentEmployeeClothesFormViewModel.EmployeeLastname,
                                          commentEmployeeClothesFormViewModel.EmployeeFirstname,
                                          commentEmployeeClothesFormViewModel.Employee.Comment)
            {
                EmployeeClothes = commentEmployeeClothesFormViewModel.Employee.EmployeeClothes
            };

            EmployeeClothesSize? existingItem = employeeToEdit.EmployeeClothes
                .FirstOrDefault(ecs => ecs.GuidID == commentEmployeeClothesFormViewModel.EmployeeClothesSizeGuidID);

            if (existingItem == null)
            {
                commentEmployeeClothesFormViewModel.ErrorMessage = "Bearbeiten des Kommentar ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
            }
            else
            {
                existingItem.Comment = commentEmployeeClothesFormViewModel.Comment;

                try
                {
                    await _employeeStore.Update(employeeToEdit);
                }
                catch (Exception)
                {
                    commentEmployeeClothesFormViewModel.ErrorMessage = "Bearbeiten des Kommentar ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
                }
                finally
                {
                    commentEmployeeClothesFormViewModel.IsSubmitting = false;
                    _modalNavigationStore.Close();
                }
            }
        }
    }
}
