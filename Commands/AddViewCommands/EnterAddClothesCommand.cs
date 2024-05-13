using DVS.ViewModels.AddViewModels;

namespace DVS.Commands.AddViewCommands
{
    class EnterAddClothesCommand(AddClothesViewModel addClothesViewModel) : CommandBase
    {
        private readonly AddClothesViewModel addClothesViewModel = addClothesViewModel;

        public override void Execute(object parameter)
        {
            
        }
    }
}
