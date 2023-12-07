using DVS.ViewModels;

namespace DVS.Commands
{
    internal class FilterListViewCommand : CommandBase
    {
        private DVSViewModel dVSViewModel;

        public FilterListViewCommand(DVSViewModel dVSViewModel)
        {
            this.dVSViewModel = dVSViewModel;
        }

        public override void Execute(object parameter)
        {
            //TODO: das Command testen
        }
    }
}
