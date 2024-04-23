using DVS.ViewModels;

namespace DVS.Commands
{
    internal class FilterCommand : CommandBase
    {
        private DVSViewModel dVSViewModel;

        public FilterCommand(DVSViewModel dVSViewModel)
        {
            this.dVSViewModel = dVSViewModel;
        }

        public override void Execute(object parameter)
        {
            
        }
    }
}
