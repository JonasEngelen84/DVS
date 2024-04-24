using DVS.ViewModels;

namespace DVS.Commands.DVSViewCommands
{
    internal class FilterCommand(DVSViewModel dVSViewModel) : CommandBase
    {
        private readonly DVSViewModel dVSViewModel = dVSViewModel;

        public override void Execute(object parameter)
        {
            
        }
    }
}
