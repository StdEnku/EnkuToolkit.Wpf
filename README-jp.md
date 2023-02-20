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

| コントロール名                                               | 備考                                                         |
| ------------------------------------------------------------ | ------------------------------------------------------------ |
| [AnimatedFrame](./docs/AnimatedFrame-jp.md)                  | 画面遷移時にアニメーションが可能なFrameクラス                |
| [AnimatedNavigationWindow](./docs/AnimatedNavigationWindow-jp.md) | 画面遷移時にアニメーションが可能なNavigationWindowクラス     |
| [CustomTitlebarWindow](./docs/CustomTitlebarWindow-jp.md)    | タイトルバーをカスタマイズ可能なWindowクラス                 |
| [CustomTitlebarAnimatedNavigationWindow](./docs/CustomTitlebarAnimatedNavigationWindow-jp.md) | タイトルバーをカスタマイズ可能なAnimatedNavigationWindowクラス |
| [TransformContentControl](./docs/TransformContentControl-jp.md) | 移動、変形、拡大、等の変形操作が簡単に行えるContentControl   |
| [NormalizedTransformContentControl](./docs/NormalizedTransformContentControl-jp.md) | 変形用プロパティを0~1までの値で操作可能にしたTransformContentControl |



## 添付ビヘイビア

| ビヘイビア名                                                 | 備考                                                         |
| ------------------------------------------------------------ | ------------------------------------------------------------ |
| [WindowStateSaveBehavior](./docs/WindowStateSaveBehavior-jp.md) | Windowに添付すると終了時に現在の位置、サイズ、WidnowStateプロパティを保存して、次回起動時に以前の状態を復元させるためのビヘイビア |



## View Service

| View Service名                                      | 備考                                                         |
| --------------------------------------------------- | ------------------------------------------------------------ |
| [NavigationService](./docs/NavigationService-jp.md) | Application.Current.MainWindowがNavigationWindowの場合、使用可能なViewModelから画面遷移を実行するためのViewService |
| [MessageBoxService](./docs/MessageBoxService-jp.md) | メッセージボックスの操作をViewModelから行えるようにするためのViewServce |

