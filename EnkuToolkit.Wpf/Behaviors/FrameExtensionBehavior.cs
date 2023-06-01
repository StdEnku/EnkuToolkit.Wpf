namespace EnkuToolkit.Wpf.Behaviors;

using System.Windows.Controls;
using System.Windows;
using System.Windows.Navigation;
using SystemNavigationMode = System.Windows.Navigation.NavigationMode;
using EnkuNavigationMode = UiIndependent.Navigation.NavigationMode;
using EnkuToolkit.UiIndependent.Navigation;

/// <summary>
/// Attached behaviors to extend Frame
/// </summary>
public class FrameExtensionBehavior
{
    /// <summary>
    /// Attached property to specify whether or not to pass parameters for screen transition by executing its OnNavigated method when the destination ViewModel is castable to INavigationAware after screen transition.
    /// </summary>
    public static readonly DependencyProperty IsSendNavigationParamProperty
        = DependencyProperty.RegisterAttached(
            "IsSendNavigationParam",
            typeof(bool),
            typeof(FrameExtensionBehavior),
            new PropertyMetadata(false, onIsSendChanged)
        );

    /// <summary>
    /// Setter for IsSendNavigationParamProperty, an attached property to specify whether or not to pass parameters for screen transition by executing its OnNavigated method when the destination ViewModel is castable to INavigationAware after screen transition.
    /// </summary>
    /// <param name="target">Target Frame</param>
    /// <param name="value">
    /// If true, the OnNavigated method is executed.
    /// If false, the OnNavigated method is not executed.
    /// </param>
    public static void SetIsSendNavigationParam(Frame target, bool value)
        => target.SetValue(IsSendNavigationParamProperty, value);

    /// <summary>
    /// Getter for IsSendNavigationParamProperty, an attached property to specify whether or not to pass parameters for screen transition by executing its OnNavigated method when the destination ViewModel is castable to INavigationAware after screen transition.
    /// </summary>
    /// <param name="target">Target Frame</param>
    /// <returns>
    /// If true, the OnNavigated method is executed.
    /// If false, the OnNavigated method is not executed.
    /// </returns>
    public static bool GetIsSendNavigationParam(Frame target)
        => (bool)target.GetValue(IsSendNavigationParamProperty);

    private static void onIsSendChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var value = (bool)e.NewValue;
        var frame = (Frame)d;
        if (!value) return;
        frame.Navigating += OnFrameNavigating;
    }

    private static void OnFrameNavigating(object sender, NavigatingCancelEventArgs e)
    {
        var frame = (Frame)sender;
        var isSendNavigationParam = GetIsSendNavigationParam(frame);
        var navigationMode = e.NavigationMode;

        if (!isSendNavigationParam) return;

        NavigatedEventHandler? handler = null;
        handler = (sender, e) => 
        {
            var frame = (Frame)sender;
            var extraData = e.ExtraData;
            var nextPage = (Page)e.Content;

            if (nextPage.DataContext is INavigationAware navigationAware)
            {
                var nm = navigationMode == SystemNavigationMode.New ? EnkuNavigationMode.New :
                         navigationMode == SystemNavigationMode.Forward ? EnkuNavigationMode.Forward :
                         navigationMode == SystemNavigationMode.Back ? EnkuNavigationMode.Back :
                         EnkuNavigationMode.Refresh;
                navigationAware.OnNavigated(extraData, nm);
            }
            frame.Navigated -= handler;
        };

        frame.Navigated += handler;
    }
}