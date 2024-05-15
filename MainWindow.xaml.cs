using DVS.Stores;
using System.Windows;
using System.Windows.Input;

namespace DVS
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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

        // Fullscreen/normal MainWindow
        private void FullscreenAppClick(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else
                WindowState = WindowState.Maximized;
        }

        // removable MainWindow
        // 
        private bool isDragging = false;
        private Point startPoint;

        private void WindowMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            startPoint = e.GetPosition(this);
        }

        private void WindowMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
        }

        private void WindowMouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point endPoint = e.GetPosition(this);
                double dx = endPoint.X - startPoint.X;
                double dy = endPoint.Y - startPoint.Y;

                Left += dx;
                Top += dy;
            }
        }
    }
}