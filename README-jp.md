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

xaml上で下記に示すすべてのクラスにアクセスするには下記のxml名前空間からアクセスしてください。

```xaml
xmlns:et="https://github.com/StdEnku/EnkuToolkit/Wpf/Controls"
```



## カスタムコントロール

| 名前                                                         | 対応バージョン | 備考                                                         |
| ------------------------------------------------------------ | -------------- | ------------------------------------------------------------ |
| [AnimatedFrame](./docs/AnimatedFrame-jp.md)                  | 1.0.0以降      | 画面遷移時にアニメーションを実行可能なFrame。                |
| [AnimatedNavigationWindow](./docs/AnimatedNavigationWindow-jp.md) | 1.0.0以降      | 画面遷移時にアニメーションを実行可能なNavigationWindow。     |
| [CustomTitlebarAnimatedNavigationWindow](./docs/CustomTitlebarAnimatedNaviagtionWindow-jp.md) | 1.0.0以降      | タイトルバーをカスタマイズ可能なAnimatedNavigationWindow。   |
| [CustomTitlebarWindow](./docs/CustomTitlebarWindow-jp.md)    | 1.0.0以降      | タイトルバーをカスタマイズ可能なAnimatedNavigationWindow。   |
| [NormalizedTransformContentControl](./docs/NormalizedTransformContentControl-jp.md) | 1.0.0以降      | すべての変形用依存関係プロパティを0~1の間で指定可能なTransformContentControl。 |
| [TransformContentControl.cs](./docs/TransformContentControl-jp.md) | 1.0.0以降      | 移動、回転、拡大を依存関係プロパティで指定可能なContentControl。 |



## 添付ビヘイビア

| 名前                                                         | 対応バージョン | 備考                                                         |
| ------------------------------------------------------------ | -------------- | ------------------------------------------------------------ |
| [TitlebarComponentsBehavior](./docs/TitlebarComponentsBehivior-jp.md) | 1.0.0以降      | CustomTitlebarWindowやCustomTitlebarAnimatedNavigationWindowのタイトルバー内のボタンなどに添付してクリック可能にするか指定するためのプロパティを持つ添付ビヘイビア。 |
| [WindowStateSaveBehavior](./docs/WindowStateSaveBehavior-jp.md) | 1.0.0以降      | Windowに添付して終了時に位置、大きさ、WindowStateプロパティを保存して次回起動時に読み込めるようにするか指定するためのプロパティを持つ添付ビヘイビア |



## ViewServices

| 名前                                                | 対応バージョン | 備考                                                         |
| --------------------------------------------------- | -------------- | ------------------------------------------------------------ |
| [MessageBoxService](./docs/MessageBoxService-jp.md) | 1.0.0以降      | MessageBoxをWPFのアセンブリに依存しないViewModelから操作するためのViewService。 |
| [NavigationService](./docs/NavigationService-jp.md) | 1.0.0以降      | System.Windows.Application.Current.MainWindowプロパティがNavigationWindowの場合使用できる画面遷移用のViewService。 |

