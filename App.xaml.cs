using DVS.Stores;
using DVS.ViewModels;
using System.Windows;

namespace DVS
{
    /// <summary>
    /// App.xaml.cs wird genutzt um Konfigurationenen, bei Programmstart, festzulegen.
    /// In App.xaml werden zB. Dictionaries implementiert, welche im gesamten code genutzt weden können.
    /// </summary>
    public partial class App : Application
    {
        // Einzige Instanzen von "DVSViewModel" und "ModalNavigationStore"
        // Diese werden der MainViewModel Instanz übergeben.
        private readonly DVSViewModel _dVSViewModel;
        private readonly ModalNavigationStore _modalNavigationStore;

        public App()
        {
            _modalNavigationStore = new();
            _dVSViewModel = new(_modalNavigationStore);
        }


        // Festlegen des DataContext von MainWindow.cs auf MainViewModel.cs
        // Bei der Erstellung der einzigen Istanz von "MainViewModel", wird dieser,
        // die einzigen Instanzen von "ModalNavigationStore" und "DVSViewModel" übergeben.
        protected override void OnStartup(StartupEventArgs e)
        {

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_dVSViewModel, _modalNavigationStore)
            };

            MainWindow.Show();

            base.OnStartup(e);
        }
    }

}
