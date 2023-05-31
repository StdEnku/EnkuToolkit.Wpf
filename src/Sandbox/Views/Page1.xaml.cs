using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace Sandbox.Views
{
    /// <summary>
    /// Page1.xaml の相互作用ロジック
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Page1はダブルクリックされました");
        }

        public static T GetParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject dependencyObject = VisualTreeHelper.GetParent(child);

            if (dependencyObject != null)
            {
                T parent = dependencyObject as T;
                if (parent != null)
                {
                    return parent;
                }
                else
                {
                    return GetParent<T>(dependencyObject);
                }
            }
            else
            {
                return null;
            }
        }

        private void toPage2_Click(object sender, RoutedEventArgs e)
        {
            var f = GetParent<NavigationWindow>(this);
            var baseUri = new Uri("pack://application:,,,/Views/", UriKind.Absolute);
            var uri = new Uri(baseUri, "Page2.xaml");
            f.Navigate(uri, "From Page1");
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
