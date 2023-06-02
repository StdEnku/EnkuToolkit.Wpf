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
namespace EnkuToolkit.Wpf.Services;

using System;
using System.Windows;
using System.Windows.Navigation;

/// <summary>
/// NavigationService available only when Application.Current.MainWindow is NavigationWindow
/// </summary>
public class MainNavigationWindowNavigationService : AbstractNavigationService
{
    /// <summary>
    /// Property that casts Application.Current.MainWindow to NavigationWindow and returns its NavigationServcie property
    /// </summary>
    /// <exception cref="InvalidCastException">Thrown when Application.Current.MainWindow cannot be cast to NavigationWindow.</exception>
    protected override NavigationService TargetNavigationService
    {
        get
        {
            var nw = Application.Current.MainWindow as NavigationWindow;
            if (nw is null) throw new InvalidCastException("Application.Current.MainWindow cannot be cast to NavigationWindow.");
            return nw.NavigationService;
        }
    }
}