# AnimatedFrame Class

assembly : EnkuToolkit.Wpf

namespace : EnkuToolkit.Wpf.Controls

## summary

Extension of the Frame class that can perform animation during screen transitions

The basic usage of the Frame class is the same as the standard WPF Frame class, but the following CLR properties have been added.

The following CLR properties have been added, allowing the user to specify the animation to be executed during screen transitions

The following CLR properties have been added.

```c#
// When using the standard animations already included in this library
// property for specifying the type of animation to execute during screen transitions.
// If you do not want to use your own animation or do not want to execute an animation
// null.
// Also, the BuiltinAnimTypes type is an enum value.
public BuiltinAnimTypes? BuiltinAnimType { get; set; }
```

```c# 
// Property for specifying animation in Storyboard for self-made forward screen transitions.
// If the above BuiltinAnimType is not null, it is ignored.
// If null is specified, the animation for forward screen transitions is not executed.
public Storyboard? ForwardAnim { get; set; }
```

```c#
// Property for specifying an animation in Storyboard for a home-made reverse screen transition.
// If the above BuiltinAnimType is not null, it is ignored.
// If null is specified, the animation for reverse screen transitions is not executed.
public Storyboard? BackwardAnim { get; set; }
```

Currently, only the following two standard animations are included in this library

- Slidein 
- ModernSlidein



## example

All of the following files should be placed in the root folder of the project.

DefaultPage.xaml

```xaml
<Page x:Class="MyApp.DefaultPage"
      xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
      xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
      xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" 
      xmlns:d="http://schemas.microsoft.com/expression/blend/2008" 
      xmlns:local="clr-namespace:MyApp"
      mc:Ignorable="d" 
      d:DesignHeight="450" d:DesignWidth="800"
      Title="DefaultPage">

    <Viewbox>
        <Label Content="Default Page" />
    </Viewbox>
</Page>
```



Page1.xaml

```xaml
<Page x:Class="MyApp.Page1"
      xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
      xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
      xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" 
      xmlns:d="http://schemas.microsoft.com/expression/blend/2008" 
      xmlns:local="clr-namespace:MyApp"
      mc:Ignorable="d" 
      d:DesignHeight="450" d:DesignWidth="800"
      Title="Page1">

    <Viewbox>
        <Label Content="Page1" />
    </Viewbox>
</Page>
```



Page2.xaml

```xaml
<Page x:Class="MyApp.Page2"
      xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
      xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
      xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" 
      xmlns:d="http://schemas.microsoft.com/expression/blend/2008" 
      xmlns:local="clr-namespace:MyApp"
      mc:Ignorable="d" 
      d:DesignHeight="450" d:DesignWidth="800"
      Title="Page2">

    <Viewbox>
        <Label Content="Page2" />
    </Viewbox>
</Page>

```



MainWindow.xaml

```xaml
<Window x:Class="MyApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:MyApp"
        xmlns:et="https://github.com/StdEnku/EnkuToolkit/Wpf/Controls"
        mc:Ignorable="d"
        Title="MainWindow" Height="450" Width="800">
    
    <DockPanel LastChildFill="True">
        <UniformGrid Columns="4" DockPanel.Dock="Bottom">
            <Button Content="Go Back" Margin="10" Click="GoBack" IsEnabled="{Binding ElementName=MainFrame, Path=CanGoBack}" />
            <Button Content="Go Forward" Margin="10" Click="GoForward" IsEnabled="{Binding ElementName=MainFrame, Path=CanGoForward}" />
            <Button Content="Go Page1" Margin="10" Click="GoPage1" />
            <Button Content="Go Page2" Margin="10" Click="GoPage2" />
        </UniformGrid>

        <et:AnimatedFrame Name="MainFrame"
                          Source="./DefaultPage.xaml"
                          BuiltinAnimType="ModernSlidein"
                          DockPanel.Dock="Top" />
    </DockPanel>
</Window>
```



MainWindow.xaml.cs

```csharp
namespace MyApp;

using System;
using System.Windows;

public partial class MainWindow : Window
{
    private readonly Uri _baseUri = new Uri("pack://application:,,,/", UriKind.Absolute);
    
    public MainWindow()
    {
        InitializeComponent();
    }

    private void GoBack(object sender, RoutedEventArgs e)
    {
        this.MainFrame.GoBack();
    }

    private void GoForward(object sender, RoutedEventArgs e)
    {
        this.MainFrame.GoForward();
    }

    private void GoPage1(object sender, RoutedEventArgs e)
    {
        var nextUri = new Uri(this._baseUri, "Page1.xaml");
        this.MainFrame.Navigate(nextUri);
    }

    private void GoPage2(object sender, RoutedEventArgs e)
    {
        var nextUri = new Uri(this._baseUri, "Page2.xaml");
        this.MainFrame.Navigate(nextUri);
    }
}
```

