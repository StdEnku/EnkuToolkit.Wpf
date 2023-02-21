namespace EnkuToolkit.Wpf.Behaviors;

using System;
using System.Windows;
using System.Windows.Shell;

/// <summary>
/// CustomTitlebarWindowやCustomTitlebarAnimatedNavigationWindowで
/// タイトルバー内のボタンなどをクリック可能にするためのビヘイビア
/// </summary>
public class TitlebarComponentsBehavior
{
    /// <summary>
    /// タイトルバー内のボタンなどをクリック可能にするか指定するための添付プロパティ
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
    /// <param name="inputElement">添付対象のIInputElementを実装したDependencyObject</param>
    /// <param name="value">
    /// タイトルバー内のボタンなどをクリック可能にする場合trueを指定する、
    /// しない場合はfalseを指定する。
    /// </param>
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
    /// <param name="inputElement">添付対象のIInputElementを実装したDependencyObject</param>
    /// <returns>
    /// タイトルバー内のボタンなどをクリック可能にする場合trueを返す、
    /// しない場合はfalseを返す。
    /// </returns>
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