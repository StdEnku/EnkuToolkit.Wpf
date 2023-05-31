namespace EnkuToolkit.UiIndependent.Test.Collections;

using EnkuToolkit.UiIndependent.Collections;
using EnkuToolkit.UiIndependent.DataObjects;
using System;
using System.Collections.Specialized;

[TestFixture]
public class DayDataCollectionTest
{
    #region コンストラクタ
    [TestCase]
    public void コンストラクタ_引数yearが9999の場合_例外は投げられない()
    {
        // arrange
        var year = 9999;
        var month = 1;

        // act
        TestDelegate act = () =>
        {
            new DayDataCollection(year, month);
        };

        // assert
        Assert.DoesNotThrow(act);
    }

    [TestCase]
    public void コンストラクタ_引数yearが10000の場合_ArgumentOutOfRangeExceptionを投げる()
    {
        // arrange
        var year = 10000;
        var month = 1;
        var expectedExceptionMessages = $"The possible values for the argument year must be between {DateTime.MinValue.Year} and {DateTime.MaxValue.Year}. (Parameter 'year')";

        // act
        TestDelegate act = () =>
        {
            new DayDataCollection(year, month);
        };

        // assert
        var exceptionMessage = Assert.Throws<ArgumentOutOfRangeException>(act).Message;
        Assert.That(exceptionMessage, Is.EqualTo(expectedExceptionMessages));
    }

    [TestCase]
    public void コンストラクタ_引数yearが1の場合_例外は投げられない()
    {
        // arrange
        var year = 1;
        var month = 1;

        // act
        TestDelegate act = () =>
        {
            new DayDataCollection(year, month);
        };

        // assert
        Assert.DoesNotThrow(act);
    }

    [TestCase]
    public void コンストラクタ_引数yearが0の場合_ArgumentOutOfRangeExceptionを投げる()
    {
        // arrange
        var year = 0;
        var month = 1;
        var expectedExceptionMessages = $"The possible values for the argument year must be between {DateTime.MinValue.Year} and {DateTime.MaxValue.Year}. (Parameter 'year')";

        // act
        TestDelegate act = () =>
        {
            new DayDataCollection(year, month);
        };

        // assert
        var exceptionMessage = Assert.Throws<ArgumentOutOfRangeException>(act).Message;
        Assert.That(exceptionMessage, Is.EqualTo(expectedExceptionMessages));
    }

    [TestCase]
    public void コンストラクタ_引数monthが12の場合_例外は投げられない()
    {
        // arrange
        var year = 1;
        var month = 12;

        // act
        TestDelegate act = () =>
        {
            new DayDataCollection(year, month);
        };

        // assert
        Assert.DoesNotThrow(act);
    }

    [TestCase]
    public void コンストラクタ_引数monthが13の場合_ArgumentOutOfRangeExceptionを投げる()
    {
        // arrange
        var year = 1;
        var month = 13;
        var expectedExceptionMessages = $"The argument month must be a value between {DateTime.MinValue.Month} and {DateTime.MaxValue.Month}. (Parameter 'month')";

        // act
        TestDelegate act = () =>
        {
            new DayDataCollection(year, month);
        };

        // assert
        var exceptionMessage = Assert.Throws<ArgumentOutOfRangeException>(act).Message;
        Assert.That(exceptionMessage, Is.EqualTo(expectedExceptionMessages));
    }

    [TestCase]
    public void コンストラクタ_引数monthが1の場合_例外は投げられない()
    {
        // arrange
        var year = 1;
        var month = 1;

        // act
        TestDelegate act = () =>
        {
            new DayDataCollection(year, month);
        };

        // assert
        Assert.DoesNotThrow(act);
    }

    [TestCase]
    public void コンストラクタ_引数monthが0の場合_ArgumentOutOfRangeExceptionを投げる()
    {
        // arrange
        var year = 1;
        var month = 0;
        var expectedExceptionMessages = $"The argument month must be a value between {DateTime.MinValue.Month} and {DateTime.MaxValue.Month}. (Parameter 'month')";

        // act
        TestDelegate act = () =>
        {
            new DayDataCollection(year, month);
        };

        // assert
        var exceptionMessage = Assert.Throws<ArgumentOutOfRangeException>(act).Message;
        Assert.That(exceptionMessage, Is.EqualTo(expectedExceptionMessages));
    }
    #endregion

