using System;
using System.Windows.Input; // Contiene la clase ICommand

namespace MeditacionDominical.ViewModels
{
    public class ActionCommand : ICommand
    {
        Action accion;

        public ActionCommand(Action action)
        {
            this.accion = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public event EventHandler Executed;

        public void Execute(object parameter)
        {
            accion();
        }
    }
}
