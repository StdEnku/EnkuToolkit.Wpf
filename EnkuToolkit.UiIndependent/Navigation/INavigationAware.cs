namespace EnkuToolkit.UiIndependent.Navigation;

/// <summary>
/// Screen Transition Types
/// </summary>
public enum NavigationMode
{
    /// <summary>
    /// Navigation is to a new instance of a page (not going forward or backward in the stack).
    /// </summary>
    New,

    /// <summary>
    /// Navigation is going backward in the stack.
    /// </summary>
    Back,

    /// <summary>
    /// Navigation is going forward in the stack.
    /// </summary>
    Forward,

    /// <summary>
    /// Navigation is to the current page (perhaps with different data).
    /// </summary>
    Refresh,
}

/// <summary>
/// Interface that describes methods to be executed on the ViewModel after screen transition when the IsSendNavigationParam attached property of FrameExtensionBehavior or NavigationWindowExtensionBehavior is True.
/// </summary>
public interface INavigationAware
{
    /// <summary>
    /// Methods executed after screen transitions
    /// </summary>
    /// <param name="param">Parameters passed from previous screens</param>
    /// <param name="navigationMode">Mode of Navigation</param>
    void OnNavigated(object? param, NavigationMode navigationMode);
}