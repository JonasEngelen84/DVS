using System.Windows.Controls;
using DVS.ViewModels;

namespace DVS.Components
{
    /// <summary>
    /// Interaktionslogik für ClothesListViewComponent.xaml.
    /// Wird wie alle anderen Views durch DataContext übergangen
    /// auf die selbst angelegte Klasse "ClothesListViewViewModel".
    /// </summary>
    public partial class ClothesListViewComponent : UserControl
    {
        public ClothesListViewComponent()
        {
            InitializeComponent();

            DataContext = new ClothesListViewViewModel();
        }
    }
}
