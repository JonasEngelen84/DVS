using DVS.Stores;
using DVS.ViewModels;
using System.Windows;

namespace DVS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly SelectedClothesStore _selectedClothesStore;

        public App()
        {
            _selectedClothesStore = new SelectedClothesStore();
        }

        // Festlegen des DataContext von MainWindow
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            {
                DataContext = new DVSViewModel(_selectedClothesStore)
            };

            MainWindow.Show();

            base.OnStartup(e);
        }
    }

}
