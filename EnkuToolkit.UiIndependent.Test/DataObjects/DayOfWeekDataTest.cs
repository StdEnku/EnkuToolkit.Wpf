namespace EnkuToolkit.UiIndependent.Test.DataObjects;

using EnkuToolkit.UiIndependent.DataObjects;

[TestFixture]
public class DayOfWeekDataTest
{
    [TestCase]
    public void DayOfWeek_Initセッターになにも指定せずに初期化_日曜日を返す()
    {
        // arrange
        var dayOfWeekData = new DayOfWeekData();

        // act
        var result = dayOfWeekData.DayOfWeek;

        // assert
        Assert.That(result, Is.EqualTo(DayOfWeek.Sunday));
    }

    [TestCase]
    public void DayOfWeek_Initセッター金曜日指定して初期化_金曜日を返す()
    {
        // arrange
        var dayOfWeek = DayOfWeek.Friday;
        var dayOfWeekData = new DayOfWeekData() { DayOfWeek = dayOfWeek };

        // act
        var result = dayOfWeekData.DayOfWeek;

        // assert
        Assert.That(result, Is.EqualTo(dayOfWeek));
    }

    [TestCase]
    public void Text_Initセッターになにも指定せずに初期化_空の文字列を返す()
    {
        // arrange
        var expected = string.Empty;
        var dayOfWeekData = new DayOfWeekData();

        // act
        var result = dayOfWeekData.Text;

        // assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase]
    public void Text_InitセッターDummyText指定して初期化_DummyTextを返す()
    {
        // arrange
        var dummyText = "DummyText";
        var dayOfWeekData = new DayOfWeekData() { Text = dummyText };

        // act
        var result = dayOfWeekData.Text;

        // assert
        Assert.That(result, Is.EqualTo(dummyText));
    }

    [TestCase]
    public void IsHoliday_DayOfWeekプロパティのInitセッターに月曜日を指定して初期化_falseを返す()
    {
        // arrange
        var dayOfWeek = DayOfWeek.Monday;
        var dayOfWeekData = new DayOfWeekData() { DayOfWeek = dayOfWeek };

        // act
        var result = dayOfWeekData.IsHoliday;

        // assert
        Assert.False(result);
    }

    [TestCase]
    public void IsHoliday_DayOfWeekプロパティのInitセッターに土曜日を指定して初期化_trueを返す()
    {
        // arrange
        var dayOfWeek = DayOfWeek.Saturday;
        var dayOfWeekData = new DayOfWeekData() { DayOfWeek = dayOfWeek };

        // act
        var result = dayOfWeekData.IsHoliday;

        // assert
        Assert.True(result);
    }

    [TestCase]
    public void IsHoliday_DayOfWeekプロパティのInitセッターに日曜日を指定して初期化_trueを返す()
    {
        // arrange
        var dayOfWeek = DayOfWeek.Sunday;
        var dayOfWeekData = new DayOfWeekData() { DayOfWeek = dayOfWeek };

        // act
        var result = dayOfWeekData.IsHoliday;

        // assert
        Assert.True(result);
    }
}