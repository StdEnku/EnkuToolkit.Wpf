namespace Sandbox.Views;

using EnkuToolkit.Wpf.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
/// MainWindow.xaml の相互作用ロジック
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        customizableCalendar.CellsUpdated += CustomizableCalendar_CellsUpdated;
        customizableCalendar.CellsUpdating += CustomizableCalendar_CellsUpdating;
    }

    private void CustomizableCalendar_CellsUpdating(object sender, CustomizableCalendar.CellsUpdateEventArgs e)
    {
        Debug.WriteLine($"CellsUpdating : {e.UpdateMode.ToString()}");
    }

    private void CustomizableCalendar_CellsUpdated(object sender, CustomizableCalendar.CellsUpdateEventArgs e)
    {
        Debug.WriteLine($"CellsUpdated : {e.UpdateMode.ToString()}");
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        customizableCalendar.Update();
    }
}
