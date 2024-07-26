using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Store_Management.ViewModels
{
    public class RelayCommand : ICommand
    {
        readonly Action<object?> _execute = null!;
        readonly Predicate<object?>? _canExecute;


        public event EventHandler? CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }


        public static RelayCommand From(Action<object?> execute) => new RelayCommand(execute);
        //Constructors
        public RelayCommand(Action<object?> execute) : this(execute, null!) { }
        public RelayCommand(Action<object?> execute, Predicate<object?> canExecute)
        {
            this._execute = execute;
            this._canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute is null ? true : _canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            _execute(parameter);
        }
    }
}
