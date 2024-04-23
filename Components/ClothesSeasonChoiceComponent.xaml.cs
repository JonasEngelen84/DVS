using DVS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DVS.Components
{
    /// <summary>
    /// Interaktionslogik für ClothesSeasonChoiceComponent.xaml
    /// </summary>
    public partial class ClothesSeasonChoiceComponent : UserControl
    {
        public ClothesSeasonChoiceComponent()
        {
            InitializeComponent();

            DataContext = new ClothesSeasonChoiceViewModel();
        }
    }
}
