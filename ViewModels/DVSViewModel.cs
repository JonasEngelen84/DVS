using DVS.Commands;
using System.Windows.Input;
using DVS.Commands.DVSViewCommands;
using DVS.Components;

namespace DVS.ViewModels
{
    internal class DVSViewModel : ViewModelBase
    {
        public EmployeesClothesListViewViewModel EmployeesClothesListViewViewModel { get; }
        public ClothesListViewViewModel ClothesListViewViewModel { get; }
        public ClothesListViewComponent ClothesListViewComponent { get; }

        public ICommand FilterCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand SafeCommand { get; }
        public ICommand PrintCommand { get; }
        public ICommand PlusCommand { get; }
        public ICommand MinusCommand { get; }

        public DVSViewModel()
        {
            EmployeesClothesListViewViewModel = new EmployeesClothesListViewViewModel();
            ClothesListViewViewModel = new ClothesListViewViewModel();
            ClothesListViewComponent = new ClothesListViewComponent();
            
            FilterCommand = new FilterCommand(this);
            AddCommand = new AddCommand(this);
            EditCommand = new EditCommand(this);
            SafeCommand = new SafeCommand(this);
            PrintCommand = new PrintCommand(this);
            PlusCommand = new PlusCommand(this);
            MinusCommand = new MinusCommand(this);
        }

        public void PlusButtonClick(object sender, EventArgs e)
        {
            ClothesListViewComponent.ClothesListView.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
