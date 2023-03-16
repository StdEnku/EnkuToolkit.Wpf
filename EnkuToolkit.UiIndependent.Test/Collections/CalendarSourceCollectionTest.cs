namespace EnkuToolkit.UiIndependent.Test.Collections;

using EnkuToolkit.UiIndependent.Collections;

public class CalendarSourceCollectionTest
{ 
    #region Yearプロパティ
    [Fact]
    public void Year_InitセッターにてDateTimeで取り扱える最低値を入力する_例外は投げられない()
    {
        // Data
        var minYear = DateTime.MinValue.Year;

        // Act
        var exceptions = Record.Exception(() => 
        {
            new CalendarSourceCollection() { Year=minYear };
        });

        // Assert
        Assert.Null(exceptions);
    }

    [Fact]
    public void Year_InitセッターにてDateTimeで取り扱える最低値以下の値を入力する_正しいメッセージのArgumentOutOfRangeExceptionを投げる()
    {
        // Data
        var minYear = DateTime.MinValue.Year - 1;
        var expectationOfExceptionMessage = $"Year property must be from {DateTime.MaxValue.Year} to {DateTime.MinValue.Year}. (Parameter 'value')";

        // Act
        var exception = Record.Exception(() =>
        {
            new CalendarSourceCollection() { Year = minYear };
        });

        // Assert
        Assert.Equal(typeof(ArgumentOutOfRangeException), exception.GetType());
        Assert.Equal(expectationOfExceptionMessage, exception.Message);
    }

    [Fact]
    public void Year_InitセッターにてDateTimeで取り扱える最大値を入力する_例外は投げられない()
    {
        // Data
        var maxYear = DateTime.MaxValue.Year;

        // Act
        var exceptions = Record.Exception(() =>
        {
            new CalendarSourceCollection() { Year = maxYear };
        });

        // Assert
        Assert.Null(exceptions);
    }

    [Fact]
    public void Year_InitセッターにてDateTimeで取り扱える最低値以上の値を入力する_正しいメッセージのArgumentOutOfRangeExceptionを投げる()
    {
        // Data
        var maxYear = DateTime.MaxValue.Year + 1;
        var expectationOfExceptionMessage = $"Year property must be from {DateTime.MaxValue.Year} to {DateTime.MinValue.Year}. (Parameter 'value')";

        // Act
        var exception = Record.Exception(() =>
        {
            new CalendarSourceCollection() { Year = maxYear };
        });

        // Assert
        Assert.Equal(typeof(ArgumentOutOfRangeException), exception.GetType());
        Assert.Equal(expectationOfExceptionMessage, exception.Message);
    }
    #endregion

    #region Monthプロパティ
    [Fact]
    public void Month_InitセッターにてDateTimeで取り扱える最低値を入力する_例外は投げられない()
    {
        // Data
        var minMonth = DateTime.MinValue.Month;

        // Act
        var exceptions = Record.Exception(() =>
        {
            new CalendarSourceCollection() { Month = minMonth };
        });

        // Assert
        Assert.Null(exceptions);
    }

    [Fact]
    public void Month_InitセッターにてDateTimeで取り扱える最低値以下の値を入力する_正しいメッセージのArgumentOutOfRangeExceptionを投げる()
    {
        // Data
        var minMonth = DateTime.MinValue.Month - 1;
        var expectationOfExceptionMessage = $"Month property must be from {DateTime.MaxValue.Month} to {DateTime.MinValue.Month}. (Parameter 'value')";

        // Act
        var exception = Record.Exception(() =>
        {
            new CalendarSourceCollection() { Month = minMonth };
        });

        // Assert
        Assert.Equal(typeof(ArgumentOutOfRangeException), exception.GetType());
        Assert.Equal(expectationOfExceptionMessage, exception.Message);
    }

    [Fact]
    public void Month_InitセッターにてDateTimeで取り扱える最大値を入力する_例外は投げられない()
    {
        // Data
        var maxMonth = DateTime.MaxValue.Month;

        // Act
        var exceptions = Record.Exception(() =>
        {
            new CalendarSourceCollection() { Month = maxMonth };
        });

        // Assert
        Assert.Null(exceptions);
    }

