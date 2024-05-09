using DVS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVS.Stores
{
    /// <summary>
    /// Klasse zur navigation der Modals:
    /// sobald sich das aktuelle Modal (CurrentViewModelBase) ändert,
    /// wird durch Invoke() das "CurrentViewModelChanged" event aufgerufen,
    /// welches für das wechseln der Modals zuständig ist.
    /// 
    /// Eine Instanz von "ModalNavigationStore" wird in App.xaml.cs implementiert.
    /// Somit existiert diese eine Instanz die gesamte Lebensdauer der App hinweg.
    /// Von App.xaml.cs wird diese Instanz weitergereicht an das MainViewModel.
    /// </summary>
    class ModalNavigationStore
    {
        private ViewModelBase _currentViewModelBase;
        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModelBase; }

            set
            {
                _currentViewModelBase = value;
                CurrentViewModelChanged?.Invoke();
            }
        }

        // Bool zur Prüfung ob das aktuelle Modal bereits geöffnet ist.
        public bool IsOpen => CurrentViewModel != null;

        public event Action CurrentViewModelChanged;
    }
}
