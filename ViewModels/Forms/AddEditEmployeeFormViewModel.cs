using System.Windows.Input;

namespace DVS.ViewModels.AddViewModels.Forms
{
    public class AddEditEmployeeFormViewModel : ViewModelBase
    {
        public ICommand SubmitAddEmployeeCommand { get; }
        public ICommand CloseModalCommand { get; }

        public AddEditEmployeeFormViewModel(ICommand submitAddEmployeeCommand, ICommand closeModalCommand)
        {
            SubmitAddEmployeeCommand = submitAddEmployeeCommand;
            CloseModalCommand = closeModalCommand;
        }
    }
}
