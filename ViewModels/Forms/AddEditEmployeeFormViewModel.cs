using System.Windows.Input;

namespace DVS.ViewModels.AddViewModels.Forms
{
    public class AddEditEmployeeFormViewModel : ViewModelBase
    {
        public ICommand SubmitAddEditEmployeeCommand { get; }
        public ICommand CloseModalCommand { get; }

        public AddEditEmployeeFormViewModel(ICommand submitAddEmployeeCommand, ICommand closeModalCommand)
        {
            SubmitAddEditEmployeeCommand = submitAddEmployeeCommand;
            CloseModalCommand = closeModalCommand;
        }
    }
}
