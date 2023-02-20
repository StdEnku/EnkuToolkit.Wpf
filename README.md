![logo](./docs/imgs/logo.png)

[日本語版README](./README-jp.md)

# How to Install?

This library is available as a package at Nuget.org.

Please install it using VisualStudio's Nuget package manager, etc.



## Explanation of the two assemblies

| DL                                                           | assembly name                                                | remarks                                                      |
| ------------------------------------------------------------ | ------------------------------------------------------------ | ------------------------------------------------------------ |
| <img src="https://img.shields.io/nuget/dt/EnkuToolkit.Wpf?color=indigo&logo=Nuget&style=plastic" alt="Nuget" style="zoom:200%;" /> | [EnkuToolkit.Wpf](https://www.nuget.org/packages/EnkuToolkit.Wpf/) | An assembly that describes custom controls and other items that depend on WPF. |
| <img src="https://img.shields.io/nuget/dt/EnkuToolkit.UiIndependent?color=indigo&logo=Nuget&style=plastic" alt="Nuget" style="zoom:200%;" /> | [EnkuToolkit.UiIndependent](https://www.nuget.org/packages/EnkuToolkit.UiIndependent/) | An assembly marked with WPF-independent portions intended to be called at the ViewModel layer. |

This library consists of the above two assemblies

EnkuToolkit.Wpf internally depends on EnkuToolkit.UiIndependent, so

UiIndependent, so if you want to manage View and ViewModel in a single project, you should use

Wpf should be installed only.

If the View and ViewModel are in separate projects, install only EnkuToolkit.Wpf in the View side project.

EnkuToolkit.Wpf in the View side project.

UiIndependent in the ViewModel project.



# List of Features

To access all classes in this library from xaml, please access them from the following xml namespace.

```xaml
xmlns:et="https://github.com/StdEnku/EnkuToolkit/Wpf/Controls"
```



## Custom Controls

| Control name                                                 | remarks                                                      |
| ------------------------------------------------------------ | ------------------------------------------------------------ |
| [AnimatedFrame](./docs/AnimatedFrame-en.md)                  | Frame class that enables animation during screen transitions |
| [AnimatedNavigationWindow](./docs/AnimatedNavigationWindow-en.md) | NavigationWindow class that enables animation during screen transitions |
| [CustomTitlebarWindow](./docs/CustomTitlebarWindow-en.md)    | Window class with customizable title bar                     |
| [CustomTitlebarAnimatedNavigationWindow](./docs/CustomTitlebarAnimatedNavigationWindow-en.md) | AnimatedNavigationWindow class with customizable title bar   |
| [TransformContentControl](./docs/TransformContentControl-en.md) | ContentControl for easy transformation operations such as moving, transforming, enlarging, etc. |
| [NormalizedTransformContentControl](./docs/NormalizedTransformContentControl-en.md) | TransformContentControl that allows transform properties to be manipulated with values ranging from 0 to 1. |



## Attached Behaviors

| behavior name                                                | remarks                                                      |
| ------------------------------------------------------------ | ------------------------------------------------------------ |
| [WindowStateSaveBehavior](./docs/WindowStateSaveBehavior-en.md) | Behavior to attach to Window to save the current position, size, and WidnowState properties when exiting and restore the previous state the next time it is launched. |



## View Services

| View Service Name                                   | remarks                                                      |
| --------------------------------------------------- | ------------------------------------------------------------ |
| [NavigationService](./docs/NavigationService-en.md) | ViewService to perform screen transitions from an available ViewModel when Application.Current.MainWindow is NavigationWindow |
| [MessageBoxService](./docs/MessageBoxService-en.md) | ViewServce to allow message box operations to be performed from the ViewModel |
