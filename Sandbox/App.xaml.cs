namespace Sandbox;

using EnkuToolkit.UiIndependent.Attributes;
using EnkuToolkit.UiIndependent.Services;
using EnkuToolkit.Wpf.MarkupExtensions;
using EnkuToolkit.Wpf.Services;
using EnkuToolkit.Wpf.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sandbox.Services;
using System;
using System.Windows;

public partial class App : Application, IServicesOwner
{
    private static readonly IHost _host = Host
        .CreateDefaultBuilder()
        .ConfigureServices(ConfigureServices)
        .Build();

    private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        // Register AppHostedService
        services.AddHostedService<AppHostedService>();

        // Register ViewServices
        services.AddSingleton<IMessageBoxService, MessageBoxService>();

        // Register the attached type of DiRegisterAttribute to the DI container
        var diRegisterAttribAttachedTypes = DiRegisterUtil.AllDiRegisterAttributeAttachedTypes();

        foreach (var AttachedTypeInfo in diRegisterAttribAttachedTypes)
        {
            var mode = AttachedTypeInfo.Mode;
            var type = AttachedTypeInfo.Type;
            if (mode == DiRegisterMode.Transient) services.AddTransient(type);
            else if (mode == DiRegisterMode.Scoped) services.AddTransient(type);
            else if (mode == DiRegisterMode.Singleton) services.AddSingleton(type);
        }
    }

    public IServiceProvider Services => _host.Services;

    private async void OnStartup(object sender, StartupEventArgs e)
    {
        await _host.StartAsync();
    }

    private async void OnExit(object sender, ExitEventArgs e)
    {
        await _host.StopAsync();
        _host.Dispose();
    }
}