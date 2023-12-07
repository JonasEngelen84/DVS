using System;
using System.Collections.Generic;
using DVS.Commands;
using DVS.Stores;
using System.Windows.Input;

namespace DVS.ViewModels
{
    internal class DVSViewModel : ViewModelBase
    {
        public EmployeesListViewViewModel EmployeesDetailsViewModel { get; }
        public ClothesListViewViewModel ClothesDetailsViewModel { get; }

        public ICommand FilterListViewCommand { get; }
        public ICommand EditModelsCommand { get; }
        public ICommand BackwardCommand { get; }
        public ICommand SafeCommand { get; }
        public ICommand PrintCommand { get; }
        public ICommand ContactCommand { get; }
        public ICommand EditPlusCommand { get; }
        public ICommand EditMinusCommand { get; }

        public DVSViewModel(SelectedClothesStore _selectedClothesStore)
        {
            ClothesDetailsViewModel = new ClothesListViewViewModel(_selectedClothesStore);
            EmployeesDetailsViewModel = new EmployeesListViewViewModel(_selectedClothesStore);
            FilterListViewCommand = new FilterListViewCommand(this);
        }
    }
}
