using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;
using System.Windows;

namespace DVS.WPF.Commands.AddEditSeasonCommands
{
    public class DeleteSeasonCommand(AddEditSeasonViewModel addEditSeasonViewModel,
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

            if (ConfirmDeleteSeason(addEditSeasonFormViewModel))
            {
                addEditSeasonFormViewModel.HasError = false;
                addEditSeasonFormViewModel.IsDeleting = true;

                try
                {
                    await _seasonStore.Delete((Guid)addEditSeasonFormViewModel.SelectedSeason.GuidID, addEditSeasonFormViewModel);
                }
                catch (Exception)
                {
                    ShowErrorMessageBox("Löschen der Saison ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Saison löschen");

                    addEditSeasonFormViewModel.HasError = true;
                }
                finally
                {
                    addEditSeasonFormViewModel.IsDeleting = false;
                }
            }
        }

        private bool ConfirmDeleteSeason(AddEditSeasonFormViewModel addEditSeasonFormViewModel)
        {
            string messageBoxText = $"Die Saison \"{addEditSeasonFormViewModel.SelectedSeason.Name}\"" +
                $"und ihre Schnittstellen werden gelöscht.\n\nLöschen fortsetzen?";
            string caption = "Saison löschen";
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
