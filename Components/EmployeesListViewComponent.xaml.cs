using DVS.ViewModels;
using System.Windows.Controls;

namespace DVS.Components
{
    public partial class EmployeesListViewComponent : UserControl
    {
        public EmployeesListViewComponent()
        {
            InitializeComponent();

            DataContext = new EmployeesListViewViewModel();
        }
    }
}
