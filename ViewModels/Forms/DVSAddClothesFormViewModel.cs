using DVS.Commands.AddViewCommands;
using DVS.Stores;
using System.Windows.Input;

namespace DVS.ViewModels.AddViewModels.Forms
{
    internal class DVSAddClothesFormViewModel : ViewModelBase
    {
        public ICommand OpenAddEditCategories {  get; }
        public ICommand OpenAddEditSeasons {  get; }

        public DVSAddClothesFormViewModel(ModalNavigationStore _modalNavigationStore)
        {
            OpenAddEditCategories= new OpenAddEditCategoriesCommand(_modalNavigationStore);
            OpenAddEditSeasons = new OpenAddEditSeasonsCommand(_modalNavigationStore);
        }
    }
}
