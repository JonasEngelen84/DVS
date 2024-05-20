using DVS.Commands;
using DVS.Stores;
using System.Windows.Input;

namespace DVS.ViewModels.AddEditViewModels
{
    class AddEditCategorieViewModel : ViewModelBase
    {
        public ICommand CloseModalCommand { get; }

        public AddEditCategorieViewModel(ModalNavigationStore _modalNavigationStore)
        {
            CloseModalCommand = new CloseModalCommand(_modalNavigationStore);
        }
    }
}
