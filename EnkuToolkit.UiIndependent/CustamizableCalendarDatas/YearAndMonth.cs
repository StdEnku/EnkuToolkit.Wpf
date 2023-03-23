namespace EnkuToolkit.UiIndependent.CustamizableCalendarDatas;

/// <summary>
/// CustamizableCalendarに表示対象の年と月を指定するためのイミュータブルなデータ
/// </summary>
public class YearAndMonth
{
    /// <summary>
    /// 年を表すプロパティ
    /// </summary>
    public int Year { get; init; }

    /// <summary>
    /// 月を表すプロパティ
    /// </summary>
    public int Month { get; init; }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public YearAndMonth()
    {

    }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public YearAndMonth(int year, int month)
    {
        this.Year = year;
        this.Month = month;
    }

    /// <summary>
    /// String化用メソッド
    /// </summary>
    public override string ToString()
        => $"{this.Year}/{this.Month:00}";

    /// <summary>
    /// 月を一つ進めたオブジェクトを返すメソッド
    /// </summary>
    public YearAndMonth ForwardMonth()
    {
        var currentYear = this.Year;
        var currentMonth = this.Month;
        var isLastMonth = currentMonth == 12;

        return new YearAndMonth(
            isLastMonth ? currentYear + 1 : currentYear,
            isLastMonth ? 1 : currentMonth + 1
        );
    }

    /// <summary>
    /// 月を一つ戻したオブジェクトを返すメソッド
    /// </summary>
    /// <returns></returns>
    public YearAndMonth BackwardMonth()
    {
        var currentYear = this.Year;
        var currentMonth = this.Month;
        var isFirstMonth = currentMonth == 1;

        return new YearAndMonth(
            isFirstMonth ? currentYear - 1 : currentYear,
            isFirstMonth ? 12 : currentMonth - 1
        );
    }
}