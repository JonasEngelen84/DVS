using System;
using System.Collections.Generic;
using DVS.Commands;
using System.Windows.Input;
using System.Windows;
using DVS.Views;
using DVS.Components;

namespace DVS.ViewModels
{
    internal class DVSViewModel : ViewModelBase
    {
        public EmployeesListViewViewModel EmployeesDetailsViewModel { get; }
        public ClothesListViewViewModel ClothesDetailsViewModel { get; }

        public ICommand FilterCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand SafeCommand { get; }
        public ICommand PrintCommand { get; }
        public ICommand PlusCommand { get; }
        public ICommand MinusCommand { get; }

        public DVSViewModel()
        {
            EmployeesDetailsViewModel = new EmployeesListViewViewModel();
            ClothesDetailsViewModel = new ClothesListViewViewModel();
            
            FilterCommand = new FilterCommand(this);
            AddCommand = new AddCommand(this);
            EditCommand = new EditCommand(this);
            SafeCommand = new SafeCommand(this);
            PrintCommand = new PrintCommand(this);
            PlusCommand = new PlusCommand(this);
            MinusCommand = new MinusCommand(this);
        }

        //readonly AddClothesView addClothesView = new();

        //public void OpenAddClothesView(object sender, RoutedEventArgs e)
        //    => addClothesView._AddClothesView.Visibility = Visibility.Visible;
    }
}
