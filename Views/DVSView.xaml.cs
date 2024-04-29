using DVS.Components;
using DVS.ViewModels;
using System.Windows.Controls;

namespace DVS.Views
{
    public partial class DVSView : UserControl
    {
        public DVSView()
        {
            InitializeComponent();

            //DataContext = new DVSViewModel();

            //ClothesListViewComponent = new ClothesListViewComponent();
        }

        //public ClothesListViewComponent ClothesListViewComponent { get; }

        //public void PlusButtonClick(object sender, EventArgs e)
        //{
        //    ClothesListViewComponent.ClothesListView.Visibility = System.Windows.Visibility.Hidden;
        //}
    }
}
