namespace EnkuToolkit.UiIndependent.Items;

/// <summary>
/// CustamizableCalendarのSourceプロパティで使用するデータ。
/// クライアント側で継承して使用することを想定している。
/// </summary>
public class CalendarSource
{
    /// <summary>
    /// 対象の日付を表すプロパティ
    /// </summary>
    public DateTime Date { get; init; }

    /// <summary>
    /// Dateプロパティが土曜日か日曜日の場合trueとなるプロパティ
    /// </summary>
    public bool IsHoliday => this.Date.DayOfWeek == DayOfWeek.Sunday || 
                             this.Date.DayOfWeek == DayOfWeek.Saturday ?
                             true : false;

    /// <summary>
    /// Dateプロパティが今日の日付の場合trueとなるプロパティ
    /// </summary>
    public bool IsToday => this.Date == DateTime.Today;

    /// <summary>
    /// 本オブジェクトのデータを表示するListBoxItemのIsEnabledを有効化するかを表すプロパティ
    /// </summary>
    public bool IsEnabled { get; init; } = true;

    /// <summary>
    /// セル表示用のテンプレートを表すプロパティ
    /// CustamizableCalendarの機構上や仕方なく追加したプロパティのためnullにしておいてください。
    /// </summary>
    public object? TargetTemplate { get; set; }
}