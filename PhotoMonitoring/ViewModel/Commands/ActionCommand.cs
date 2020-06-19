using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace PhotoMonitoring.ViewModel.Commands
{
    public class ActionCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action action;

        public ActionCommand(Action _action)
        {
            this.action = _action;
        }
        public bool CanExecute(object parameter)
        {
            //throw new NotImplementedException();
            return true;
        }

        public void Execute(object parameter)
        {
            //throw new NotImplementedException();
            this.action.Invoke();
        }
    }
}
