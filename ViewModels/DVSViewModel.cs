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
        //public EmployeesListViewViewModel EmployeesDetailsViewModel { get; }
        //public ClothesListViewViewModel ClothesDetailsViewModel { get; }

        //public ICommand FilterListViewCommand { get; }
        //public ICommand EditModelsCommand { get; }
        //public ICommand BackwardCommand { get; }
        //public ICommand SafeCommand { get; }
        //public ICommand PrintCommand { get; }
        //public ICommand EditPlusCommand { get; }
        //public ICommand EditMinusCommand { get; }

        public DVSViewModel()
        {
            //EmployeesDetailsViewModel = new EmployeesListViewViewModel(_selectedClothesStore);
            //ClothesDetailsViewModel = new ClothesListViewViewModel(_selectedClothesStore);
            //FilterListViewCommand = new FilterListViewCommand(this);
        }

        readonly AddClothesView addClothesView = new();

        public void OpenAddClothesView(object sender, RoutedEventArgs e)
            => addClothesView._AddClothesView.Visibility = Visibility.Visible;
    }
}
