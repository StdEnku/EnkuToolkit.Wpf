namespace EnkuToolkit.Wpf.Behaviors;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

/// <summary>
/// 添付対象のページへの画面遷移後に実行可能なコマンドを指定できる添付ビヘイビア。
/// 以前表示していたページからのデータの受け取りを行うことを想定している。
/// 継承してカスタマイズする際はTargetNavigationServiceプロパティのゲッターをオーバーライドして
/// 画面遷移の対象となるFrameやNavigationWindowのNavigationServiceプロパティを返す処理を記してください。
/// </summary>
/// <typeparam name="TInheritance">継承先の型</typeparam>
public abstract class AbstractNavigatedCommandBehavior<TInheritance> 
    where TInheritance : AbstractNavigatedCommandBehavior<TInheritance>, new()
{
    /// <summary>
    /// 画面遷移の対象となるFrameやNavigationWindowのNavigationServiceプロパティを返すプロパティ
    /// </summary>
    protected abstract NavigationService TargetNavigationService { get; }

    /// <summary>
    /// 画面遷移後に実行可能なコマンドを指定するための添付プロパティ
    /// </summary>
    public static readonly DependencyProperty NavigatedCommandProperty
        = DependencyProperty.RegisterAttached(
            "NavigatedCommand",
            typeof(ICommand),
            typeof(AbstractNavigatedCommandBehavior<TInheritance>),
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
        var ncb = new TInheritance();
        ncb.TargetNavigationService.Navigated += onNavigated;
    }

    private static void onNavigated(object sender, NavigationEventArgs e)
    {
        var ncb = new TInheritance();
        var page = (Page)e.Content;
        var extraData = e.ExtraData;
        var command = GetNavigatedCommand(page);

        if (command?.CanExecute(extraData) ?? false)
            command.Execute(extraData);

        ncb.TargetNavigationService.Navigated -= onNavigated;
    }
}