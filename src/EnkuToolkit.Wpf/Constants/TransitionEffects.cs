namespace EnkuToolkit.Wpf.Constants;

/// <summary>
/// Enum value to specify the effect to be executed when the state changes
/// </summary>
public enum TransitionEffects
{
    /// <summary>
    /// Effects are not executed.
    /// </summary>
    None,

    /// <summary>
    /// Perform horizontal Sliding effects
    /// </summary>
    HorizontalSlide,

    /// <summary>
    /// Perform vertical Sliding effects
    /// </summary>
    VerticalSlide,

    /// <summary>
    /// Perform horizontal Modernsliding effects
    /// </summary>
    HorizontalModernSlide,

    /// <summary>
    /// Perform vertical Modernsliding effects
    /// </summary>
    VerticalModernSlide,

    /// <summary>
    /// Performing the Fade effect
    /// </summary>
    FadeOnly,

    /// <summary>
    /// Perform custom effects
    /// </summary>
    Custom,
}