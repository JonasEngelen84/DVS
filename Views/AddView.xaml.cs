using DVS.Components;
using DVS.ViewModels;
using DVS.ViewModels.AddViewModels;
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

namespace DVS.Views
{
    public partial class AddView : UserControl
    {
        public AddView()
        {
            InitializeComponent();

            DataContext = new AddView();
        }
    }
}
