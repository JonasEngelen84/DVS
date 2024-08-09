using DVS.WPF.Commands;
using DVS.WPF.Commands.AddEditClothesCommands;
using DVS.WPF.Commands.AddEditEmployeeCommands;
using DVS.WPF.Commands.CommentCommands;
using DVS.WPF.Stores;
using System.Windows.Input;

namespace DVS.WPF.ViewModels.Views
{
    public class DVSDetailedViewModel(DVSListingViewModel dVSListingViewModel,
                                      ModalNavigationStore modalNavigationStore,
                                      SizeStore sizeStore,
                                      CategoryStore categoryStore,
                                      SeasonStore seasonStore,
                                      ClothesStore clothesStore,
                                      EmployeeStore employeeStore,
                                      SelectedDetailedClothesItemStore _selectedDetailedClothesItemStore,
                                      SelectedDetailedEmployeeClothesItemStore _selectedDetailedEmployeeClothesItemStore)
                                      : ViewModelBase
    {
        public DVSListingViewModel DVSListingViewModel { get; } = dVSListingViewModel;

        public ICommand OpenFilterClothesList { get; } = new OpenFilterClothesListCommand(modalNavigationStore);
        public ICommand OpenFilterEmployeeList { get; } = new OpenFilterEmployeeListCommand(modalNavigationStore);
        public ICommand Plus { get; } = new PlusCommand(modalNavigationStore);
        public ICommand Minus { get; } = new MinusCommand(modalNavigationStore);

        public ICommand OpenComment { get; } = new OpenCommentCommand(_selectedDetailedClothesItemStore,
                                                                      _selectedDetailedEmployeeClothesItemStore,
                                                                      modalNavigationStore,
                                                                      clothesStore,
                                                                      employeeStore,
                                                                      dVSListingViewModel);

        public ICommand OpenAddClothes { get; } = new OpenAddClothesCommand(modalNavigationStore,
                                                                            sizeStore,
                                                                            categoryStore,
                                                                            seasonStore,
                                                                            clothesStore);

        public ICommand OpenAddEmployee { get; } = new OpenAddEmployeeCommand(dVSListingViewModel,
                                                                              employeeStore,
                                                                              clothesStore,
                                                                              modalNavigationStore);

        public ICommand OpenEditDetailedItem { get; } = new OpenEditDetailedItemCommand(_selectedDetailedClothesItemStore,
                                                                                        _selectedDetailedEmployeeClothesItemStore,
                                                                                        modalNavigationStore,
                                                                                        sizeStore,
                                                                                        clothesStore,
                                                                                        employeeStore,
                                                                                        dVSListingViewModel,
                                                                                        categoryStore,
                                                                                        seasonStore);
    }
}
