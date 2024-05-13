using DVS.ViewModels.AddViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
