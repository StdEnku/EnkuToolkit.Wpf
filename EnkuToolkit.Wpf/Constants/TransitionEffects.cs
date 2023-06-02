/*
 * Copyright (c) 2022 StdEnku
 *
 * This software is provided 'as-is', without any express or implied warranty.
 * In no event will the authors be held liable for any damages arising from the use of this software.
 *
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would be
 *    appreciated but is not required.
 *
 * 2. Altered source versions must be plainly marked as such, and must not be
 *    misrepresented as being the original software.
 *
 * 3. This notice may not be removed or altered from any source distribution.
 */
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