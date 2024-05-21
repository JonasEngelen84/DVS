using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

//TODO: Alle Clicks zu Commands
namespace DVS
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // removable MainWindow
        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        // Close MainWindow
        private void CloseAppClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // Minimize MainWindow
        private void MinimizAppClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        // Maximize/Normalize MainWindow
        private void MaximizeAppClick(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;

        }

        // Change Min/Max-Image when WindowState changed (Drag window to top screen)
        private void WindowStateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Normal) 
            {
                MaximizePNG.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Maximize.png")); 
                MaximizePNG.Width = 14;
                MaximizePNG.Height = 14;
            }
            else
            {
                MaximizePNG.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Normalize.png"));
                MaximizePNG.Width = 17;
                MaximizePNG.Height = 17;
            }

        }       
    }
}