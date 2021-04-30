using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMTools
{
    public class Command : BaseCommand
    {
        public Action action;
        public Func<bool> canExecute;

        public Command(Action action, Func<bool> canExecute = null)
        {
            this.action = action;
            this.canExecute = canExecute;
        }
        public override void Execute(object parameter)
        {
            action.Invoke();
        }

        public override bool CanExecute(object parameter) => canExecute?.Invoke() ?? true;
    }

    public class Command<T> : BaseCommand
    {
        public Action<T> action;
        public Func<T, bool> canExecute;

        public Command(Action<T> action, Func<T, bool> canExecute = null)
        {
            this.action = action;
            this.canExecute = canExecute;
        }
        public override void Execute(object parameter)
        {
            action.Invoke((T)parameter);
        }

        public override bool CanExecute(object parameter) => canExecute?.Invoke((T)parameter) ?? true;
    }
}
