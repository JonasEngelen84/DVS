using DVS.Commands.DVSViewCommands;
using DVS.Stores;
using System.Windows.Input;

namespace DVS.ViewModels.Forms
{
    public class DVSViewModel : ViewModelBase
    {
        public EmployeesClothesListViewViewModel EmployeesClothesListViewViewModel { get; }
        public ClothesListViewViewModel ClothesListViewViewModel { get; }

        public ICommand OpenFilterClothesListCommand { get; }
        public ICommand OpenFilterEmployeeListCommand { get; }
        public ICommand OpenAddEmployeeCommand { get; }
        public ICommand OpenAddClothesCommand { get; }
        public ICommand OpenEditEmployeeClothesCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand PlusCommand { get; }
        public ICommand MinusCommand { get; }

        public DVSViewModel(ModalNavigationStore _modalNavigationStore)
        {
            EmployeesClothesListViewViewModel = new();
            ClothesListViewViewModel = new();

            OpenFilterClothesListCommand = new OpenFilterClothesListCommand(_modalNavigationStore);
            OpenFilterEmployeeListCommand = new OpenFilterEmployeeListCommand(_modalNavigationStore);
            OpenAddEmployeeCommand = new OpenAddEmployeeCommand(_modalNavigationStore);
            OpenAddClothesCommand = new OpenAddClothesCommand(_modalNavigationStore);
            OpenEditEmployeeClothesCommand = new OpenEditEmployeeClothesCommand(_modalNavigationStore);
            SaveCommand = new SaveCommand(_modalNavigationStore);
            PlusCommand = new PlusCommand(_modalNavigationStore);
            MinusCommand = new MinusCommand(_modalNavigationStore);
        }
    }
}
