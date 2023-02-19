namespace EnkuToolkit.Wpf.Behaviors;

using System;
using System.Windows;
using System.Windows.Shell;

/// <summary>
/// CustomTitlebarWindowまたはCustomTitlebarAnimatedNavigationWindowの
/// タイトルバー内で使用するボタン等のIInputElementを実装したオブジェクトが
/// クリック可能か指定するためのビヘイビア
/// </summary>
/// <remarks>
/// このビヘイビアは名前空間を統一するために実装されているだけで実態は
/// WindowChrome.IsHitTestVisibleInChromeの薄いラッパーです。
/// </remarks>
public class TitlebarComponentsBehavior
{
    /// <summary>
    /// 以前の状態を保存するか指定するための添付プロパティ
    /// </summary>
    public static readonly DependencyProperty IsHitTestVisibleProperty
        = DependencyProperty.RegisterAttached(
            "IsHitTestVisible",
            typeof(bool),
            typeof(TitlebarComponentsBehavior),
            new PropertyMetadata(false, onIsHitTestVisibleChanged)
        );

    /// <summary>
    /// IsHitTestVisibleProperty添付プロパティのセッター
    /// </summary>
    /// <param name="inputElement">対象のIInputElementを実装したDependencyObject</param>
    /// <param name="value">セットしたい値</param>
    public static void SetIsHitTestVisible(IInputElement inputElement, bool value)
    {
        var attachedObject = inputElement as DependencyObject;

        if (attachedObject is null)
            throw new ArgumentException("The element must be a DependencyObject", "inputElement");

        attachedObject.SetValue(IsHitTestVisibleProperty, value);
    }

    /// <summary>
    /// IsHitTestVisibleProperty添付プロパティのゲッター
    /// </summary>
    /// <param name="inputElement">対象のIInputElementを実装したDependencyObject</param>
    /// <returns>IsStateSaveProperty添付プロパティの値</returns>
    public static bool GetIsHitTestVisible(IInputElement inputElement)
    {
        var attachedObject = inputElement as DependencyObject;

        if (attachedObject is null)
            throw new ArgumentException("The element must be a DependencyObject", "inputElement");

        return (bool)attachedObject.GetValue(IsHitTestVisibleProperty);
    }

    private static void onIsHitTestVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var titlebarContent = (IInputElement)d;
        var value = (bool)e.NewValue;
        WindowChrome.SetIsHitTestVisibleInChrome(titlebarContent, value);
    }
}