using DVS.ViewModels;
using System.Windows.Controls;

namespace DVS.Views
{
    public partial class DVSView : UserControl
    {
        public DVSView()
        {
            InitializeComponent();

            DataContext = new DVSViewModel();
        }
    }
}
