# WindowStateSaveBehavior ビヘイビア

アセンブリ : EnkuToolkit.Wpf

名前空間 : EnkuToolkit.Wpf.Controls

## 概要

```xaml
et:WindowStateSaveBehavior.IsStateSave="True"
```

上記コードのようにWindowに添付すると終了時に現在の位置、サイズ、WidnowStateプロパティを保存して、次回起動時に以前の状態を復元させるためのビヘイビア。

Windowを継承しているクラスにはすべて添付可能です。

## 使用例

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
        et:WindowStateSaveBehavior.IsStateSave="True">

    <Viewbox>
        <Label Content="Hello World!" />
    </Viewbox>
</Window>
```

