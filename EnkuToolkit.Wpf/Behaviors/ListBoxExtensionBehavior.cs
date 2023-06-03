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
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

/// <summary>
/// Attached behaviors to extend ListBox
/// </summary>
public static class ListBoxExtensionBehavior
{
    #region Mutually bindable SelectedItems attachment properties
    /// <summary>
    ///  Mutually bindable SelectedItems attachment properties
    /// </summary>
    public static readonly DependencyProperty BindableSelectedItemsProperty
        = DependencyProperty.RegisterAttached(
            "BindableSelectedItems",  
            typeof(IList),
            typeof(ListBoxExtensionBehavior),
            new FrameworkPropertyMetadata(new List<object>(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnBindableSelectedItemsChanged)
        );

    /// <summary>
    ///  Getter for BindableSelectedItemsProperty, a mutually bindable SelectedItems attachment property
    /// </summary>
    /// <param name="target">Target ListBox</param>
    /// <returns>All items currently selected in the target ListBox</returns>
    public static IList GetBindableSelectedItems(ListBox target)
        => (IList)target.GetValue(BindableSelectedItemsProperty);

    /// <summary>
    /// Setter for BindableSelectedItemsProperty, a mutually bindable SelectedItems attachment property
    /// </summary>
    /// <param name="target">Target ListBox</param>
    /// <param name="value">List containing the items you want to make selected</param>
    public static void SetBindableSelectedItems(ListBox target, List<object> value)
        => target.SetValue(BindableSelectedItemsProperty, value);
    #endregion

    #region A group of attached properties used by the internal mechanism
    private static readonly DependencyProperty IsRegisteredProperty
        = DependencyProperty.RegisterAttached(
            "IsRegistered",
            typeof(bool),
            typeof(ListBoxExtensionBehavior),
            new PropertyMetadata(false)
        );

    private static readonly DependencyProperty IsListBoxSelectionChangedProperty
        = DependencyProperty.RegisterAttached(
            "IsListBoxSelectionChanged",
            typeof(bool),
            typeof(ListBoxExtensionBehavior),
            new PropertyMetadata(false)
        );

    private static readonly DependencyProperty IsSourceChangedProperty
        = DependencyProperty.RegisterAttached(
            "IsSourceChanged",
            typeof(bool),
            typeof(ListBoxExtensionBehavior),
            new PropertyMetadata(false)
        );

    private static bool GetIsRegistered(ListBox target)
        => (bool)target.GetValue(IsRegisteredProperty);

    private static void SetIsRegistered(ListBox target, bool value)
        => target.SetValue(IsRegisteredProperty, value);

    private static bool GetIsListBoxSelectionChanged(ListBox target)
        => (bool)target.GetValue(IsListBoxSelectionChangedProperty);

    private static void SetIsListBoxSelectionChanged(ListBox target, bool value)
        => target.SetValue(IsListBoxSelectionChangedProperty, value);

    private static bool GetIsSourceChanged(ListBox target)
        => (bool)target.GetValue(IsSourceChangedProperty);

    private static void SetIsSourceChanged(ListBox target, bool value)
        => target.SetValue(IsSourceChangedProperty, value);
    #endregion

    private static void OnTargetListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var listBox = (ListBox)sender;
        var isSourceChanged = GetIsSourceChanged(listBox);
        if (isSourceChanged) return;
        SetIsListBoxSelectionChanged(listBox, true);
        #region The part that is executed only when the selection on the target ListBox side changes from here
        var list = from item in listBox.SelectedItems.Cast<object>()
                   select item is ListBoxItem listBoxItem ? listBoxItem.Content : item;

        SetBindableSelectedItems(listBox, list.ToList());
        #endregion
        SetIsListBoxSelectionChanged(listBox, false);
    }

    private static void OnBindableSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var listBox = (ListBox)d;

        var isRegistered = GetIsRegistered(listBox);
        if (!isRegistered)
        {
            listBox.SelectionChanged += OnTargetListBoxSelectionChanged;
            SetIsRegistered(listBox, true);
        }

        var isListBoxSelectionChanged = GetIsListBoxSelectionChanged(listBox);
        if (isListBoxSelectionChanged) return;
        SetIsSourceChanged(listBox, true);
        #region The part to be executed only if the binding source changes from here

        var sourceValue = GetBindableSelectedItems(listBox);

        if (listBox.SelectionMode == SelectionMode.Single && sourceValue.Count > 1)
        {
            SetIsSourceChanged(listBox, false);
            throw new InvalidOperationException("The target ListBox is trying to perform multiple selections even though its SelectionMode is Single. Please review the SelectionMode property or binding mode of the target ListBox or modify the code so that the number of elements in this property does not exceed 1.");
        }

        var nextItems = from item in listBox.Items.Cast<object>()
                        from source in sourceValue.Cast<object>()
                        where EqualsLogic(item, source)
                        select item;

        listBox.SelectedItems.Clear();
        foreach (var item in nextItems)
            listBox.SelectedItems.Add(item);

        #endregion
        SetIsSourceChanged(listBox, false);
    }

    private static bool EqualsLogic(object item, object source)
    {
        var result = item is ListBoxItem listBoxItem ? 
            source.Equals(listBoxItem.Content) : 
            source.Equals(item);

        return result;
    }
}