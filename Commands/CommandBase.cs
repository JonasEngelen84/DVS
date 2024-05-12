using System.Windows.Input;

namespace DVS.Commands
{
    public abstract class CommandBase : ICommand
    {
        //TODO: Klasse dokumentieren
        public event EventHandler CanExecuteChanged;

        public virtual bool CanExecute(object parameter) => true;

        public abstract void Execute(object parameter);

        protected virtual void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, new EventArgs());
    }
}
