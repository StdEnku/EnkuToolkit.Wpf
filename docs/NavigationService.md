# NavigationService ViewService

Application.Current.MainWindowがNavigationWindowの場合、

使用可能なViewModelから画面遷移を実行するためのViewService。

ViewとViewModelを別プロジェクトで管理することを想定して

interfaceとその実装は別アセンブリに格納されています。



## インターフェース側

インターフェース名 : INavigationService

アセンブリ : EnkuToolkit.UiIndependent

名前空間 : EnkuToolkit.UiIndependent.Services

インターフェースで定義されているメソッド : 

```c#
// プロジェクトのルートをベースとした相対URIで遷移先を指定可能な画面遷移用メソッド
bool NavigateRootBase(string uriStr, object? extraData = null);

// URIで遷移先を指定可能な画面遷移用メソッド
bool Navigate(Uri uri, object? extraData = null);

// ページを進めるためのメソッド
void GoForward();

// ページを戻すためのメソッド
void GoBack();

// ページの再読み込みを行うためのメソッド
void Refresh();

// "戻る" 履歴から最新の履歴項目を削除するためのメソッド
void RemoveBackEntry();

// 現在のナビゲーション要求に対応するコンテンツのダウンロードを中止するためのメソッド
void StopLoading();
```



## 実装側

クラス名 : NavigationService

アセンブリ : EnkuToolkit.Wpf

名前空間 : EnkuToolkit.Wpf.Services



## 使用例

DIコンテナを使用してViewModelから操作することを想定しています。



Page1ViewModel.cs

```c#
using EnkuToolkit.UiIndependent.Services;

public class Page1ViewModel : INotifyPropertyChanged
{
    private readonly INavigationService _navigationService;
    
    // コンストラクタ注入を使用する。
    public Page1ViewModel(INavigationService navigationService)
    {
        this._navigationService = navigationService;
    }
    
    // デリゲートコマンドで呼び出されるメソッド
    private void ClickedCommand()
    {
        // Page2への画面遷移
        this._navigationService.NavigateRootBase("./Page2.xaml");
    }
    
    // ~省略~
}
```





