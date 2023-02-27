namespace EnkuToolkit.Wpf.Demo;

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using EnkuToolkit.Wpf.Services;
using EnkuToolkit.UiIndependent.Services;
using EnkuToolkit.Wpf.Demo.ViewModels;
using EnkuToolkit.Wpf.MarkupExtensions;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application, IServicesOwner
{
    public App()
    {
        this.Services = ConfigureServices();
        this.InitializeComponent();
    }

    public new static App Current => (App)Application.Current;

    public IServiceProvider Services { get; }

    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddTransient<Page1ViewModel>();
        services.AddTransient<Page2ViewModel>();
        services.AddTransient<Page3ViewModel>();
        services.AddTransient<INavigationService, MainNavigationWindowNavigationService>();
        services.AddTransient<IMessageBoxService, MessageBoxService>();
        services.AddTransient<IApplicationPropertyiesService, ApplicationPropertyiesService>();

        return services.BuildServiceProvider();
    }
}
