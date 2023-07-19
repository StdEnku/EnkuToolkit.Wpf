namespace Sandbox;

using EnkuToolkit.UiIndependent.Services;
using EnkuToolkit.Wpf.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using EnkuToolkit.Wpf.MarkupExtensions;
using EnkuToolkit.UiIndependent.Attributes;
using Sandbox.Services;
using EnkuToolkit.Wpf.Utils;

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
        var diRegisterAttribAttachedTypes = DiRegisterUtil.AllDiRegisterAttributeAttachedTypes();

        foreach (var AttachedTypeInfo in diRegisterAttribAttachedTypes)
        {
            var mode = AttachedTypeInfo.Mode;
            var type = AttachedTypeInfo.Type;
            if (mode == DiRegisterMode.Transient) serviceCollection.AddTransient(type);
            else if (mode == DiRegisterMode.Scoped) serviceCollection.AddTransient(type);
            else if (mode == DiRegisterMode.Singleton) serviceCollection.AddSingleton(type);
        }
    }
}