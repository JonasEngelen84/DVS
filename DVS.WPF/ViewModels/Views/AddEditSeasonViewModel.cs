using DVS.WPF.Commands.SeasonCommands;
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
                                      SizeStore sizeStore,
                                      CategoryStore categoryStore,
                                      SeasonStore seasonStore,
                                      ClothesStore clothesStore,
                                      ClothesSizeStore clothesSizeStore,
                                      EmployeeClothesSizeStore employeeClothesSizesStore,
                                      EmployeeStore employeeStore,
                                      AddClothesViewModel addClothesViewModel,
                                      EditClothesViewModel editClothesViewModel,
                                      AddEditClothesListingViewModel addEditListingViewModel)
        {
            ICommand addSeason = new AddSeasonCommand(this, seasonStore);

            ICommand updateSeason = new EditSeasonCommand(this,
                                                          sizeStore,
                                                          categoryStore,
                                                          seasonStore,
                                                          clothesStore,
                                                          clothesSizeStore,
                                                          employeeClothesSizesStore,
                                                          employeeStore);

            ICommand deleteSeason = new DeleteSeasonCommand(this,
                                                            sizeStore,
                                                            categoryStore,
                                                            seasonStore,
                                                            clothesStore,
                                                            clothesSizeStore,
                                                            employeeClothesSizesStore,
                                                            employeeStore);

            CloseAddSeason = new CloseAddEditSeasonCommand(modalNavigationStore,
                                                           addClothesViewModel,
                                                           editClothesViewModel);

            AddEditSeasonFormViewModel = new AddEditSeasonFormViewModel(addSeason,
                                                                        updateSeason,
                                                                        deleteSeason,
                                                                        addEditListingViewModel)
            {
                AddNewSeason = "Neue Saison",
                SelectedSeason = new(Guid.NewGuid(), "Saison wählen")
            };
        }
    }
}
