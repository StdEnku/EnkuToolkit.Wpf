# WindowStateSaveBehavior Behavior

assembly : EnkuToolkit.Wpf

namespace : EnkuToolkit.Wpf.Controls

## remarks

```xaml
et:WindowStateSaveBehavior.IsStateSave="True"
```

A behavior that, when attached to a Window as in the code above, saves the current position, size, and WidnowState properties upon exit and restores the previous state the next time it is launched.

It can be attached to any class that inherits Window.

## example

MainWindow.xaml

```xaml
<Window
        x:Class="MyApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:MyApp"
        xmlns:et="https://github.com/StdEnku/EnkuToolkit/Wpf/Controls"
        mc:Ignorable="d"
        Title="MainWindow" 
        Height="400" Width="800"
        et:WindowStateSaveBehavior.IsStateSave="True">

    <Viewbox>
        <Label Content="Hello World!" />
    </Viewbox>
</Window>
```