    #region Add
    [TestCase]
    public void Add_2023年5月に指定したオブジェクトに31日のBaseDayDataオブジェクトを追加_要素に31日のBaseDayDataオブジェクトが存在する()
    {
        // arrange
        var year = 2023;
        var month = 5;
        var day = 31;
        var baseDayData = new BaseDayData(day);
        var dayDataCollection = new DayDataCollection(year, month);

        // act
        dayDataCollection.Add(baseDayData);
        var isExists = dayDataCollection
                        .Where(x => x.Day == day)
                        .FirstOrDefault() is not null;

        // assert
        Assert.True(isExists);
    }

    [TestCase]
    public void Add_2023年4月に指定したオブジェクトに存在しないはずの31日を追加_InvalidOperationExceptionが投げられる()
    {
        // arrange
        var year = 2023;
        var month = 4;
        var day = 31;
        var baseDayData = new BaseDayData(day);
        var dayDataCollection = new DayDataCollection(year, month);
        var expectedExceptionMessages = $"Added {year}/{month}/{day} is an invalid date";

        // act
        TestDelegate act = () =>
        {
            dayDataCollection.Add(baseDayData);
        };

        // assert
        var exceptionMessage = Assert.Throws<InvalidOperationException>(act).Message;
        Assert.That(exceptionMessage, Is.EqualTo(expectedExceptionMessages));
    }

    [TestCase]
    public void Add_すでに登録済みの日付のBaseDayDataオブジェクトと同じ日付の要素を追加する_InvalidOperationExceptionが投げられる()
    {
        // arrange
        var year = 2023;
        var month = 4;
        var day1 = 15;
        var day2 = 15;
        var dayData1 = new BaseDayData(day1);
        var dayData2 = new BaseDayData(day2);
        var dayDataCollection = new DayDataCollection(year, month);
        var expectedExceptionMessages = "Duplicate dates are about to be registered";

        dayDataCollection.Add(dayData1);

        // act
        TestDelegate act = () =>
        {
            dayDataCollection.Add(dayData2);
        };

        // assert
        var exceptionMessage = Assert.Throws<InvalidOperationException>(act).Message;
        Assert.That(exceptionMessage, Is.EqualTo(expectedExceptionMessages));
    }
    #endregion

    #region Replace
    [TestCase]
    public void Replace_2023年5月に指定したオブジェクトに31日のBaseDayDataオブジェクトを再代入する_要素に31日のBaseDayDataオブジェクトが存在して最初に再代入された日付のBaseDayDataオブジェクトが存在しない()
    {
        // arrange
        var year = 2023;
        var month = 5;
        var secondDay = 31;
        var firstDay = 15;
        var firstDayData = new BaseDayData(firstDay);
        var secondDayData = new BaseDayData(secondDay);
        var dayDataCollection = new DayDataCollection(year, month);

        // act
        dayDataCollection.Add(firstDayData);
        dayDataCollection[0] = secondDayData;

        var isFirstDayDataExists = dayDataCollection
                                        .Where(x => x.Day == firstDay)
                                        .FirstOrDefault() is not null;

        var isSecondDayDataExists = dayDataCollection
                                        .Where(x => x.Day == secondDay)
                                        .FirstOrDefault() is not null;

        // assert
        Assert.False(isFirstDayDataExists);
        Assert.True(isSecondDayDataExists);
    }

    [TestCase]
    public void Replace_2023年4月登録したBaseDayDataオブジェクトに存在しないはずの31日に置き換える_InvalidOperationExceptionが投げられる()
    {
        // arrange
        var year = 2023;
        var month = 4;
        var day1 = 15;
        var day2 = 31;
        var dayData1 = new BaseDayData(day1);
        var dayData2 = new BaseDayData(day2);
        var dayDataCollection = new DayDataCollection(year, month);
        var expectedExceptionMessages = $"Added {year}/{month}/{day2} is an invalid date";

        dayDataCollection.Add(dayData1);

        // act
        TestDelegate act = () =>
        {
            dayDataCollection[0] = dayData2;
        };

        // assert
        var exceptionMessage = Assert.Throws<InvalidOperationException>(act).Message;
        Assert.That(exceptionMessage, Is.EqualTo(expectedExceptionMessages));
    }

