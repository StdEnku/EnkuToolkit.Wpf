# TransformContentControl Class

assembly : EnkuToolkit.Wpf

namespace : EnkuToolkit.Wpf.Controls

## remarks

The ContentControl class can easily realize deformation operations such as moving, rotating, enlarging, etc.

It has five dependency properties for deformation, so use them to deform the object.

- TranslateX - Property for horizontal movement
- TranslateY - Property for vertical movement
- RotateAngle - 0~360 can be specified for rotation
- ScaleX - Vertical size magnification
- ScaleY - Horizontal size magnification

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
        Height="600" Width="600"
        ResizeMode="NoResize">


    <DockPanel LastChildFill="True">
        <DockPanel DockPanel.Dock="Bottom" LastChildFill="True" Margin="10">
            <Label Content="TranslateX" />
            <Slider Name="TranslateX" Minimum="0" Maximum="500" Value="300" />
        </DockPanel>

        <DockPanel DockPanel.Dock="Bottom" LastChildFill="True" Margin="10">
            <Label Content="TranslateY" />
            <Slider Name="TranslateY" Minimum="0" Maximum="300" Value="150" />
        </DockPanel>

        <DockPanel DockPanel.Dock="Bottom" LastChildFill="True"  Margin="10">
            <Label Content="Rotate" />
            <Slider Name="Rotate" Minimum="0" Maximum="360" />
        </DockPanel>

        <DockPanel DockPanel.Dock="Bottom" LastChildFill="True"  Margin="10">
            <Label Content="ScaleX" />
            <Slider Name="ScaleX" Minimum="0" Maximum="10" Value="1" />
        </DockPanel>

        <DockPanel DockPanel.Dock="Bottom" LastChildFill="True"  Margin="10">
            <Label Content="ScaleY" />
            <Slider Name="ScaleY" Minimum="0" Maximum="10" Value="1" />
        </DockPanel>

        <Canvas DockPanel.Dock="Top" ClipToBounds="True" Background="Blue">
            <et:TransformContentControl TranslateX="{Binding ElementName=TranslateX, Path=Value}"
                                        TranslateY="{Binding ElementName=TranslateY, Path=Value}"
                                        RotateAngle="{Binding ElementName=Rotate, Path=Value}"
                                        ScaleX="{Binding ElementName=ScaleX, Path=Value}" 
                                        ScaleY="{Binding ElementName=ScaleY, Path=Value}" >
                <Label Content="A" 
                       Background="Red" />
            </et:TransformContentControl>
        </Canvas>
    </DockPanel>
</Window>

```

![result](./imgs/TransformContentControl1.gif)