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