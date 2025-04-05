using System.Windows;
using System.Windows.Controls;

namespace DVS.WPF.Components.DVSListings
{
    public partial class DVSEmployeeClothesSizeListing : UserControl
    {
        public DVSEmployeeClothesSizeListing()
        {
            InitializeComponent();
        }
        //TODO: SizesListsAutoWidth
        private void SizesListsAutoWidth(object sender, RoutedEventArgs e)
        {
            if (sender is ListView listView && listView.View is GridView gridView)
            {
                foreach (var column in gridView.Columns)
                {
                    column.Width = double.NaN; // Setzt Auto-Breite zurück
                    column.Width = column.ActualWidth; // Erzwingt Neuberechnung
                }
            }
        }
    }
}
