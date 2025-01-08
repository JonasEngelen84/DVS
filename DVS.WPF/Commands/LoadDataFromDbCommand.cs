using DVS.WPF.Stores;

namespace DVS.WPF.Commands
{
    public class LoadDataFromDbCommand(SizeStore sizeStore,
                                       CategoryStore categoryStore,
                                       SeasonStore seasonStore,
                                       ClothesStore clothesStore,
                                       ClothesSizeStore clothesSizeStore,
                                       EmployeeStore employeeStore,
                                       EmployeeClothesSizesStore employeeClothesSizesStore)
                                       : AsyncCommandBase
    {
        private readonly SizeStore _sizeStore = sizeStore;
        private readonly CategoryStore _categoryStore = categoryStore;
        private readonly SeasonStore _seasonStore = seasonStore;
        private readonly ClothesStore _clothesStore = clothesStore;
        private readonly ClothesSizeStore _clothesSizeStore = clothesSizeStore;
        private readonly EmployeeStore _employeeStore = employeeStore;
        private readonly EmployeeClothesSizesStore _employeeClothesSizesStore = employeeClothesSizesStore;

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _sizeStore.Load();
            }
            catch
            {
                ShowErrorMessageBox("Laden der Sizes ist fehlgeschlagen!", "LoadDataFromDbCommand");
            }

            try
            {
                await _categoryStore.Load();
            }
            catch
            {
                ShowErrorMessageBox("Laden der Categories ist fehlgeschlagen!", "LoadDataFromDbCommand");
            }
            
            try
            {
                await _seasonStore.Load();
            }
            catch
            {
                ShowErrorMessageBox("Laden der Seasons ist fehlgeschlagen!", "LoadDataFromDbCommand");
            }

            try
            {
                await _clothesStore.Load();
            }
            catch
            {
                ShowErrorMessageBox("Laden der Clothes ist fehlgeschlagen!", "LoadDataFromDbCommand");
            }

            try
            {
                await _clothesSizeStore.Load();
            }
            catch
            {
                ShowErrorMessageBox("Laden der ClothesSizes ist fehlgeschlagen!", "LoadDataFromDbCommand");
            }
            
            try
            {
                await _employeeStore.Load();
            }
            catch
            {
                ShowErrorMessageBox("Laden der Employees ist fehlgeschlagen!", "LoadDataFromDbCommand");
            }

            try
            {
                await _employeeClothesSizesStore.Load();
            }
            catch
            {
                ShowErrorMessageBox("Laden der EmployeeClothesSizes ist fehlgeschlagen!", "LoadDataFromDbCommand");
            }
        }
    }
}
