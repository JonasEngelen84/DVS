using DVS.Commands.DVSViewCommands;
using DVS.Stores;
using System.Windows.Input;

namespace DVS.ViewModels
{
    public class DVSViewModel(ModalNavigationStore _modalNavigationStore) : ViewModelBase
    {
        public EmployeesClothesListViewViewModel EmployeesClothesListViewViewModel { get; } = new();
        public ClothesListViewViewModel ClothesListViewViewModel { get; } = new();

        public ICommand OpenFilterClothesListCommand { get; } = new OpenFilterClothesListCommand(_modalNavigationStore);
        public ICommand OpenFilterEmployeeListCommand { get; } = new OpenFilterEmployeeListCommand(_modalNavigationStore);
        public ICommand OpenAddEmployeeCommand { get; } = new OpenAddEmployeeCommand(_modalNavigationStore);
        public ICommand OpenAddClothesCommand { get; } = new OpenAddClothesCommand(_modalNavigationStore);
        public ICommand OpenEditEmployeeClothesCommand { get; } = new OpenEditEmployeeClothesCommand(_modalNavigationStore);
        public ICommand SaveCommand { get; } = new SaveCommand();
        public ICommand PlusCommand { get; } = new PlusCommand();
        public ICommand MinusCommand { get; } = new MinusCommand();
    }
}
