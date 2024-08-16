using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;
using System.Windows.Controls;
using System.Windows;
using System;

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
            
            commentEmployeeClothesFormViewModel.HasError = false;
            commentEmployeeClothesFormViewModel.IsSubmitting = true;

            Employee employeeToEdit = new(commentEmployeeClothesFormViewModel.Employee.GuidID,
                                          commentEmployeeClothesFormViewModel.EmployeeID,
                                          commentEmployeeClothesFormViewModel.EmployeeLastname,
                                          commentEmployeeClothesFormViewModel.EmployeeFirstname,
                                          commentEmployeeClothesFormViewModel.Employee.Comment)
            {
                Clothes = commentEmployeeClothesFormViewModel.Employee.Clothes
            };

            EmployeeClothesSize? existingItem = employeeToEdit.Clothes
                .FirstOrDefault(ecs => ecs.GuidID == commentEmployeeClothesFormViewModel.EmployeeClothesSizeGuidID);

            if (existingItem == null)
            {
                string messageBoxText = $"Kommentieren der Bekleidungsgröße ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
                string caption = " Bekleidungsgröße kommentieren";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult dialog = MessageBox.Show(messageBoxText, caption, button, icon);

                commentEmployeeClothesFormViewModel.HasError = true;
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
