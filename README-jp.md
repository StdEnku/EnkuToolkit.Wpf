![logo](./docs/imgs/logo.png)



# インストール

本ライブラリはNuget.orgにてパッケージが公開されているので

VisualStudioを使用してインストールしてください。



## 二つのアセンブリの解説

| ダウンロード数 | URL  | アセンブリ名              | 備考                                                         |
| -------------- | ---- | ------------------------- | ------------------------------------------------------------ |
| -              | -    | EnkuToolkit.Wpf           | WPFに依存するカスタムコントロールなどが記されたアセンブリ。  |
| -              | -    | EnkuToolkit.UiIndependent | ViewModel層で呼び出すことを想定したWPFに依存しない部分が記されたアセンブリ。 |

本ライブラリは上記二つのアセンブリから構成されており、

EnkuToolkit.Wpfは内部でEnkuToolkit.UiIndependentに依存しているので、

ViewとViewModelを一つのプロジェクトで管理する場合は

EnkuToolkit.Wpfのみをインストールしてください。

ViewとViewModelが別のプロジェクトの場合はView側のプロジェクトに

EnkuToolkit.Wpfをインストールして、

ViewModel側のプロジェクトにEnkuToolkit.UiIndependentをインストールしてください。



# 機能一覧

xaml上から本ライブラリのすべてのクラスにアクセスするには下記のxml名前空間からアクセスしてください。

```xaml
xmlns:et="https://github.com/StdEnku/EnkuToolkit/Wpf/Controls"
```



- [アニメーション付きの画面遷移を行う。](./docs/アニメーション付きの画面遷移を行う.md)
- [Windowのタイトルバーをカスタマイズする。](./docs/Windowのタイトルバーをカスタマイズする.md)
- [Window起動時に以前の位置、サイズ、WindowStateを読み込む。](./docs/Window起動時に以前の位置、サイズ、WindowStateを読み込む.md)
- [バインディングで変形操作可能なコントロールを使う。](./docs/バインディングで変形操作可能なコントロールを使う.md)
- [ViewModelからMessageBoxの操作を行う。](./docs/ViewModelからMessageBoxの操作を行う.md)
- [ViewModelからNavigationWindowを操作する。](./docs/ViewModelからNavigationWindowを操作する.md)

