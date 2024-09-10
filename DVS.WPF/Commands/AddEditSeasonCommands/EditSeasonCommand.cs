using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;
using System.Windows;

namespace DVS.WPF.Commands.AddEditSeasonCommands
{
    public class EditSeasonCommand(AddEditSeasonViewModel addEditSeasonViewModel,
                                   SizeStore sizeStore,
                                   CategoryStore categoryStore,
                                   SeasonStore seasonStore,
                                   ClothesStore clothesStore,
                                   ClothesSizeStore clothesSizeStore,
                                   EmployeeClothesSizesStore employeeClothesSizesStore,
                                   EmployeeStore employeeStore)
                                   : AsyncCommandBase
    {
        private readonly AddEditSeasonViewModel _addEditSeasonViewModel = addEditSeasonViewModel;
        private readonly SeasonStore _seasonStore = seasonStore;

        public override async Task ExecuteAsync(object parameter)
        {
            AddEditSeasonFormViewModel addEditSeasonFormViewModel = _addEditSeasonViewModel.AddEditSeasonFormViewModel;

            if (ConfirmEditSeason(addEditSeasonFormViewModel))
            {
                addEditSeasonFormViewModel.HasError = false;
                addEditSeasonFormViewModel.IsSubmitting = true;

                Season season = new(addEditSeasonFormViewModel.SelectedSeason.GuidID, addEditSeasonFormViewModel.EditSelectedSeason);

                try
                {
                    await _seasonStore.Update(season, addEditSeasonFormViewModel);
                }
                catch (Exception)
                {
                    ShowErrorMessageBox("Bearbeiten der Saison ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Saison bearbeiten");

                    addEditSeasonFormViewModel.HasError = true;
                }
                finally
                {
                    addEditSeasonFormViewModel.IsSubmitting = false;
                }
            }
        }

        private bool ConfirmEditSeason(AddEditSeasonFormViewModel addEditSeasonFormViewModel)
        {
            string messageBoxText = $"Die Saison \"{addEditSeasonFormViewModel.SelectedSeason.Name}\" und ihre Schnittstellen werden in" +
                    $"\"{addEditSeasonFormViewModel.EditSelectedSeason}\" umbenannt.\n\nUmbennen fortsetzen?";
            string caption = "Saison umbenennen";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult dialog = MessageBox.Show(messageBoxText, caption, button, icon);
            return dialog == MessageBoxResult.Yes;
        }

        private void ShowErrorMessageBox(string message, string title)
        {
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBox.Show(message, title, button, icon);
        }
    }
}
