namespace EnkuToolkit.Wpf.Behaviors;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

/// <summary>
/// Attached behaviors to extend ListBox
/// </summary>
public static class ListBoxExtensionBehavior
{
    #region Attachment property representing all selected Contents of the target ListBox
    /// <summary>
    /// Attachment property representing all selected Contents of the target ListBox
    /// </summary>
    public static readonly DependencyProperty SelectedContentsProperty
        = DependencyProperty.RegisterAttached(
            "SelectedContents",  
            typeof(List<object>),
            typeof(ListBoxExtensionBehavior),
            new FrameworkPropertyMetadata(new List<object>(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedContentsChanged)
        );

    /// <summary>
    /// Getter for SelectedContentsProperty, an attached property representing all selected Contents of the target ListBox
    /// </summary>
    /// <param name="target">Target ListBox</param>
    /// <returns>List containing the value of Content for all items currently selected in the ListBox</returns>
    public static List<object> GetSelectedContents(ListBox target)
        => (List<object>)target.GetValue(SelectedContentsProperty);

    /// <summary>
    /// Setter for SelectedContentsProperty, an attached property representing all selected Contents of the target ListBox
    /// </summary>
    /// <param name="target">Target ListBox</param>
    /// <param name="value">List of Content equal to the item to be selected in the ListBox.</param>
    public static void SetSelectedContents(ListBox target, List<object> value)
        => target.SetValue(SelectedContentsProperty, value);
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

        var list = new List<object>();

        foreach (var item in listBox.SelectedItems.Cast<ListBoxItem>())
            list.Add(item.Content);

        SetSelectedContents(listBox, list);

        #endregion
        SetIsListBoxSelectionChanged(listBox, false);
    }

    private static void OnSelectedContentsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
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

        var sourceValue = GetSelectedContents(listBox);

        if (listBox.SelectionMode == SelectionMode.Single && sourceValue.Count > 1)
        {
            SetIsSourceChanged(listBox, false);
            throw new InvalidOperationException("The target ListBox is trying to perform multiple selections even though its SelectionMode is Single. Please review the SelectionMode property or binding mode of the target ListBox or modify the code so that the number of elements in this property does not exceed 1.");
        }

        var items = from item in listBox.Items.Cast<ListBoxItem>()
                    from source in sourceValue
                    where source.Equals(item.Content)
                    select item;

        foreach (var item in items)
            item.IsSelected = true;

        #endregion
        SetIsSourceChanged(listBox, false);
    }
}