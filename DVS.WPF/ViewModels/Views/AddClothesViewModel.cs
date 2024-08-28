﻿using DVS.WPF.Commands.AddEditCategoryCommands;
using DVS.WPF.Commands.AddEditClothesCommands;
using DVS.WPF.Commands.AddEditSeasonCommands;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.WPF.ViewModels.Views
{
    public class AddClothesViewModel : ViewModelBase
    {
        public AddEditClothesFormViewModel AddEditClothesFormViewModel { get; }
        public AddEditClothesListingViewModel AddEditListingViewModel { get; }
        public ICommand CloseAddEditClothes { get; }

        public AddClothesViewModel(ModalNavigationStore modalNavigationStore,
                                   SizeStore sizeStore,
                                   CategoryStore categoryStore,
                                   SeasonStore seasonStore,
                                   ClothesStore clothesStore)
        {
            AddEditListingViewModel = new(null, sizeStore, categoryStore, seasonStore);
            CloseAddEditClothes = new CloseAddEditClothesCommand(modalNavigationStore);

            ICommand addClothes = new AddClothesCommand(this,
                                                        clothesStore,
                                                        sizeStore,
                                                        categoryStore,
                                                        seasonStore,
                                                        modalNavigationStore);

            ICommand openAddEditCategories = new OpenAddEditCategoriesCommand(modalNavigationStore,
                                                                              categoryStore,
                                                                              this,
                                                                              null,
                                                                              AddEditListingViewModel);

            ICommand openAddEditSeasons = new OpenAddEditSeasonsCommand(modalNavigationStore,
                                                                        seasonStore,
                                                                        this,
                                                                        null,
                                                                        AddEditListingViewModel);

            AddEditClothesFormViewModel = new(null,
                                              addClothes,
                                              openAddEditCategories,
                                              openAddEditSeasons,
                                              AddEditListingViewModel)
            {
                ID = "ID",
                Name = "Name",
                Comment = "Kommentar"
            };
        }
    }
}
