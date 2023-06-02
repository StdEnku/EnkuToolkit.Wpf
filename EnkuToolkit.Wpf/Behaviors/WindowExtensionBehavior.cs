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