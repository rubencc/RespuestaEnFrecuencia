using System;
using System.Windows.Input;

namespace RespuestaEnFrecuencia.Common
{
    public class CommandBase : ICommand
    {
        private readonly Action action;

        public CommandBase(Action action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.action.Invoke();
        }

        public event EventHandler CanExecuteChanged;
    }
}
