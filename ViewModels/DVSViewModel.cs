using DVS.Commands;
using System.Windows.Input;
using DVS.Commands.DVSViewCommands;

namespace DVS.ViewModels
{
    internal class DVSViewModel : ViewModelBase
    {
        public EmployeesClothesListViewViewModel EmployeesDetailsViewModel { get; }
        public ClothesListViewViewModel ClothesDetailsViewModel { get; }

        public ICommand FilterCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand SafeCommand { get; }
        public ICommand PrintCommand { get; }
        public ICommand PlusCommand { get; }
        public ICommand MinusCommand { get; }

        public DVSViewModel()
        {
            EmployeesDetailsViewModel = new EmployeesClothesListViewViewModel();
            ClothesDetailsViewModel = new ClothesListViewViewModel();
            
            FilterCommand = new FilterCommand(this);
            AddCommand = new AddCommand(this);
            EditCommand = new EditCommand(this);
            SafeCommand = new SafeCommand(this);
            PrintCommand = new PrintCommand(this);
            PlusCommand = new PlusCommand(this);
            MinusCommand = new MinusCommand(this);
        }
    }
}
