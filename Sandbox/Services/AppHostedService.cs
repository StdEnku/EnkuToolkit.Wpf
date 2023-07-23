namespace Sandbox.Services;

using Microsoft.Extensions.Hosting;
using Sandbox.Views;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

public class AppHostedService : IHostedService
{
    private MainWindow _mainWindow;

    public AppHostedService(IServiceProvider serviceProvider)
    {
        var mainWindow = serviceProvider.GetService(typeof(MainWindow)) as MainWindow;
        Debug.Assert(mainWindow is not null);
        _mainWindow = mainWindow;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _mainWindow.Show();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}