    [Fact]
    public void Month_InitセッターにてDateTimeで取り扱える最低値以上の値を入力する_正しいメッセージのArgumentOutOfRangeExceptionを投げる()
    {
        // Data
        var maxMonth = DateTime.MaxValue.Month + 1;
        var expectationOfExceptionMessage = $"Month property must be from {DateTime.MaxValue.Month} to {DateTime.MinValue.Month}. (Parameter 'value')";

        // Act
        var exception = Record.Exception(() =>
        {
            new CalendarSourceCollection() { Month = maxMonth };
        });

        // Assert
        Assert.Equal(typeof(ArgumentOutOfRangeException), exception.GetType());
        Assert.Equal(expectationOfExceptionMessage, exception.Message);
    }
    #endregion

    #region OnCollectionChangedメソッド
    [Fact]
    public void OnCollectionChanged_DateTimeで取り扱える最低値の日付が指定されているCalendarSourceオブジェクトを登録する_例外を投げない()
    {
        // Data
        var year = 2023;
        var month = 3;
        var day = 1;
        var calendarSource = new CalendarSource() { Day = day };
        var calendarSourceCollection = new CalendarSourceCollection() { Year=year, Month=month };

        // Act
        var exceptions = Record.Exception(() =>
        {
            calendarSourceCollection.Add(calendarSource);
        });

        // Assert
        Assert.Null(exceptions);
    }

    [Fact]
    public void OnCollectionChanged_DateTimeで取り扱える最低値以下の日付が指定されているCalendarSourceオブジェクトを登録する_InvalidOperationExceptionを投げる()
    {
        // Data
        var year = 2023;
        var month = 3;
        var day = 0;
        var calendarSource = new CalendarSource() { Day = day };
        var calendarSourceCollection = new CalendarSourceCollection() { Year = year, Month = month };
        var expectationOfExceptionMessage = $"Valid date range exceeded.";

        // Act
        var exception = Record.Exception(() =>
        {
            calendarSourceCollection.Add(calendarSource);
        });

        // Assert
        Assert.Equal(typeof(InvalidOperationException), exception.GetType());
        Assert.Equal(expectationOfExceptionMessage, exception.Message);
    }

    [Fact]
    public void OnCollectionChanged_DateTimeで取り扱える最大値の日付が指定されているCalendarSourceオブジェクトを登録する_例外を投げない()
    {
        // Data
        var year = 2023;
        var month = 3;
        var day = 31;
        var calendarSource = new CalendarSource() { Day = day };
        var calendarSourceCollection = new CalendarSourceCollection() { Year = year, Month = month };

        // Act
        var exceptions = Record.Exception(() =>
        {
            calendarSourceCollection.Add(calendarSource);
        });

        // Assert
        Assert.Null(exceptions);
    }

    [Fact]
    public void OnCollectionChanged_DateTimeで取り扱える最大値以上の日付が指定されているCalendarSourceオブジェクトを登録する_InvalidOperationExceptionを投げる()
    {
        // Data
        var year = 2023;
        var month = 3;
        var day = 32;
        var calendarSource = new CalendarSource() { Day = day };
        var calendarSourceCollection = new CalendarSourceCollection() { Year = year, Month = month };
        var expectationOfExceptionMessage = $"Valid date range exceeded.";

        // Act
        var exception = Record.Exception(() =>
        {
            calendarSourceCollection.Add(calendarSource);
        });

        // Assert
        Assert.Equal(typeof(InvalidOperationException), exception.GetType());
        Assert.Equal(expectationOfExceptionMessage, exception.Message);
    }

