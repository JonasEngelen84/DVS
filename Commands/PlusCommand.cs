using DVS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVS.Commands
{
    internal class PlusCommand : CommandBase
    {
        private DVSViewModel dVSViewModel;

        public PlusCommand(DVSViewModel dVSViewModel)
        {
            this.dVSViewModel = dVSViewModel;
        }

        public override void Execute(object parameter)
        {

        }
    }
}
