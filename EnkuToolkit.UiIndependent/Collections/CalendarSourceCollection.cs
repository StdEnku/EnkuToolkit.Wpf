namespace EnkuToolkit.UiIndependent.Collections;

using System.Collections.ObjectModel;
using System.Collections.Specialized;

/// <summary>
/// CustamizableCalendarで使用するデータ用インターフェース
/// </summary>
public interface ICalendarSource
{
    /// <summary>
    /// 対象の日付を表すプロパティ
    /// </summary>
    int Day { get; init; }
}

/// <summary>
/// CustamizableCalendarで自動生成されたセル用のデータ
/// </summary>
public class CalendarSource : ICalendarSource
{
    /// <summary>
    /// 対象の日付を表すプロパティ
    /// </summary>
    public int Day { get; init; } = DateTime.MinValue.Day;
}

/// <summary>
/// CustamizableCalendarで使用するデータ用のコレクション
/// </summary>
/// <remarks>
/// 本クラスはObservableCollectionを継承しているので
/// アイテム追加時に自動的にCustamizableCalendarを更新可能
/// </remarks>
public class CalendarSourceCollection : ObservableCollection<ICalendarSource>
{
    private int _year = DateTime.MinValue.Year;
    private int _month = DateTime.MinValue.Month;

    /// <summary>
    /// 対象の西暦での年を表すプロパティ
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// セッターにてDateTimeで使用可能な年の範囲を超えた値が入力された場合に投げられる例外
    /// </exception>
    public int Year
    {
        get => this._year;
        init
        {
            var year = value;
            var maxYear = DateTime.MaxValue.Year;
            var minYear = DateTime.MinValue.Year;

            if (year > maxYear || year < minYear)
                throw new ArgumentOutOfRangeException(nameof(value), $"Year property must be from {maxYear} to {minYear}.");

            this._year = year;
        }
    }

    /// <summary>
    /// 対象の月を表すプロパティ
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// セッターにてDateTimeで使用可能な月の範囲を超えた値が入力された場合に投げられる例外
    /// </exception>
    public int Month
    {
        get => this._month;
        init
        {
            var month = value;
            var maxMonth = DateTime.MaxValue.Month;
            var minMonth = DateTime.MinValue.Month;

            if (month > maxMonth || month < minMonth)
                throw new ArgumentOutOfRangeException(nameof(value), $"Month property must be from {maxMonth} to {minMonth}.");

            this._month = month;
        }
    }

    /// <summary>
    /// アイテムの更新時に異常な値が含まれていないかのバリデーションを行うための処理が記された
    /// ObservableCollection.OnCollectionChangedメソッドのオーバーライド
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// 指定された月の範囲外の日付のアイテムが存在する場合、
    /// または、重複する日付のアイテムが存在する場合投げられる例外
    /// </exception>
    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
        var action = e.Action;
        if (action == NotifyCollectionChangedAction.Add || action == NotifyCollectionChangedAction.Replace)
        {
            // 範囲を超えた日付を指定してないかのバリデーション
            foreach (var day in from calendarSource in this select calendarSource.Day)
            {
                if (!DateTime.TryParse($"{this.Year}/{this.Month}/{day}", out DateTime _))
                    throw new InvalidOperationException("Valid date range exceeded.");
            }

            // 重複する日付が追加されていないかのバリデーション
            var isDupplicated = (from calendarSource in this
                                 group calendarSource by calendarSource.Day into uniqueGroup
                                 where uniqueGroup.Count() > 1
                                 select uniqueGroup).Count() > 0;

            if (isDupplicated)
                throw new InvalidOperationException("Duplicate dates are registered.");
        }

        base.OnCollectionChanged(e);
    }
}