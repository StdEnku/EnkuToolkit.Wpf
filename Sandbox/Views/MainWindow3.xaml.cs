namespace Sandbox.Views;
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
/// MainWindow3.xaml の相互作用ロジック
/// </summary>
public partial class MainWindow3 : Window
{
    public MainWindow3()
    {
        InitializeComponent();
    }

    private int _value = 0;

    private void ForwardButtonClick(object sender, RoutedEventArgs e)
    {
        tecc.Snapshot();
        _value++;
        label.Content = _value;
        tecc.RunForwardEffect();
    }

    private void BackwardButtonClick(object sender, RoutedEventArgs e)
    {
        tecc.Snapshot();
        _value--;
        label.Content = _value;
        tecc.RunBackwardEffect();
    }

    private void ReloadButtonClick(object sender, RoutedEventArgs e)
    {
        tecc.Snapshot();
        tecc.RunReloadEffect();
    }
}
