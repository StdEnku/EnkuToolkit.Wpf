namespace Sandbox;

using EnkuToolkit.UiIndependent.Services;
using EnkuToolkit.Wpf.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System;
using System.Windows;
using EnkuToolkit.Wpf.MarkupExtensions;
using System.Linq;
using Sandbox.Services;

public partial class App : Application, IServicesOwner
{
    public App()
    {
        var serviceCollection = new ServiceCollection();
        RegisterDiRegisterAttachedTypes(ref serviceCollection);
        RegisterServices(ref serviceCollection);
        Services = serviceCollection.BuildServiceProvider();
        InitializeComponent();
    }

    public IServiceProvider Services { get; }

    private static void RegisterServices(ref ServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<INavigationService, MainFrameNavigationService>();
        serviceCollection.AddSingleton<IMessageBoxService, MessageBoxService>();
    }

    private static void RegisterDiRegisterAttachedTypes(ref ServiceCollection serviceCollection)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var viewModelTypeWithIsSingletonFlags
            = from type in Assembly.GetExecutingAssembly().GetTypes()
              where type.GetCustomAttributes(typeof(DiRegister)).Count() == 1
              select new { ViewModelType = type, IsSingleton = ((DiRegister)type.GetCustomAttributes(typeof(DiRegister)).First()).IsSingleton };

        foreach (var viewModelTypeWithIsSingletonFlag in viewModelTypeWithIsSingletonFlags)
        {
            if (viewModelTypeWithIsSingletonFlag.IsSingleton)
                serviceCollection.AddSingleton(viewModelTypeWithIsSingletonFlag.ViewModelType);
            else
                serviceCollection.AddTransient(viewModelTypeWithIsSingletonFlag.ViewModelType);
        }
    }
}

public class DiRegister : Attribute
{
    public bool IsSingleton { get; }

    public DiRegister(bool isSingleton = false) => IsSingleton = isSingleton;
}