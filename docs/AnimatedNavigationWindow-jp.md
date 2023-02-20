# AnimatedNavigationWindowクラス

アセンブリ : EnkuToolkit.Wpf

名前空間 : EnkuToolkit.Wpf.Controls

## 概要

画面遷移時にアニメーションを実行可能なNavigationWindowクラスの拡張

基本的な使用方法はWPFに標準搭載されているNavigationWindowクラスと同じですが

下記CLRプロパティが追加されており、それらを使用して画面遷移時に実行するアニメーションを

指定できるようになっています。

```c#
// 本ライブラリにすでに搭載されている標準のアニメーションを使用する際に
// 画面遷移時に実行するアニメーションの種類を指定するためのプロパティ
// 自作アニメーションを使用したりアニメーション実行をしたくない場合は
// nullを指定する。
// また、BuiltinAnimTypes型はenum値です。
public BuiltinAnimTypes? BuiltinAnimType { get; set; }
```

```c# 
// 自作の順方向画面遷移時のアニメーションをStoryboardで指定するためのプロパティ
// 上記BuiltinAnimTypeがnullではない場合、無視される。
// nullを指定すると順方向画面遷移時のアニメーションは実行されない。
public Storyboard? ForwardAnim { get; set; }
```

```c#
// 自作の逆方向画面遷移時のアニメーションをStoryboardで指定するためのプロパティ
// 上記BuiltinAnimTypeがnullではない場合、無視される。
// nullを指定すると逆方向画面遷移時のアニメーションは実行されない。
public Storyboard? BackwardAnim { get; set; }
```

現在本ライブラリに組み込まれている標準のアニメーションは

- Slidein 
- ModernSlidein

の二つのみです。

## 使用例

下記ファイル群はすべてプロジェクトのルートフォルダに配置してください。



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

    <DockPanel LastChildFill="True">
        <Button Content="go Page2" DockPanel.Dock="Bottom" Margin="10" Click="Button_Click" />

        <Viewbox DockPanel.Dock="Top">
            <Label Content="Page1" />
        </Viewbox>
    </DockPanel>
</Page>
```



Page1.xaml.cs

```csharp
namespace MyApp;

using EnkuToolkit.Wpf.Controls;
using System;
using System.Windows.Controls;

public partial class Page1 : Page
{
    public readonly Uri _baseUri = new Uri("pack://application:,,,/", UriKind.Absolute);

    public Page1()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        var anw = (AnimatedNavigationWindow)App.Current.MainWindow;
        var nextUri = new Uri(this._baseUri, "Page2.xaml");
        anw.Navigate(nextUri);
    }
}
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

    <DockPanel LastChildFill="True">
        <Button Content="go back" DockPanel.Dock="Bottom" Margin="10" Click="Button_Click" />

        <Viewbox DockPanel.Dock="Top">
            <Label Content="Page2" />
        </Viewbox>
    </DockPanel>
</Page>
```



Page2.xaml.cs

```csharp
namespace MyApp;

using EnkuToolkit.Wpf.Controls;
using System;
using System.Windows.Controls;

public partial class Page2 : Page
{
    public Page2()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        var anw = (AnimatedNavigationWindow)App.Current.MainWindow;
        anw.GoBack();
    }
}
```





MainWindow.xaml

```xaml
<et:AnimatedNavigationWindow
        x:Class="MyApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:MyApp"
        xmlns:et="https://github.com/StdEnku/EnkuToolkit/Wpf/Controls"
        mc:Ignorable="d"
        Title="MainWindow" Height="450" Width="800"
        Source="Page1.xaml"
        BuiltinAnimType="ModernSlidein">
    
</et:AnimatedNavigationWindow>
```



![result1](C:\Users\Syple\Desktop\EnkuToolkit\docs\imgs\AnimatedNavigationWindow1.gif)



## アニメーションの自作例

上記サンプルのMainWindow.xaml内を下記のように修正してください。

```xaml
<et:AnimatedNavigationWindow
        x:Class="MyApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:MyApp"
        xmlns:et="https://github.com/StdEnku/EnkuToolkit/Wpf/Controls"
        mc:Ignorable="d"
        Title="MainWindow" Height="450" Width="800"
        Source="Page1.xaml">

    <et:AnimatedNavigationWindow.ForwardAnim>
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
    </et:AnimatedNavigationWindow.ForwardAnim>

    <et:AnimatedNavigationWindow.BackwardAnim>
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
    </et:AnimatedNavigationWindow.BackwardAnim>
    
</et:AnimatedNavigationWindow>

```

![result2](./imgs/AnimatedNavigationWindow2.gif)