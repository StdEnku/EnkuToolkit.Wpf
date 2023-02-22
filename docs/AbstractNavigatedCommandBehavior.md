# AbstractNavigatedCommandBehavior ビヘイビア

アセンブリ : EnkuToolkit.Wpf

名前空間 : EnkuToolkit.Wpf.Behaviors

## 概要

ページに添付して使用する画面遷移後にバインドされたコマンドを

実行するためのビヘイビアを作成するための抽象クラス。

画面遷移後のパラメータの受け取りに使用することを想定している。

## 使用例

下記にMainWindow内のFrameにて対象のページが画面遷移によって読み込まれた場合の例を示す。



MainWindow.xaml

```xaml
<Window
    x:Class="MyApp.MainWindow"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    xmlns:et="https://github.com/StdEnku/EnkuToolkit/Wpf/Controls"
    xmlns:local="clr-namespace:MyApp"
    mc:Ignorable="d"
    Title="MainWindow">

    <Frame Name="MainFrame" Source="Page1.xaml" />
</Window>
```





MainFrameNavigationCommandBehavior.cs

```c#
namespace MyApp;

using System.Windows.Navigation;
using System.Windows;
using EnkuToolkit.Wpf.Behaviors;

// ジェネリックには自信の型を指定する。
public class MainFrameNavigationCommandBehavior : AbstractNavigatedCommandBehavior<MainFrameNavigationCommandBehavior>
{
    // 画面遷移で使用するFrameのNavigationServiceプロパティを返す。
    protected override NavigationService TargetNavigationService
    {
        get
        {
            var window = (MainWindow)Application.Current.MainWindow;
            var frame = window.MainFrame;
            return frame.NavigationService;
        }
    }
}
```



