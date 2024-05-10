using DVS.ViewModels.AddViewModels;
using System.Windows.Controls;

namespace DVS.Components
{
    public partial class AddEmployeeComponent : UserControl
    {
        public AddEmployeeComponent()
        {
            InitializeComponent();

            DataContext = new AddEmployeeViewModel();
        }
    }
}
