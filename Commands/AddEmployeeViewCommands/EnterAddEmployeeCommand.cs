using DVS.ViewModels.AddEditViewModels;

namespace DVS.Commands.AddEmployeeViewCommands
{
    class EnterAddEmployeeCommand(AddEmployeeViewModel addEmployeeViewModel) : CommandBase
    {
        private readonly AddEmployeeViewModel addEmployeeViewModel = addEmployeeViewModel;

        public override void Execute(object parameter)
        {

        }
    }
}
