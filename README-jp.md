![logo](./docs/imgs/logo.png)



# インストール

本ライブラリはNuget.orgにてパッケージが公開されているので

VisualStudioのNugetパッケージマネージャー等を使用してインストールしてください。



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



## カスタムコントロール

| コントロール名                                               | 備考                                                     |
| ------------------------------------------------------------ | -------------------------------------------------------- |
| [AnimatedFrame](./docs/AnimatedFrame-jp.md)                  | 画面遷移時にアニメーションが可能なFrameクラス            |
| [AnimatedNavigationWindow](./docs/AnimatedNavigationWindow-jp.md) | 画面遷移時にアニメーションが可能なNavigationWindowクラス |

