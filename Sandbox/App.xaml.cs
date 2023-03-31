namespace Sandbox;

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using EnkuToolkit.Wpf.Services;
using EnkuToolkit.UiIndependent.Services;
using EnkuToolkit.Wpf.MarkupExtensions;
using Sandbox.ViewModels;

public partial class App : Application, IServicesOwner
{
    public App()
    {
        this.Services = ConfigureServices();
        AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;
        this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        this.InitializeComponent();
    }

    public new static App Current => (App)Application.Current;

    public IServiceProvider Services { get; }

    // DIコンテナへの登録用メソッド
    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddSingleton<Page1ViewModel>();
        services.AddTransient<INavigationService, MainNavigationWindowNavigationService>();
        services.AddTransient<IMessageBoxService, MessageBoxService>();
        services.AddTransient<IApplicationPropertyiesService, ApplicationPropertyiesService>();

        return services.BuildServiceProvider();
    }

    // 
    private void CurrentDomain_FirstChanceException(object sender, System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e)
    {
        string errorMember = e.Exception.TargetSite.Name;
        string errorMessage = e.Exception.Message;
        string message = string.Format($"例外が{errorMember}で発生。\nプログラムは継続します。\nエラーメッセージ：{errorMessage}");
        MessageBox.Show(message, "FirstChanceException", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    // UIスレッドで発生した例外の補足用メソッド
    private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
        string errorMember = e.Exception.TargetSite.Name;
        string errorMessage = e.Exception.Message;
        string message = string.Format($"例外が{errorMember}で発生。\nプログラムを継続しますか？\nエラーメッセージ：{errorMessage}");
        MessageBoxResult result = MessageBox.Show(message, "DispatcherUnhandledException", MessageBoxButton.YesNo, MessageBoxImage.Warning);
        if (result == MessageBoxResult.Yes)
            e.Handled = true;
    }
}