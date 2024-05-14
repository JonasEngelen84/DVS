using DVS.Commands;
using System.Windows.Input;
using DVS.Commands.DVSViewCommands;
using DVS.Components;
using System.Windows;
using DVS.Stores;

namespace DVS.ViewModels
{
    internal class DVSViewModel : ViewModelBase
    {
        public EmployeesClothesListViewViewModel EmployeesClothesListViewViewModel { get; }
        public ClothesListViewViewModel ClothesListViewViewModel { get; }

        public ICommand FilterCommand { get; }
        public ICommand AddEmployeeCommand { get; }
        public ICommand AddClothesCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand SafeCommand { get; }
        public ICommand PrintCommand { get; }
        public ICommand PlusCommand { get; }
        public ICommand MinusCommand { get; }

        public DVSViewModel(ModalNavigationStore _modalNavigationStore)
        {
            EmployeesClothesListViewViewModel = new();
            ClothesListViewViewModel = new();

            FilterCommand = new FilterCommand(this);
            AddEmployeeCommand = new OpenAddEmployeeCommand(_modalNavigationStore);
            AddClothesCommand = new OpenAddClothesCommand(_modalNavigationStore);
            EditCommand = new EditCommand(this);
            SafeCommand = new SafeCommand(this);
            PrintCommand = new PrintCommand(this);
            PlusCommand = new PlusCommand(this);
            MinusCommand = new MinusCommand(this);
        }
    }
}
