using DVS.ViewModels;
using System.Windows;

namespace DVS
{
    public partial class App : Application
    {
        // Festlegen des DataContext von MainWindow
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            {
                DataContext = new DVSViewModel()
            };

            MainWindow.Show();

            base.OnStartup(e);
        }
    }

}