    [TestCase]
    public void Replace_すでに登録済みの日付のBaseDayDataオブジェクトと重複する日付の要素への置き換えを行う_InvalidOperationExceptionが投げられる()
    {
        // arrange
        var year = 2023;
        var month = 4;
        var day1 = 3;
        var day2 = 15;
        var day3 = 15;
        var dayData1 = new BaseDayData(day1);
        var dayData2 = new BaseDayData(day2);
        var dayData3 = new BaseDayData(day3);
        var dayDataCollection = new DayDataCollection(year, month);
        var expectedExceptionMessages = "Duplicate dates are about to be registered";

        dayDataCollection.Add(dayData1);
        dayDataCollection.Add(dayData2);

        // act
        TestDelegate act = () =>
        {
            dayDataCollection[0] = dayData3;
        };

        // assert
        var exceptionMessage = Assert.Throws<InvalidOperationException>(act).Message;
        Assert.That(exceptionMessage, Is.EqualTo(expectedExceptionMessages));
    }
    #endregion

    #region CollectionChangedイベントが確実に発火するかのテスト
    [TestCase]
    public void CollectionChanged_要素が追加された場合_CollectionChangedイベントが実行される()
    {
        // arrange
        var year = 2023;
        var month = 5;
        var day = 15;
        var baseDayData = new BaseDayData(day);
        var dayDataCollection = new DayDataCollection(year, month);
        var isColled = false;

        NotifyCollectionChangedEventHandler? handler = null;
        handler += (s, e) =>
        {
            isColled = true;
            dayDataCollection.CollectionChanged -= handler;
        };
        dayDataCollection.CollectionChanged += handler;

        // act
        dayDataCollection.Add(baseDayData);

        // assert
        Assert.True(isColled);
    }

    [TestCase]
    public void CollectionChanged_要素が置き換えられた場合_CollectionChangedイベントが実行される()
    {
        // arrange
        var year = 2023;
        var month = 5;
        var day1 = 15;
        var day2 = 3;
        var dayData1 = new BaseDayData(day1);
        var dayData2 = new BaseDayData(day2);
        var dayDataCollection = new DayDataCollection(year, month);
        var isColled = false;

        dayDataCollection.Add(dayData1);

        NotifyCollectionChangedEventHandler? handler = null;
        handler += (s, e) =>
        {
            isColled = true;
            dayDataCollection.CollectionChanged -= handler;
        };
        dayDataCollection.CollectionChanged += handler;

        // act
        dayDataCollection[0] = dayData2;

        // assert
        Assert.True(isColled);
    }

    [TestCase]
    public void CollectionChanged_要素が削除された場合_CollectionChangedイベントが実行される()
    {
        // arrange
        var year = 2023;
        var month = 5;
        var day = 15;
        var baseDayData = new BaseDayData(day);
        var dayDataCollection = new DayDataCollection(year, month);
        var isColled = false;

        dayDataCollection.Add(baseDayData);

        NotifyCollectionChangedEventHandler? handler = null;
        handler += (s, e) =>
        {
            isColled = true;
            dayDataCollection.CollectionChanged -= handler;
        };
        dayDataCollection.CollectionChanged += handler;

        // act
        dayDataCollection.Remove(baseDayData);

        // assert
        Assert.True(isColled);
    }

