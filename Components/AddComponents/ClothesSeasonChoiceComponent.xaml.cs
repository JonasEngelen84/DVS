using DVS.ViewModels.AddViewModels;
using System.Windows.Controls;

namespace DVS.Components
{
    public partial class ClothesSeasonChoiceComponent : UserControl
    {
        public ClothesSeasonChoiceComponent()
        {
            InitializeComponent();

            DataContext = new ClothesSeasonChoiceViewModel();
        }
    }
}
