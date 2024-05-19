using DVS.ViewModels.AddEditViewModels;

namespace DVS.Commands.AddViewCommands
{
    class EnterAddEmployeeCommand(AddEmployeeViewModel addEmployeeViewModel) : CommandBase
    {
        private readonly AddEmployeeViewModel addEmployeeViewModel = addEmployeeViewModel;

        public override void Execute(object parameter)
        {

        }
    }
}
