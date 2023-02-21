# MessageBoxService ViewService

WPFのアセンブリに依存してはいけないViewModel内から

MessageBoxを操作するためのViewService。

ViewとViewModelを別プロジェクトで管理することを想定して

interfaceとその実装は別アセンブリに格納されています。



## インターフェース側

インターフェース名 : IMessageBoxService

アセンブリ : EnkuToolkit.UiIndependent

名前空間 : EnkuToolkit.UiIndependent.Services

インターフェースで定義されているメソッド :

```c#
// 選択肢のないOKのみのメッセージボックス表示用メソッド
void ShowOk(string message, string? title = null);

// 選択肢がYesまたはNoのメッセージボックス表示用メソッド
bool ShowYesNo(string message, string? title = null);
```



## 実装側

クラス名 : MessageBoxService

アセンブリ : EnkuToolkit.Wpf

名前空間 : EnkuToolkit.Wpf.Services



## 使用例

DIコンテナを使用してViewModelから操作することを想定しています。



Page1ViewModel.cs

```c#
using EnkuToolkit.UiIndependent.Services;

public class Page1ViewModel : INotifyPropertyChanged
{
    private readonly IMessageBoxService _messageBoxService;
    
    // コンストラクタ注入を使用する。
    public Page1ViewModel(IMessageBoxService messageBoxService)
    {
        this._messageBoxService = messageBoxService;
    }
    
    // デリゲートコマンドで呼び出されるメソッド
    private void ClickedCommand()
    {
        // 選択肢のないOKボタンのみのメッセージボックス表示
        this._messageBoxService.ShowOk("Its operation is impossible", "hello Worning");
    }
    
    // ~省略~
}
```
