using DVS.Stores;
using DVS.ViewModels.View_ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVS.Commands.EmployeeCommands
{
    class ClearEmployeeClothesListCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;

        public ClearEmployeeClothesListCommand(ModalNavigationStore modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter) => _modalNavigationStore.Close();
    }
}
