namespace Sandbox.ViewModels;

using DataObjects;
using EnkuToolkit.UiIndependent.Collections;
using MvvmUtil;
using System;
using System.Collections.Generic;
using System.Windows;

public class MainWindowViewModel : ViewModelsBase
{
    #region Sourceプロパティ
    private DayDataCollection _source;

    public DayDataCollection Source
    {
        get => _source;
        set
        {
            _source = value;
            NotifyPropertyChanged();
        }
    }
    #endregion

    #region 選択されている日付プロパティ
    private IEnumerable<DateTime> _selectedDates = new List<DateTime>();

    public IEnumerable<DateTime> SelectedDates
    {
        get => _selectedDates;
        set
        {
            _selectedDates = value;
            NotifyPropertyChanged();
        }
    }
    #endregion

    #region ソース変更コマンド
    public DelegateCommand AddSourceCommand { get; }
    private void AddSource(object? param)
    {
        Source[0] = new DayData(1, "カップ麺", "カップ麺", "カップ麺");
        Source.Add(new DayData(4, "カップ麺", "カップ麺", "カップ麺"));
    }
    #endregion

    #region Clicked Commands
    public DelegateCommand LeftArrowClickedCommand { get; }
    private void LeftArrowClicked(object? arg)
        => Source = Source.CreateLastMonth();

    public DelegateCommand RightArrowClickedCommand { get; }
    private void RightArrowClicked(object? arg)
        => Source = Source.CreateNextMonth();
    #endregion

    public DelegateCommand ShowSelectedDatesCommand { get; }
    private void ShowSelectedDates(object? param)
    {
        var text = string.Empty;
        foreach (var i in SelectedDates)
        {
            text += i.ToString() + '\n';
        }
        MessageBox.Show(text);
    }

    public DelegateCommand SetSelectedDatesCommand { get; }
    private void SetSelectedDates(object? param)
    {
        var year = Source.Year;
        var month = Source.Month;

        var result = new List<DateTime>();
        result.Add(new DateTime(year, month, 1));
        result.Add(new DateTime(year, month, 2));
        result.Add(new DateTime(year, month, 3));
        result.Add(new DateTime(year, month, 4));
        result.Add(new DateTime(year, month, 5));
        result.Add(new DateTime(year, month, 6));
        result.Add(new DateTime(year, month, 7));

        SelectedDates = result;
    }

    public DelegateCommand DoubleClickedCommand { get; }
    private void DoubleClicked(object? param)
    {
        var dt = param as DateTime?;
        if (dt is not null)
            MessageBox.Show(dt.Value.ToString());
    }

    public MainWindowViewModel()
    {
        LeftArrowClickedCommand = new(LeftArrowClicked);
        RightArrowClickedCommand = new(RightArrowClicked);
        AddSourceCommand = new(AddSource);
        ShowSelectedDatesCommand = new(ShowSelectedDates);
        SetSelectedDatesCommand = new(SetSelectedDates);
        DoubleClickedCommand = new(DoubleClicked);

        var year = DateTime.Now.Year;
        var month = DateTime.Now.Month;
        var source = new DayDataCollection(year, month);
        source.Add(new DayData(1 , "鮭", "カレー", "ハンバーグ"));
        source.Add(new DayData(2 , "鮭", "カレー", "ハンバーグ"));
        source.Add(new DayData(3 , "鮭", "カレー", "ハンバーグ"));
        _source = source;
    }
}