﻿using DVS.Commands;
using DVS.Commands.ClothesCommands;
using DVS.Stores;
using DVS.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.ViewModels.Views
{
    public class AddClothesViewModel : ViewModelBase
    {
        public AddClothesFormViewModel AddClothesFormViewModel { get; }

        public AddClothesViewModel(
            ModalNavigationStore modalNavigationStore,
            CategoryStore categoryStore,
            SeasonStore seasonStore,
            SelectedCategoryStore selectedCategoryStore,
            SelectedSeasonStore selectedSeasonStore,
            ClothesListViewViewModel clothesListViewViewModel)
        {
            ICommand AddClothesCommand = new AddClothesCommand(this, modalNavigationStore);
            ICommand closeModalCommand = new CloseModalCommand(modalNavigationStore);

            ICommand openAddEditCategoriesCommand = new OpenAddEditCategoriesCommand(modalNavigationStore,
                                                                                     categoryStore,
                                                                                     seasonStore,
                                                                                     selectedCategoryStore,
                                                                                     selectedSeasonStore,
                                                                                     clothesListViewViewModel);

            ICommand openAddEditSeasonsCommand = new OpenAddEditSeasonsCommand(modalNavigationStore,
                                                                               categoryStore,
                                                                               seasonStore,
                                                                               selectedCategoryStore,
                                                                               selectedSeasonStore,
                                                                               clothesListViewViewModel);

            AddClothesFormViewModel = new(categoryStore,
                                          seasonStore,
                                          clothesListViewViewModel,
                                          openAddEditCategoriesCommand,
                                          openAddEditSeasonsCommand,
                                          AddClothesCommand,
                                          closeModalCommand);
        }
    }
}