using System.Windows;
using System.Windows.Input;

namespace DVS.WPF.Commands
{
    /// <summary>
    /// Hauptklasse der Commands, welche von allen Unterklassen vererbt wird.
    /// Von ihr sollen keine Instanzen deklariert werden (abstract)!
    /// Sie erbt von ICommand und stellt somit folgende Eigenschaften zur Verfügung:
    /// 
    /// public event EventHandler CanExecuteChanged;
    /// public virtual bool CanExecute(object parameter) => true;
    /// public abstract void Execute(object parameter);
    /// protected virtual void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, new EventArgs());
    /// 
    /// Zugriff:
    /// Ein in der View implementierter Button bekommt einen command mit einem selbst definierten Namen
    /// zugewiesen.
    /// In dem ViewModel der View wird ein ICommand mit dem selben Namen des commands aus der View deklariert.
    /// Dieses ICommand ist eine Instanz der Command-Klasse, welche ebenfalls selbst angelegt werden muss,
    /// und von der Hauptklasse erbt.
    /// Der Instanz der Command-Klasse werden bei erzeugung die benötigten Parameter zugewiesen.
    /// (Bei FormViews werden alle Commands in dem ViewModel erzeugt, in dessen View die Form implementiert wurde.
    /// Eine Instanz dieser Form wird in dem ViewModel deklar-/initialisiert und bekommt die angelegten
    /// Commands übergeben.)
    /// In der Command-Klasse wird nun, je nach gewünschter Ausführung, auf die geerbten Eigenschaften
    /// zugegriffen.
    /// </summary>
    public abstract class CommandBase : ICommand
    {
        // Tritt auf, wenn Änderungen vorgenommen werden, die sich darauf auswirken, ob der Befehl ausgeführt werden soll.
        public event EventHandler CanExecuteChanged;


        // Bestimmt, ob der Befehl in seinem aktuellen Zustand ausgeführt werden kann.
        public virtual bool CanExecute(object parameter) => true;


        // Definiert die Methode, die aufgerufen wird, wenn der Befehl aufgerufen wird.
        public abstract void Execute(object parameter);


        // Wird aufgerufen, wenn sich die Bedingungen geändert haben, die bestimmen, ob der Befehl ausgeführt werden kann.
        protected virtual void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, new EventArgs());


        public static void ShowErrorMessageBox(string message, string title)
        {
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBox.Show(message, title, button, icon);
        }
    }
}
