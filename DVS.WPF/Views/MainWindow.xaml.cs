using CommunityToolkit.Mvvm.Input;
using DVS.Domain;
using DVS.Domain.Services.Interfaces;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace DVS.WPF
{
    public partial class MainWindow : Window
    {
        public ICommand SaveCommand { get; }

        public MainWindow(IDirtyEntitySaver dirtyEntitySaver)
        {
            InitializeComponent();
            SaveCommand = new RelayCommand(async () => await dirtyEntitySaver.SaveDirtyEntitiesAsync());
        }

        private void ChangeViewClick(object sender, RoutedEventArgs e)
        {
            if (SizeView.Visibility == Visibility.Visible)
            {
                SizeView.Visibility = Visibility.Hidden;
                HeadView.Visibility = Visibility.Visible;
                UserWindow.Width = 900;
                UserWindow.Height = 900;
            }
            else
            {
                SizeView.Visibility = Visibility.Visible;
                HeadView.Visibility = Visibility.Hidden;
                UserWindow.Width = 1250;
                UserWindow.Height = 900;
            }
        }

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseAppClick(object sender, RoutedEventArgs e)
        {
            if (ObservableEntity.GlobalDirtyTrackingService.HasUnsavedChanges)
            {
                SaveCommand.Execute(null);
            }

            Close();
        }

        private void MinimizeAppClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeAppClick(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;

        }

        private void ChangeMinMaxImage(object sender, EventArgs e)
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