![result](./imgs/AnimatedFrame1.gif)



## Example of self-made animation

Modify the MainWindow.xaml of the above sample as follows.

```xaml
<Window x:Class="MyApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:MyApp"
        xmlns:et="https://github.com/StdEnku/EnkuToolkit/Wpf/Controls"
        mc:Ignorable="d"
        Title="MainWindow" Height="450" Width="800">
    
    <DockPanel LastChildFill="True">
        <UniformGrid Columns="4" DockPanel.Dock="Bottom">
            <Button Content="Go Back" Margin="10" Click="GoBack" IsEnabled="{Binding ElementName=MainFrame, Path=CanGoBack}" />
            <Button Content="Go Forward" Margin="10" Click="GoForward" IsEnabled="{Binding ElementName=MainFrame, Path=CanGoForward}" />
            <Button Content="Go Page1" Margin="10" Click="GoPage1" />
            <Button Content="Go Page2" Margin="10" Click="GoPage2" />
        </UniformGrid>

        <et:AnimatedFrame Name="MainFrame"
                          Source="./DefaultPage.xaml"
                          DockPanel.Dock="Top">

            <et:AnimatedFrame.ForwardAnim>
                <Storyboard>
                    <DoubleAnimationUsingKeyFrames BeginTime="0:0:0"
                                       Storyboard.TargetName="CurrentTransform"
                                       Storyboard.TargetProperty="(et:NormalizedTransformContentControl.TranslateY)"
                                       FillBehavior="Stop">

                        <DiscreteDoubleKeyFrame KeyTime="0:0:0" Value="1" />
                        <EasingDoubleKeyFrame KeyTime="0:0:1" Value="0">
                            <EasingDoubleKeyFrame.EasingFunction>
                                <BounceEase EasingMode="EaseOut" />
                            </EasingDoubleKeyFrame.EasingFunction>
                        </EasingDoubleKeyFrame>
                    </DoubleAnimationUsingKeyFrames>

                    <DoubleAnimationUsingKeyFrames BeginTime="0:0:0"
                                       Storyboard.TargetName="OldTransform"
                                       Storyboard.TargetProperty="(et:NormalizedTransformContentControl.TranslateY)"
                                       FillBehavior="Stop">

                        <DiscreteDoubleKeyFrame KeyTime="0:0:0" Value="0" />
                        <EasingDoubleKeyFrame KeyTime="0:0:1" Value="-1">
                            <EasingDoubleKeyFrame.EasingFunction>
                                <BounceEase EasingMode="EaseOut" />
                            </EasingDoubleKeyFrame.EasingFunction>
                        </EasingDoubleKeyFrame>
                    </DoubleAnimationUsingKeyFrames>
                </Storyboard>
            </et:AnimatedFrame.ForwardAnim>

            <et:AnimatedFrame.BackwardAnim>
                <Storyboard>
                    <DoubleAnimationUsingKeyFrames BeginTime="0:0:0"
                                       Storyboard.TargetName="CurrentTransform"
                                       Storyboard.TargetProperty="(et:NormalizedTransformContentControl.TranslateY)"
                                       FillBehavior="Stop">

                        <DiscreteDoubleKeyFrame KeyTime="0:0:0" Value="-1" />
                        <EasingDoubleKeyFrame KeyTime="0:0:1" Value="0">
                            <EasingDoubleKeyFrame.EasingFunction>
                                <BounceEase EasingMode="EaseOut" />
                            </EasingDoubleKeyFrame.EasingFunction>
                        </EasingDoubleKeyFrame>
                    </DoubleAnimationUsingKeyFrames>

                    <DoubleAnimationUsingKeyFrames BeginTime="0:0:0"
                                       Storyboard.TargetName="OldTransform"
                                       Storyboard.TargetProperty="(et:NormalizedTransformContentControl.TranslateY)"
                                       FillBehavior="Stop">

                        <DiscreteDoubleKeyFrame KeyTime="0:0:0" Value="0" />
                        <EasingDoubleKeyFrame KeyTime="0:0:1" Value="1">
                            <EasingDoubleKeyFrame.EasingFunction>
                                <BounceEase EasingMode="EaseOut" />
                            </EasingDoubleKeyFrame.EasingFunction>
                        </EasingDoubleKeyFrame>
                    </DoubleAnimationUsingKeyFrames>
                </Storyboard>
            </et:AnimatedFrame.BackwardAnim>
        </et:AnimatedFrame>
    </DockPanel>
</Window>
```

![result](./imgs/AnimatedFrame2.gif)
