using DVS.Stores;
using DVS.ViewModels;
using System.Windows;

namespace DVS
{
    public partial class App : Application
    {
        // Einzige Instanzen von "ModalNavigationStore" und "DVSViewModel"
        // Werden der MainViewModel Instanz übergeben.
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly DVSViewModel _dVSViewModel;

        public App()
        {
            _modalNavigationStore = new();
            _dVSViewModel = new();
        }


        // Festlegen des DataContext von MainWindow.cs auf MainViewModel.cs
        // Bei der Erstellung der einzigen Istanz von "MainViewModel", wird dieser,
        // die einzigen Instanzen von "ModalNavigationStore" und "DVSViewModel" übergeben.
        protected override void OnStartup(StartupEventArgs e)
        {

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_modalNavigationStore, _dVSViewModel)
            };

            MainWindow.Show();

            base.OnStartup(e);
        }
    }

}
