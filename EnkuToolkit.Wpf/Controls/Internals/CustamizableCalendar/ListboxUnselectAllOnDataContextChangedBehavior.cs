namespace EnkuToolkit.Wpf.Controls.Internals.CustamizableCalendar;

using System.Windows;
using System.Windows.Controls;

internal class ListboxUnselectAllOnDataContextChangedBehavior
{
    public static readonly DependencyProperty IsApplyProperty
        = DependencyProperty.RegisterAttached(
            "IsApply",
            typeof(bool),
            typeof(ListboxUnselectAllOnDataContextChangedBehavior),
            new PropertyMetadata(false, onIsApplyChanged)
        );

    public static void SetIsApply(ListBox target, bool value)
        => target.SetValue(IsApplyProperty, value);

    public static bool GetIsApply(ListBox target)
        => (bool)target.GetValue(IsApplyProperty);

    private static void onIsApplyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var listBox = (ListBox)d;
        var value = (bool)e.NewValue;
        if (value)
            listBox.DataContextChanged += ListBox_DataContextChanged;
        else
            listBox.DataContextChanged -= ListBox_DataContextChanged;
    }

    private static void ListBox_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        var listBox = (ListBox)sender;
        listBox.UnselectAll();
    }
}