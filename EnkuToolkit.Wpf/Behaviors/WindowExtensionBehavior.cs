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
namespace EnkuToolkit.Wpf.Behaviors;

using System;
using System.Windows;
using System.IO;
using System.Diagnostics;
using System.Text.Json;

/// <summary>
/// Attached behaviors to extend Window
/// </summary>
public class WindowExtensionBehavior
{
    /// <summary>
    /// Attached property to specify the path to a Json save file to save the window position and height and WindowState properties on exit and load them on next startup
    /// </summary>
    public static readonly DependencyProperty StateSavePathProperty
        = DependencyProperty.RegisterAttached(
            "StateSavePath",
            typeof(string),
            typeof(WindowExtensionBehavior),
            new PropertyMetadata(string.Empty, OnStateSavePathChanged)
        );

    /// <summary>
    /// Getter for StateSavePathProperty, an attached property to specify the path to the Json save file for saving the window position and height and WindowState properties at exit and loading them at next startup
    /// </summary>
    /// <param name="target">Window to be attached</param>
    /// <returns>String of destination Path</returns>
    public static string GetStateSavePath(Window target)
        => (string)target.GetValue(StateSavePathProperty);

    /// <summary>
    /// Setter for StateSavePathProperty, an attached property to specify the path to the Json save file for saving the window position and height and WindowState properties at exit and loading them at next startup
    /// </summary>
    /// <param name="target">Window to be attached</param>
    /// <param name="value">String of destination Path</param>
    public static void SetStateSavePath(Window target, string value)
        => target.SetValue(StateSavePathProperty, value);

    private static void OnStateSavePathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var window = (Window)d;
        window.Initialized += OnWindowInitialized;
        window.Closed += OnWindowClosed;
    }

    private static void OnWindowInitialized(object? sender, EventArgs e)
    {
        var window = sender as Window;
        Debug.Assert(window is not null);

        var path = GetStateSavePath(window);
        if (File.Exists(path))
        {
            WindowSaveData? windowSaveData;
            try
            {
                var jsonString = File.ReadAllText(path);
                windowSaveData = JsonSerializer.Deserialize<WindowSaveData>(jsonString);
            }
            catch (Exception)
            {
                File.Delete(path);
                return;
            }
        
            if (windowSaveData is null)
            {
                File.Delete(path);
                return;
            }

            window.Height = windowSaveData.Height;
            window.Width = windowSaveData.Width;
            window.Top = windowSaveData.Top;
            window.Left = windowSaveData.Left;
            window.WindowState = windowSaveData.IsMaximized ? WindowState.Maximized : WindowState.Normal;

            window.Initialized -= OnWindowInitialized;
            window.Closed -= OnWindowClosed;
        }
    }

    private static void OnWindowClosed(object? sender, EventArgs e)
    {
        var window = sender as Window;
        Debug.Assert(window is not null);
        var path = GetStateSavePath(window);

        var saveData = new WindowSaveData()
        {
            Height = window.Height,
            Width = window.Width,
            Top = window.Top,
            Left = window.Left,
            IsMaximized = window.WindowState == WindowState.Maximized,
        };

        var jsonString = JsonSerializer.Serialize(saveData);
        File.WriteAllText(path, jsonString);
    }

    private class WindowSaveData
    {
        public double Height { get; set; }

        public double Width { get; set; }

        public double Top { get; set; }

        public double Left { get; set; }

        public bool IsMaximized { get; set; }
    }
}