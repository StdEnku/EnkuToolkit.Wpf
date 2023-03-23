namespace EnkuToolkit.Wpf.Controls.Internals;

using System.Windows.Input;
using System;

internal class InternalDelegateCommand : ICommand
{
    private Action _execute;
    private Func<bool>? _canExecute;

    public InternalDelegateCommand(Action execute)
        => this._execute = execute;

    public InternalDelegateCommand(Action execute, Func<bool> canExecute)
    {
        this._execute = execute;
        this._canExecute = canExecute;
    }

    #region IComamndの実装
    public bool CanExecute(object? parameter)
        => this._canExecute?.Invoke() ?? true;

    public void Execute(object? parameter)
        => this._execute();

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value; 
    }
    #endregion
}