using System.Windows.Input;

namespace DVS.ViewModels.AddViewModels.Forms
{
    public class DVSAddEmployeeFormViewModel(ICommand submitAddEmployeeCommand, ICommand closeModalCommand) : ViewModelBase
    {
        public ICommand SubmitAddEmployeeCommand { get; } = submitAddEmployeeCommand;
        public ICommand CloseModalCommand { get; } = closeModalCommand;
    }
}
