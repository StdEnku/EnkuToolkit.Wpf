namespace Sandbox.Views;

using Sandbox.ViewModels;
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
/// MainWindow9.xaml の相互作用ロジック
/// </summary>
public partial class MainWindow9 : Window
{
    public MainWindow9()
    {
        InitializeComponent();
        DataContext = new Page9ViewModel();
        Loaded += MainWindow9_Loaded;
    }

    private void MainWindow9_Loaded(object sender, RoutedEventArgs e)
    {
        var vm = (Page9ViewModel)DataContext;
        vm.OnLoaded();
    }
}
