namespace Sandbox.MvvmUtil;

using System.Windows.Input;
using System;

public class DelegateCommand : ICommand
{
    private Action<object?> _execute;
    private Func<object?, bool> _canExecute;

    public DelegateCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute ?? (x => true);
    }

    public void RaiseCanExecuteChanged()
    {
        var functor = CanExecuteChanged;
        functor?.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => _canExecute(parameter);

    public void Execute(object? parameter) => _execute(parameter);
}