    [TestCase]
    public void CollectionChanged_要素がクリアされた場合_CollectionChangedイベントが実行される()
    {
        // arrange
        var year = 2023;
        var month = 5;
        var day = 15;
        var baseDayData = new BaseDayData(day);
        var dayDataCollection = new DayDataCollection(year, month);
        var isColled = false;

        dayDataCollection.Add(baseDayData);

        NotifyCollectionChangedEventHandler? handler = null;
        handler += (s, e) =>
        {
            isColled = true;
            dayDataCollection.CollectionChanged -= handler;
        };
        dayDataCollection.CollectionChanged += handler;

        // act
        dayDataCollection.Clear();

        // assert
        Assert.True(isColled);
    }

    [TestCase]
    public void CollectionChanged_要素が移動された場合_CollectionChangedイベントが実行される()
    {
        // arrange
        var year = 2023;
        var month = 5;
        var day1 = 15;
        var day2 = 3;
        var dayData1 = new BaseDayData(day1);
        var dayData2 = new BaseDayData(day2);
        var dayDataCollection = new DayDataCollection(year, month);
        var isColled = false;

        dayDataCollection.Add(dayData1);
        dayDataCollection.Add(dayData2);

        NotifyCollectionChangedEventHandler? handler = null;
        handler += (s, e) =>
        {
            isColled = true;
            dayDataCollection.CollectionChanged -= handler;
        };
        dayDataCollection.CollectionChanged += handler;

        // act
        dayDataCollection.Move(1, 0);

        // assert
        Assert.True(isColled);
    }
    #endregion

    #region CreateNextMonth
    [TestCase]
    public void CreateNextMonth_9998年12月が指定された状態で本メソッドを呼び出す_9999年1月のDayDataCollectionオブジェクトが生成される()
    {
        // arrange
        var year = 9999;
        var month = 11;
        var expectYear = 9999;
        var expectMonth = 12;
        var dayDataCollection = new DayDataCollection(year, month);

        // act
        var nextMonthDayDataCollection = dayDataCollection.CreateNextMonth();

        // assert
        Assert.That(nextMonthDayDataCollection.Year, Is.EqualTo(expectYear));
        Assert.That(nextMonthDayDataCollection.Month, Is.EqualTo(expectMonth));
    }

    [TestCase]
    public void CreateNextMonth_9999年12月が指定された状態で本メソッドを呼び出す_InvalidOperationExceptionが投げられる()
    {
        // arrange
        var year = 9999;
        var month = 12;
        var dayDataCollection = new DayDataCollection(year, month);
        var expectedExceptionMessage = $"Dates after {DateTime.MaxValue.Date.ToString()} are not supported in DayDatacollection.";

        // act
        TestDelegate act = () =>
        {
            dayDataCollection.CreateNextMonth();
        };

        // assert
        var exceptionMessage = Assert.Throws<InvalidOperationException>(act).Message;
        Assert.That(exceptionMessage, Is.EqualTo(expectedExceptionMessage));
    }
    #endregion

    #region CreateLastMonth
    [TestCase]
    public void CreateLastMonth_1年2月が指定された状態で本メソッドを呼び出す_1年1月のDayDataCollectionオブジェクトが生成される()
    {
        // arrange
        var year = 1;
        var month = 2;
        var expectYear = 1;
        var expectMonth = 1;
        var dayDataCollection = new DayDataCollection(year, month);

        // act
        var nextMonthDayDataCollection = dayDataCollection.CreateLastMonth();

        // assert
        Assert.That(nextMonthDayDataCollection.Year, Is.EqualTo(expectYear));
        Assert.That(nextMonthDayDataCollection.Month, Is.EqualTo(expectMonth));
    }

    [TestCase]
    public void CreateNextMonth_1年1月が指定された状態で本メソッドを呼び出す_InvalidOperationExceptionが投げられる()
    {
        // arrange
        var year = 1;
        var month = 1;
        var dayDataCollection = new DayDataCollection(year, month);
        var expectedExceptionMessage = $"Dates prior {DateTime.MinValue.Date.ToString()} are not supported in DayDatacollection.";

        // act
        TestDelegate act = () =>
        {
            dayDataCollection.CreateLastMonth();
        };

        // assert
        var exceptionMessage = Assert.Throws<InvalidOperationException>(act).Message;
        Assert.That(exceptionMessage, Is.EqualTo(expectedExceptionMessage));
    }
    #endregion
}