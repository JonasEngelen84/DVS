using DVS.Components;
using DVS.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DVS.Views
{
    public partial class DVSView : UserControl
    {
        public DVSView()
        {
            InitializeComponent();
            DataContext = new DVSViewModel();
        }

        public void PlusButtonClick(object sender, RoutedEventArgs e)
        {
            MinusButton.Visibility = Visibility.Hidden;
        }
    }
}
