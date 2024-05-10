using DVS.ViewModels.AddViewModels;
using System.Windows.Controls;

namespace DVS.Components
{
    public partial class AddClothesComponent : UserControl
    {
        public AddClothesComponent()
        {
            InitializeComponent();

            DataContext = new AddClothesViewModel();
        }
    }
}
