namespace EnkuToolkit.UiIndependent.Test.DataObjects;

using EnkuToolkit.UiIndependent.DataObjects;
using EnkuToolkit.UiIndependent.Collections;

[TestFixture]
public class BaseDayDataTest
{
    #region コンストラクタ
    [TestCase]
    public void コンストラクタ_引数で指定する日付が1日の場合_例外は投げられない()
    {
        // arrange
        var day = 1;

        // act
        TestDelegate act = () =>
        {
            new BaseDayData(day);
        };

        // assert
        Assert.DoesNotThrow(act);
    }

    [TestCase]
    public void コンストラクタ_引数で指定する日付が31日の場合_例外は投げられない()
    {
        // arrange
        var day = 31;

        // act
        TestDelegate act = () =>
        {
            new BaseDayData(day);
        };

        // assert
        Assert.DoesNotThrow(act);
    }

    [TestCase]
    public void コンストラクタ_引数で指定する日付が0日の場合_ArgumentOutOfRangeExceptionがなげられる()
    {
        // arrange
        var day = 0;
        var expectedExceptionMessage = "Date must be in the range of 1 to 31 (Parameter 'day')";

        // act
        TestDelegate act = () =>
        {
            new BaseDayData(day);
        };

        // assert
        var exceptionMessage = Assert.Throws<ArgumentOutOfRangeException>(act).Message;
        Assert.That(exceptionMessage, Is.EqualTo(expectedExceptionMessage));
    }

    [TestCase]
    public void コンストラクタ_引数で指定する日付が32日の場合_ArgumentOutOfRangeExceptionがなげられる()
    {
        // arrange
        var day = 32;
        var expectedExceptionMessage = "Date must be in the range of 1 to 31 (Parameter 'day')";

        // act
        TestDelegate act = () =>
        {
            new BaseDayData(day);
        };

        // assert
        var exceptionMessage = Assert.Throws<ArgumentOutOfRangeException>(act).Message;
        Assert.That(exceptionMessage, Is.EqualTo(expectedExceptionMessage));
    }
    #endregion

    #region IsHoliday
    [TestCase]
    public void IsHoliday_親となる2023年5月に設定したDayDataCollectionオブジェクトに29日として登録している状態_falseを返す()
    {
        // arrange
        var year = 2023;
        var month = 5;
        var day = 29;
        var parent = new DayDataCollection(year, month);
        var item = new BaseDayData(day);
        parent.Add(item);

        // act
        var isHoliday = item.IsHoliday;

        // assert
        Assert.False(isHoliday);
    }

    [TestCase]
    public void IsHoliday_親となる2023年5月に設定したDayDataCollectionオブジェクトに27日として登録している状態_trueを返す()
    {
        // arrange
        var year = 2023;
        var month = 5;
        var day = 27;
        var parent = new DayDataCollection(year, month);
        var item = new BaseDayData(day);
        parent.Add(item);

        // act
        var isHoliday = item.IsHoliday;

        // assert
        Assert.True(isHoliday);
    }

    [TestCase]
    public void IsHoliday_親となる2023年5月に設定したDayDataCollectionオブジェクトに28日として登録している状態_trueを返す()
    {
        // arrange
        var year = 2023;
        var month = 5;
        var day = 28;
        var parent = new DayDataCollection(year, month);
        var item = new BaseDayData(day);
        parent.Add(item);

        // act
        var isHoliday = item.IsHoliday;

        // assert
        Assert.True(isHoliday);
    }

    [TestCase]
    public void IsHoliday_親となるDayDataCollectionオブジェクトに登録していない状態で値を読み取ろうとする_NullReferenceExceptionをなげる()
    {
        // arrange
        var day = 1;
        var baseDayData = new BaseDayData(day);
        var expectedExceptionMessage = "This object has no parent DaySourceCollection object. This property must be added to the DaySourceCollection object before it can be used.";

        // act
        TestDelegate act = () =>
        {
            var isHoliday = baseDayData.IsHoliday;
        };

        // assert
        var exceptionMessage = Assert.Throws<NullReferenceException>(act).Message;
        Assert.That(exceptionMessage, Is.EqualTo(expectedExceptionMessage));
    }

    [TestCase]
    public void IsHoliday_親となるDayDataCollectionオブジェクトの参照が切れた状態で値を読み取ろうとする_NullReferenceExceptionをなげる()
    {
        // arrange
        var year = 2023;
        var month = 5;
        var day = 1;
        var excepted = new DateTime(year, month, day);
        var parent = new DayDataCollection(year, month);
        var item = new BaseDayData(day);
        var expectedExceptionMessage = "This object has no parent DaySourceCollection object. This property must be added to the DaySourceCollection object before it can be used.";

        parent = null;
        // act
        TestDelegate act = () =>
        {
            var isHoliday = item.IsHoliday;
        };

        // assert
        var exceptionMessage = Assert.Throws<NullReferenceException>(act).Message;
        Assert.That(exceptionMessage, Is.EqualTo(expectedExceptionMessage));
    }
    #endregion

    #region DateTime
    [TestCase]
    public void DateTime_親となる2023年5月に設定したDayDataCollectionオブジェクトに28日として登録している状態_2023年5月28日のDateTimeオブジェクトを返す()
    {
        // arrange
        var year = 2023;
        var month = 5;
        var day = 28;
        var excepted = new DateTime(year, month, day);
        var parent = new DayDataCollection(year, month);
        var item = new BaseDayData(day);
        parent.Add(item);

        // act
        var dateTime = item.DateTime;

        // assert
        Assert.That(dateTime, Is.EqualTo(excepted));
    }

