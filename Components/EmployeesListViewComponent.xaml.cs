using DVS.ViewModels;
using System.Windows.Controls;

namespace DVS.Components
{
    /// <summary>
    /// Interaktionslogik für EmployeesListViewComponent.xaml
    /// </summary>
    public partial class EmployeesListViewComponent : UserControl
    {
        public EmployeesListViewComponent()
        {
            InitializeComponent();

            DataContext = new EmployeesListViewViewModel();
        }
    }
}
