using DVS.WPF.Commands.AddEditCategoryCommands;
using DVS.WPF.Commands.AddEditClothesCommands;
using DVS.WPF.Commands.AddEditSeasonCommands;
using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.WPF.ViewModels.Views
{
    public class EditClothesViewModel : ViewModelBase
    {
        public AddEditClothesFormViewModel AddEditClothesFormViewModel { get; }
        public AddEditListingViewModel AddEditListingViewModel { get; }
        public ICommand CloseAddEditClothes { get; }


        public EditClothesViewModel(Clothes clothes,
                                      ModalNavigationStore modalNavigationStore,
                                      SizeStore sizeStore,
                                      CategoryStore categoryStore,
                                      SeasonStore seasonStore,
                                      ClothesStore clothesStore)
        {
            AddEditListingViewModel = new(clothes, sizeStore, categoryStore, seasonStore);
            CloseAddEditClothes = new CloseAddEditClothesCommand(modalNavigationStore);

            ICommand updateClothes = new EditClothesCommand(this, clothesStore , modalNavigationStore);

            ICommand openAddEditCategories = new OpenAddEditCategoriesCommand(modalNavigationStore,
                                                                              categoryStore,
                                                                              null,
                                                                              this,
                                                                              AddEditListingViewModel);

            ICommand openAddEditSeasons = new OpenAddEditSeasonsCommand(modalNavigationStore,
                                                                        seasonStore,
                                                                        null,
                                                                        this,
                                                                        AddEditListingViewModel);

            AddEditClothesFormViewModel = new(clothes,
                                              updateClothes,
                                              openAddEditCategories,
                                              openAddEditSeasons,
                                              AddEditListingViewModel)
            {
                ID = clothes.ID,
                Name = clothes.Name,
                Category = clothes.Category,
                Season = clothes.Season,
                Comment = clothes.Comment
            };
        }
    }
}
