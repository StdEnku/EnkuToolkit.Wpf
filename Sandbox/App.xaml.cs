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
}