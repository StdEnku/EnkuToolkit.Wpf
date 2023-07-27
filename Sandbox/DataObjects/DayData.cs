namespace Sandbox.DataObjects;

using EnkuToolkit.UiIndependent.DataObjects;

public class DayData : BaseDayData
{
    public string BreakfastName { get; }

    public string LunchName { get; }

    public string DinnerName { get; }

    public DayData(int day, string breakfastName, string lunchName, string dinnerName) : base(day)
    {
        BreakfastName = breakfastName;
        LunchName = lunchName;
        DinnerName = dinnerName;
    }
}