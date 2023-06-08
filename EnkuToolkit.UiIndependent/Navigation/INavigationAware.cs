/*
 * MIT License
 * 
 * Copyright (c) 2023 StdEnku
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
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