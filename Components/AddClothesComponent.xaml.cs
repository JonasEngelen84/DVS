using DVS.Stores;
using DVS.ViewModels;
using System.Windows.Controls;

namespace DVS.Components
{
    /// <summary>
    /// Interaktionslogik für AddClothesComponent.xaml
    /// </summary>
    public partial class AddClothesComponent : UserControl
    {
        public AddClothesComponent()
        {
            InitializeComponent();

            DataContext = new AddClothesViewModel();
        }
    }
}
