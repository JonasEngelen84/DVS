using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;
using System.Collections.ObjectModel;

namespace DVS.WPF.Commands.CommentCommands
{
    public class SubmitCommentEmployeeClothesCommand(
        CommentEmployeeClothesViewModel commentEmployeeClothesViewModel,
        EmployeeStore employeeStore,
        EmployeeClothesSizeStore employeeClothesSizesStore,
        ModalNavigationStore modalNavigationStore)
        : AsyncCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            CommentEmployeeClothesFormViewModel commentEmployeeClothesFormViewModel = commentEmployeeClothesViewModel.CommentEmployeeClothesFormViewModel;
            commentEmployeeClothesFormViewModel.HasError = false;
            commentEmployeeClothesFormViewModel.IsSubmitting = true;

            //// Zu kommentierende EmployeeClothesSize speichern
            //EmployeeClothesSize employeeClothesSizeToComment = commentEmployeeClothesFormViewModel.Employee.Clothes
            //    .FirstOrDefault(ecs => ecs.GuidId == commentEmployeeClothesFormViewModel.EmployeeClothesSizeGuidID);

            //// Neues, kommentiertes, EmployeeClothesSize erstellen
            //EmployeeClothesSize commentedEmployeeClothesSize = new(employeeClothesSizeToComment.GuidId,
            //                                                       employeeClothesSizeToComment.Employee,
            //                                                       employeeClothesSizeToComment.ClothesSize,
            //                                                       employeeClothesSizeToComment.Quantity,
            //                                                       commentEmployeeClothesFormViewModel.Comment);

            // Sämtliche alten EmployeeClothesSizes aus ClothesSize-Liste und DB entfernen
            foreach (EmployeeClothesSize ecs in commentEmployeeClothesFormViewModel.Employee.Clothes)
            {
                ecs.ClothesSize.EmployeeClothesSizes.Remove(ecs);

                try
                {
                    await employeeClothesSizesStore.Delete(ecs);
                }
                catch (Exception)
                {
                    ShowErrorMessageBox("Kommentieren der Bekleidungsgröße ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Bekleidungsgröße Kommentieren");

                    commentEmployeeClothesFormViewModel.HasError = true;
                }
            }

            // Altes EmployeeClothesSize entfernen und neues einfügen
            //commentedEmployeeClothesSize.Employee.Clothes.Remove(employeeClothesSizeToComment);
            //commentedEmployeeClothesSize.Employee.Clothes.Add(commentedEmployeeClothesSize);

            // Neues Clothes mit neuer Bekleidungs-Liste erstellen
            Employee updatedEmployee = new(commentEmployeeClothesFormViewModel.EmployeeId,
                                           commentEmployeeClothesFormViewModel.EmployeeLastname,
                                           commentEmployeeClothesFormViewModel.EmployeeFirstname,
                                           commentEmployeeClothesFormViewModel.Employee.Comment)
            {
                Clothes = new ObservableCollection<EmployeeClothesSize>(commentEmployeeClothesFormViewModel.Employee.Clothes
                .Select(ecs => new EmployeeClothesSize(ecs.GuidId, ecs.Employee, ecs.ClothesSize, ecs.Quantity, ecs.Comment)))
            };

            // EmployeeClothesSize den ClothesSize-Listen hinzufügen
            foreach (EmployeeClothesSize ecs in updatedEmployee.Clothes)
            {
                ecs.ClothesSize.EmployeeClothesSizes.Add(ecs);
            }

            try
            {
                await employeeStore.Update(updatedEmployee);
            }
            catch (Exception)
            {
                ShowErrorMessageBox("Kommentieren der Bekleidungsgröße ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Bekleidungsgröße Kommentieren");

                commentEmployeeClothesFormViewModel.HasError = true;
            }
            finally
            {
                commentEmployeeClothesFormViewModel.IsSubmitting = false;
                modalNavigationStore.Close();
            }
        }
    }
}
