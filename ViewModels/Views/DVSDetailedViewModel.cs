using DVS.Commands;
using DVS.Commands.AddEditClothesCommands;
using DVS.Commands.AddEditEmployeeCommands;
using DVS.Commands.CommentCommands;
using DVS.Stores;
using System.Windows.Input;

namespace DVS.ViewModels.Views
{
    public class DVSDetailedViewModel(DVSListingViewModel dVSListingViewModel,
                                      ModalNavigationStore modalNavigationStore,
                                      CategoryStore categoryStore,
                                      SeasonStore seasonStore,
                                      ClothesStore clothesStore,
                                      EmployeeStore employeeStore) : ViewModelBase
    {
        public DVSListingViewModel DVSListingViewModel { get; } = dVSListingViewModel;

        public ICommand OpenFilterClothesList { get; } = new OpenFilterClothesListCommand(modalNavigationStore);
        public ICommand OpenFilterEmployeeList { get; } = new OpenFilterEmployeeListCommand(modalNavigationStore);
        public ICommand OpenComment { get; } = new OpenCommentCommand(dVSListingViewModel, modalNavigationStore);
        public ICommand Plus { get; } = new PlusCommand(modalNavigationStore);
        public ICommand Minus { get; } = new MinusCommand(modalNavigationStore);

        public ICommand OpenAddClothes { get; } = new OpenAddClothesCommand(
            modalNavigationStore, categoryStore, seasonStore, clothesStore);

        //public ICommand OpenEditDetailedItem { get; } = new OpenEditDetailedItemCommand(
        //    modalNavigationStore, categoryStore, seasonStore, clothesStore);

        public ICommand OpenAddEmployee { get; } = new OpenAddEmployeeCommand(
            dVSListingViewModel, employeeStore, clothesStore, modalNavigationStore);
    }
}
