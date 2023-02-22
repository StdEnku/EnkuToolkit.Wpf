namespace EnkuToolkit.Wpf.Behaviors;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

/// <summary>
/// Application.Current.MainWindowがNavigateionWindowの場合のみ使用可能な
/// 添付対象のページへの画面遷移後に実行可能なコマンドを指定できる添付ビヘイビア。
/// 以前表示していたページからのデータの受け取りを行うことを想定している。
/// </summary>
public class PageNavigatedCommandBehavior
{
    /// <summary>
    /// 画面遷移後に実行可能なコマンドを指定するための添付プロパティ
    /// </summary>
    public static readonly DependencyProperty NavigatedCommandProperty
        = DependencyProperty.RegisterAttached(
            "NavigatedCommand",
            typeof(ICommand),
            typeof(PageNavigatedCommandBehavior),
            new PropertyMetadata(null, onNavigatedCommandChanged)
        );

    /// <summary>
    /// NavigatedCommandProperty添付プロパティのセッター
    /// </summary>
    /// <param name="targetPage">添付対象のPageオブジェクト</param>
    /// <param name="value">画面遷移時に実行したいICommandオブジェクト</param>
    public static void SetNavigatedCommand(Page targetPage, ICommand value)
        => targetPage.SetValue(NavigatedCommandProperty, value);

    /// <summary>
    /// NavigatedCommandProperty添付プロパティのゲッター
    /// </summary>
    /// <param name="targetPage">添付対象のPageオブジェクト</param>
    /// <returns>画面遷移時に実行されるICommandオブジェクト</returns>
    public static ICommand? GetNavigatedCommand(Page targetPage)
        => targetPage.GetValue(NavigatedCommandProperty) as ICommand;

    private static void onNavigatedCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var page = (Page)d;
        var nw = (NavigationWindow)Application.Current.MainWindow;
        var ns = nw.NavigationService;
        ns.Navigated += onTargetNavigated;
    }

    private static void onTargetNavigated(object sender, NavigationEventArgs e)
    {
        var ns = (NavigationWindow)sender;
        var page = (Page)e.Content;
        var extraData = e.ExtraData;
        var command = GetNavigatedCommand(page);

        if (command?.CanExecute(extraData) ?? false)
            command.Execute(extraData);

        ns.Navigated -= onTargetNavigated;
    }
}