namespace _02_move_control;

using EnkuToolkit.UiIndependent.Services;
using EnkuToolkit.Wpf.MarkupExtensions;
using EnkuToolkit.Wpf.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using System.Windows;

public partial class App : Application, IServicesOwner
{
    public App()
    {
        Services = ConfigureServices();
        InitializeComponent();
    }

    public IServiceProvider Services { get; }

    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        // register services
        services.AddSingleton<INavigationService, MainNavigationWindowNavigationService>();
        services.AddSingleton<IMessageBoxService, MessageBoxService>();

        // register view models
        var assembly = Assembly.GetExecutingAssembly();
        var viewModelTypeWithIsSingletonFlags
            = from type in Assembly.GetExecutingAssembly().GetTypes()
              where type.GetCustomAttributes(typeof(ViewModelAttribute)).Count() == 1
              select new { ViewModelType = type, IsSingleton = ((ViewModelAttribute)type.GetCustomAttributes(typeof(ViewModelAttribute)).First()).IsSingleton };

        foreach (var viewModelTypeWithIsSingletonFlag in viewModelTypeWithIsSingletonFlags)
        {
            if (viewModelTypeWithIsSingletonFlag.IsSingleton)
                services.AddSingleton(viewModelTypeWithIsSingletonFlag.ViewModelType);
            else
                services.AddTransient(viewModelTypeWithIsSingletonFlag.ViewModelType);
        }

        return services.BuildServiceProvider();
    }
}

public class ViewModelAttribute : Attribute
{
    public bool IsSingleton { get; }

    public ViewModelAttribute(bool isSingleton = false) => IsSingleton = isSingleton;
}