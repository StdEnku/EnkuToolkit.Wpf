namespace Sandbox.MvvmUtil;

using System.ComponentModel;
using System.Runtime.CompilerServices;

public class ViewModelsBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}