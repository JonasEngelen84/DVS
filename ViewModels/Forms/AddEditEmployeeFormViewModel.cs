using System.Windows.Input;

namespace DVS.ViewModels.AddViewModels.Forms
{
    public class AddEditEmployeeFormViewModel(ICommand submitAddEmployeeCommand, ICommand closeModalCommand) : ViewModelBase
    {
        public ICommand SubmitAddEmployeeCommand { get; } = submitAddEmployeeCommand;
        public ICommand CloseModalCommand { get; } = closeModalCommand;
    }
}
