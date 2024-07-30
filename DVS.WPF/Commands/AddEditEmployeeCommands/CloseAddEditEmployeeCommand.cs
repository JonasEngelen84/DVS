using DVS.WPF.Stores;

namespace DVS.WPF.Commands.AddEditEmployeeCommands
{
    public class CloseAddEditEmployeeCommand(ClothesStore clothesStore,
        ModalNavigationStore modalNavigationStore) : CommandBase
    {
        private readonly ClothesStore _clothesStore = clothesStore;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override void Execute(object parameter)
        {//TODO: wenn bei Add/Edit mitarbeiter abgebrochen wird => DetailedClothesCollection aus DataBase neu laden
            _clothesStore.Load();
            _modalNavigationStore.Close();
        }
    }
}
