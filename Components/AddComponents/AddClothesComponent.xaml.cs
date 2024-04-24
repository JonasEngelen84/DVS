using DVS.ViewModels;
using System.Windows.Controls;

namespace DVS.Components
{
    public partial class AddClothesComponent : UserControl
    {
        public AddClothesComponent()
        {
            InitializeComponent();

            DataContext = new AddClothesComponent();
        }
    }
}