    [TestCase]
    public void DateTime_親となるDayDataCollectionオブジェクトに登録していない状態で値を読み取ろうとする_NullReferenceExceptionをなげる()
    {
        // arrange
        var day = 1;
        var baseDayData = new BaseDayData(day);
        var expectedExceptionMessage = "This object has no parent DaySourceCollection object. This property must be added to the DaySourceCollection object before it can be used.";

        // act
        TestDelegate act = () =>
        {
            var dateTime = baseDayData.DateTime;
        };

        // assert
        var exceptionMessage = Assert.Throws<NullReferenceException>(act).Message;
        Assert.That(exceptionMessage, Is.EqualTo(expectedExceptionMessage));
    }

    [TestCase]
    public void DateTime_親となるDayDataCollectionオブジェクトの参照が切れた状態で値を読み取ろうとする_NullReferenceExceptionをなげる()
    {
        // arrange
        var year = 2023;
        var month = 5;
        var day = 1;
        var excepted = new DateTime(year, month, day);
        var parent = new DayDataCollection(year, month);
        var item = new BaseDayData(day);
        var expectedExceptionMessage = "This object has no parent DaySourceCollection object. This property must be added to the DaySourceCollection object before it can be used.";

        parent = null;
        // act
        TestDelegate act = () =>
        {
            var dateTime = item.DateTime;
        };

        // assert
        var exceptionMessage = Assert.Throws<NullReferenceException>(act).Message;
        Assert.That(exceptionMessage, Is.EqualTo(expectedExceptionMessage));
    }
    #endregion

    #region Year
    [TestCase]
    public void Year_親となる2023年5月に設定したDayDataCollectionオブジェクトに28日として登録している状態_2023を返す()
    {
        // arrange
        var year = 2023;
        var month = 5;
        var day = 28;
        var parent = new DayDataCollection(year, month);
        var item = new BaseDayData(day);
        parent.Add(item);

        // act
        var result = item.Year;

        // assert
        Assert.That(result, Is.EqualTo(year));
    }

    [TestCase]
    public void Year_親となるDayDataCollectionオブジェクトに登録していない状態で値を読み取ろうとする_NullReferenceExceptionをなげる()
    {
        // arrange
        var day = 1;
        var baseDayData = new BaseDayData(day);
        var expectedExceptionMessage = "This object has no parent DaySourceCollection object. This property must be added to the DaySourceCollection object before it can be used.";

        // act
        TestDelegate act = () =>
        {
            var result = baseDayData.Year;
        };

        // assert
        var exceptionMessage = Assert.Throws<NullReferenceException>(act).Message;
        Assert.That(exceptionMessage, Is.EqualTo(expectedExceptionMessage));
    }

    [TestCase]
    public void Year_親となるDayDataCollectionオブジェクトの参照が切れた状態で値を読み取ろうとする_NullReferenceExceptionをなげる()
    {
        // arrange
        var year = 2023;
        var month = 5;
        var day = 1;
        var excepted = new DateTime(year, month, day);
        var parent = new DayDataCollection(year, month);
        var item = new BaseDayData(day);
        var expectedExceptionMessage = "This object has no parent DaySourceCollection object. This property must be added to the DaySourceCollection object before it can be used.";

        parent = null;
        // act
        TestDelegate act = () =>
        {
            var result = item.Year;
        };

        // assert
        var exceptionMessage = Assert.Throws<NullReferenceException>(act).Message;
        Assert.That(exceptionMessage, Is.EqualTo(expectedExceptionMessage));
    }
    #endregion

    #region Month
    [TestCase]
    public void Month_親となる2023年5月に設定したDayDataCollectionオブジェクトに28日として登録している状態_5を返す()
    {
        // arrange
        var year = 2023;
        var month = 5;
        var day = 28;
        var parent = new DayDataCollection(year, month);
        var item = new BaseDayData(day);
        parent.Add(item);

        // act
        var result = item.Month;

        // assert
        Assert.That(result, Is.EqualTo(month));
    }

    [TestCase]
    public void Month_親となるDayDataCollectionオブジェクトに登録していない状態で値を読み取ろうとする_NullReferenceExceptionをなげる()
    {
        // arrange
        var day = 1;
        var baseDayData = new BaseDayData(day);
        var expectedExceptionMessage = "This object has no parent DaySourceCollection object. This property must be added to the DaySourceCollection object before it can be used.";

        // act
        TestDelegate act = () =>
        {
            var result = baseDayData.Month;
        };

        // assert
        var exceptionMessage = Assert.Throws<NullReferenceException>(act).Message;
        Assert.That(exceptionMessage, Is.EqualTo(expectedExceptionMessage));
    }

    [TestCase]
    public void Month_親となるDayDataCollectionオブジェクトの参照が切れた状態で値を読み取ろうとする_NullReferenceExceptionをなげる()
    {
        // arrange
        var year = 2023;
        var month = 5;
        var day = 1;
        var excepted = new DateTime(year, month, day);
        var parent = new DayDataCollection(year, month);
        var item = new BaseDayData(day);
        var expectedExceptionMessage = "This object has no parent DaySourceCollection object. This property must be added to the DaySourceCollection object before it can be used.";

        parent = null;
        // act
        TestDelegate act = () =>
        {
            var result = item.Month;
        };

        // assert
        var exceptionMessage = Assert.Throws<NullReferenceException>(act).Message;
        Assert.That(exceptionMessage, Is.EqualTo(expectedExceptionMessage));
    }
    #endregion
}