namespace Sandbox.ViewModels;

using EnkuToolkit.Wpf.Behaviors;
using Sandbox.MvvmUtil;
using System.Collections.Generic;
using System.Collections;
using System.Windows;

public class Page9ViewModel : ViewModelsBase
{
    private IList _selecteds = new List<string>();
    public IList Selecteds
    {
        get => _selecteds;
        set
        {
            _selecteds = value;
            NotifyPropertyChanged();
        }
    }

    public DelegateCommand ClickedCommand { get; }
    private void Clicked(object? param)
    {
        var text = string.Empty;
        foreach (var i in Selecteds)
            text += i.ToString() + '\n';
        MessageBox.Show(text);
    }

    public Page9ViewModel()
    {
        ClickedCommand = new(Clicked);
    }

    public void OnLoaded()
    {
        var list = new List<string>();
        list.Add("みかん");
        list.Add("パイナップル");
        Selecteds = list;
    }
}