    [Fact]
    public void OnCollectionChanged_登録済みの日付が指定されたCalendarSourceオブジェクトを再度登録_InvalidOperationExceptionを投げる()
    {
        // Data
        var year = 2023;
        var month = 3;
        var day1 = 1;
        var day2 = 1;
        var calendarSource1 = new CalendarSource() { Day = day1 };
        var calendarSource2 = new CalendarSource() { Day = day2 };
        var calendarSourceCollection = new CalendarSourceCollection() { Year = year, Month = month };
        var expectationOfExceptionMessage = $"Duplicate dates are registered.";

        // Act
        calendarSourceCollection.Add(calendarSource1);

        var exception = Record.Exception(() =>
        {
            calendarSourceCollection.Add(calendarSource2);
        });

        // Assert
        Assert.Equal(typeof(InvalidOperationException), exception.GetType());
        Assert.Equal(expectationOfExceptionMessage, exception.Message);
    }

    [Fact]
    public void OnCollectionChanged_登録されているアイテムをDateTimeで取り扱える最低値の日付が指定されているCalendarSourceオブジェクトに置き換える_例外を投げない()
    {
        // Data
        var year = 2023;
        var month = 3;
        var day1 = 5;
        var day2 = 1;
        var calendarSource1 = new CalendarSource() { Day = day1 };
        var calendarSource2 = new CalendarSource() { Day = day2 };
        var calendarSourceCollection = new CalendarSourceCollection() { Year = year, Month = month };

        // Act
        calendarSourceCollection.Add(calendarSource1);

        var exceptions = Record.Exception(() =>
        {
            calendarSourceCollection[0] = calendarSource2;
        });

        // Assert
        Assert.Null(exceptions);
    }

    [Fact]
    public void OnCollectionChanged_登録されているアイテムを最低値以下の日付が指定されているCalendarSourceオブジェクトに置き換える_InvalidOperationExceptionを投げる()
    {
        // Data
        var year = 2023;
        var month = 3;
        var day1 = 5;
        var day2 = 0;
        var calendarSource1 = new CalendarSource() { Day = day1 };
        var calendarSource2 = new CalendarSource() { Day = day2 };
        var calendarSourceCollection = new CalendarSourceCollection() { Year = year, Month = month };
        var expectationOfExceptionMessage = $"Valid date range exceeded.";

        // Act
        calendarSourceCollection.Add(calendarSource1);

        var exception = Record.Exception(() =>
        {
            calendarSourceCollection[0] = calendarSource2;
        });

        // Assert
        Assert.Equal(typeof(InvalidOperationException), exception.GetType());
        Assert.Equal(expectationOfExceptionMessage, exception.Message);
    }

    [Fact]
    public void OnCollectionChanged_登録されているアイテムをDateTimeで取り扱える最大値の日付が指定されているCalendarSourceオブジェクトに置き換える_例外を投げない()
    {
        // Data
        var year = 2023;
        var month = 3;
        var day1 = 5;
        var day2 = 31;
        var calendarSource1 = new CalendarSource() { Day = day1 };
        var calendarSource2 = new CalendarSource() { Day = day2 };
        var calendarSourceCollection = new CalendarSourceCollection() { Year = year, Month = month };

        // Act
        calendarSourceCollection.Add(calendarSource1);

        var exceptions = Record.Exception(() =>
        {
            calendarSourceCollection[0] = calendarSource2;
        });

        // Assert
        Assert.Null(exceptions);
    }

    [Fact]
    public void OnCollectionChanged_登録されているアイテムを最大値以上の日付が指定されているCalendarSourceオブジェクトに置き換える_InvalidOperationExceptionを投げる()
    {
        // Data
        var year = 2023;
        var month = 3;
        var day1 = 5;
        var day2 = 32;
        var calendarSource1 = new CalendarSource() { Day = day1 };
        var calendarSource2 = new CalendarSource() { Day = day2 };
        var calendarSourceCollection = new CalendarSourceCollection() { Year = year, Month = month };
        var expectationOfExceptionMessage = $"Valid date range exceeded.";

        // Act
        calendarSourceCollection.Add(calendarSource1);

        var exception = Record.Exception(() =>
        {
            calendarSourceCollection[0] = calendarSource2;
        });

        // Assert
        Assert.Equal(typeof(InvalidOperationException), exception.GetType());
        Assert.Equal(expectationOfExceptionMessage, exception.Message);
    }
    #endregion
}