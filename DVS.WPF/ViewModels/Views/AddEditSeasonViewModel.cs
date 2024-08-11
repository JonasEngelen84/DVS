using DVS.WPF.Commands.AddEditSeasonCommands;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.WPF.ViewModels.Views
{
    public class AddEditSeasonViewModel : ViewModelBase
    {
        public AddEditSeasonFormViewModel AddEditSeasonFormViewModel { get; }
        public ICommand CloseAddSeason { get; }

        public AddEditSeasonViewModel(ModalNavigationStore modalNavigationStore,
                                      SeasonStore seasonStore,
                                      AddClothesViewModel addClothesViewModel,
                                      UpdateClothesViewModel editClothesViewModel,
                                      AddEditListingViewModel addEditListingViewModel)
        {
            ICommand addSeason = new AddSeasonCommand(this, seasonStore);
            ICommand updateSeason = new UpdateSeasonCommand(this, seasonStore);
            ICommand deleteSeason = new DeleteSeasonCommand(this, seasonStore);
            ICommand clearSeasonList = new ClearSeasonListCommand(this, seasonStore);

            CloseAddSeason = new CloseAddEditSeasonCommand(modalNavigationStore,
                                                           addClothesViewModel,
                                                           editClothesViewModel);

            AddEditSeasonFormViewModel = new AddEditSeasonFormViewModel(addSeason,
                                                                        updateSeason,
                                                                        deleteSeason,
                                                                        clearSeasonList,
                                                                        addEditListingViewModel)
            {
                AddNewSeason = "Neue Saison",
                UpdateSelectedSeason = "Saison wählen",
                SelectedSeason = new(Guid.NewGuid(), "Saison wählen")
            };
        }
    }
}
