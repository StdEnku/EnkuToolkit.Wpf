namespace EnkuToolkit.Wpf.Behaviors;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

/// <summary>
/// ListBoxを拡張するためのビヘイビア
/// </summary>
public class ListBoxExpansionsBehavior
{
    #region IsEnable添付プロパティ
    /// <summary>
    /// 本ビヘイビアを有効かするか指定するための添付プロパティ
    /// </summary>
    public static readonly DependencyProperty IsEnableExpansionsProperty
        = DependencyProperty.RegisterAttached(
            "IsEnableExpansions",
            typeof(bool),
            typeof(ListBoxExpansionsBehavior),
            new PropertyMetadata(false, onIsEnableChanged)
        );

    /// <summary>
    /// IsEnable添付プロパティ用のゲッター
    /// </summary>
    public static bool GetIsEnableExpansions(ListBox target)
        => (bool)target.GetValue(IsEnableExpansionsProperty);

    /// <summary>
    /// IsEnable添付プロパティ用のセッター
    /// </summary>
    public static void SetIsEnableExpansions(ListBox target, bool value)
        => target.SetValue(IsEnableExpansionsProperty, value);

    private static void onIsEnableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var target = (ListBox)d;
        var value = (bool)e.NewValue;

        if (value)
            target.SelectionChanged += onTargetSelectionChanged;
        else 
            target.SelectionChanged -= onTargetSelectionChanged;
    }

    private static void onTargetSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var target = (ListBox)sender;
        var selectedItems = target.SelectedItems;
        target.SetValue(BindableSelectedSourcesProperty, selectedItems);
    }
    #endregion

    #region BindableSelectedSources添付プロパティ
    /// <summary>
    /// 選択済みのソースを取得するための添付プロパティ
    /// 本添付プロパティはバインド可能ですがセッターは機能しないのでバインディングモードに注意してください。
    /// </summary>
    public static readonly DependencyProperty BindableSelectedSourcesProperty
        = DependencyProperty.RegisterAttached(
            "BindableSelectedSources",
            typeof(IList),
            typeof(ListBoxExpansionsBehavior),
            new PropertyMetadata(new List<object>())
        );

    /// <summary>
    /// BindableSelectedSourcesProperty用のゲッター
    /// </summary>
    public static IList GetBindableSelectedSources(ListBox target)
        => (IList)target.GetValue(BindableSelectedSourcesProperty);

    /// <summary>
    /// BindableSelectedSourcesProperty用のセッター
    /// </summary>
    public static void SetBindableSelectedSources(ListBox target, IList value)
        => throw new InvalidOperationException("The ListBoxExpansionsBehavior.BindableSelectedSources attachment property only supports OneWayToSource binding mode.");
    #endregion
}