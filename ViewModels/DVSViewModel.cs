using DVS.Commands.DVSViewCommands;
using DVS.Stores;
using System.Windows.Input;

namespace DVS.ViewModels
{
    internal class DVSViewModel : ViewModelBase
    {
        public EmployeesClothesListViewViewModel EmployeesClothesListViewViewModel { get; }
        public ClothesListViewViewModel ClothesListViewViewModel { get; }

        public ICommand FilterClothesCommand { get; }
        public ICommand FilterEmployeeCommand { get; }
        public ICommand AddEmployeeCommand { get; }
        public ICommand AddClothesCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand SafeCommand { get; }
        public ICommand PlusCommand { get; }
        public ICommand MinusCommand { get; }

        public DVSViewModel(ModalNavigationStore _modalNavigationStore)
        {
            EmployeesClothesListViewViewModel = new();
            ClothesListViewViewModel = new();

            FilterClothesCommand = new OpenFilterEmployeeListCommand(this);
            FilterEmployeeCommand = new OpenFilterEmployeeListCommand(this);
            AddEmployeeCommand = new OpenAddEmployeeCommand(_modalNavigationStore);
            AddClothesCommand = new OpenAddClothesCommand(_modalNavigationStore);
            EditCommand = new OpenEditCommand(this);
            SafeCommand = new SafeCommand(this);
            PlusCommand = new PlusCommand(this);
            MinusCommand = new MinusCommand(this);
        }
    }
}
