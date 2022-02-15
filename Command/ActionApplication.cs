using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DataManagerPC.Command
{
    class ActionApplication : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action<object> _execute;
        private Func<object, bool> _canExecute;

        public ActionApplication(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);

        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
