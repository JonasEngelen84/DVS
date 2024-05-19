using DVS.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVS.Commands.AddViewCommands
{
    class OpenAddEditSeasonsCommand(ModalNavigationStore modalNavigationStore) : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override void Execute(object parameter)
        {
            
        }
    }
}
