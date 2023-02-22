namespace EnkuToolkit.Wpf.Behaviors;

using System.Windows.Navigation;
using System.Windows;

/// <summary>
/// Application.Current.MainWindowがNavigationWindowの場合のみ使用可能な
/// 添付対象のページへの画面遷移後に実行可能なコマンドを指定できる添付ビヘイビア。
/// 以前表示していたページからのデータの受け取りを行うことを想定している。
/// </summary>
public class MainNavigationWindowNavigationCommandBehavior : AbstractNavigatedCommandBehavior<MainNavigationWindowNavigationCommandBehavior>
{
    /// <summary>
    /// 画面遷移の対象となるFrameやNavigationWindowのNavigationServiceプロパティを返すプロパティ
    /// </summary>
    protected override NavigationService TargetNavigationService
    {
        get
        {
            var nw = (NavigationWindow)Application.Current.MainWindow;
            return nw.NavigationService;
        }
    }
}
