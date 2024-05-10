using DVS.ViewModels.AddViewModels;
using System.Windows.Controls;

namespace DVS.Components
{
    public partial class ClothesCategorieCheckboxComponent : UserControl
    {
        public ClothesCategorieCheckboxComponent()
        {
            InitializeComponent();

            DataContext = new ClothesCategorieCheckboxViewModel();
        }
    }
}
