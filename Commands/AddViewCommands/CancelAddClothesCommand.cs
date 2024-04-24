using DVS.ViewModels.AddViewModels;

namespace DVS.Commands.AddViewCommands
{
    internal class CancelAddClothesCommand(AddViewModel addViewModel) : CommandBase
    {
        private readonly AddViewModel addViewModel = addViewModel;

        public override void Execute(object parameter)
        {
            
        }
    }
}