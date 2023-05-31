namespace Sandbox.Views;

using EnkuToolkit.Wpf.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

/// <summary>
/// MainWindow7.xaml の相互作用ロジック
/// </summary>
public partial class MainWindow7 : CustomizableTitlebarAnimatedNavigationWindow
{
    public MainWindow7()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
    }

    private void Button_Click_2(object sender, RoutedEventArgs e)
    {
        App.Current.Shutdown();
    }
}
