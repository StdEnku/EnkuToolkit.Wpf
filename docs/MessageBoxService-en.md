# MessageBoxService ViewService

From within the ViewModel, which should not depend on WPF assembly.

ViewService to manipulate the MessageBox.

Assuming that the View and ViewModel are managed in separate projects, the

interface and its implementation are stored in separate assemblies.



## interface side

interface name : IMessageBoxService

assembly : EnkuToolkit.UiIndependent

namespace : EnkuToolkit.UiIndependent.Services

Methods defined in the interface :

```c#
// Method for displaying OK-only message box with no choices
void ShowOk(string message, string? title = null);

// Method for displaying a message box with a Yes or No choice
bool ShowYesNo(string message, string? title = null);
```



## implementation side

class name : MessageBoxService

assembly : EnkuToolkit.Wpf

namespace : EnkuToolkit.Wpf.Services



## example

It is assumed that the DI container will be used to operate from the ViewModel.



Page1ViewModel.cs

```c#
using EnkuToolkit.UiIndependent.Services;

public class Page1ViewModel : INotifyPropertyChanged
{
    private readonly IMessageBoxService _messageBoxService;
    
    // Use constructor injection.
    public Page1ViewModel(IMessageBoxService messageBoxService)
    {
        this._messageBoxService = messageBoxService;
    }
    
    // Methods invoked by the delegate command
    private void ClickedCommand()
    {
        // OK button only message box display with no options
        this._messageBoxService.ShowOk("Its operation is impossible", "hello Worning");
    }
    
    // ~ommit~
}
```
