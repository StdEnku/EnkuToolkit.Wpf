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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sandbox.Views
{
    /// <summary>
    /// Page2.xaml の相互作用ロジック
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Page2はダブルクリックされました");
        }

        private void toPage1_Click(object sender, RoutedEventArgs e)
        {
            var baseUri = new Uri("pack://application:,,,/Views/", UriKind.Absolute);
            var uri = new Uri(baseUri, "Page1.xaml");
            NavigationService.Navigate(uri);
        }

        private void goBack_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
        }

        private void goForward_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoForward)
                NavigationService.GoForward();
        }

        private void reflesh_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Refresh();
        }
    }
}
