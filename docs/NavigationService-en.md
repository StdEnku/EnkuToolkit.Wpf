# NavigationService ViewService

If Application.Current.MainWindow is a NavigationWindow, then the

ViewService to perform screen transitions from the available ViewModel.

Assuming that View and ViewModel are managed in separate projects

interface and its implementation are stored in separate assemblies.



## interface side

interface name : INavigationService

assembly : EnkuToolkit.UiIndependent

namespace : EnkuToolkit.UiIndependent.Services

Methods defined in the interface : 

```c#
// Methods for screen transitions that allow specifying transition destinations by relative URI based on the project root
bool NavigateRootBase(string uriStr, object? extraData = null);

// Methods for screen transitions that allow specifying the transition destination by URI
bool Navigate(Uri uri, object? extraData = null);

// Methods to advance the page
void GoForward();

// Methods to return pages
void GoBack();

// Methods for reloading pages
void Refresh();

// Method to remove the latest history item from the back history
void RemoveBackEntry();

// Method to stop downloading content corresponding to the current navigation request
void StopLoading();
```



## implementation side

class name : NavigationService

assembly : EnkuToolkit.Wpf

namespace : EnkuToolkit.Wpf.Services



## example

It is assumed that the DI container will be used to operate from the ViewModel.



Page1ViewModel.cs

```c#
using EnkuToolkit.UiIndependent.Services;

public class Page1ViewModel : INotifyPropertyChanged
{
    private readonly INavigationService _navigationService;
    
    // Use constructor injection.
    public Page1ViewModel(INavigationService navigationService)
    {
        this._navigationService = navigationService;
    }
    
    // Methods invoked by the delegate command
    private void ClickedCommand()
    {
        // to Page2
        this._navigationService.NavigateRootBase("./Page2.xaml");
    }
    
    // ~omit~
}
```





