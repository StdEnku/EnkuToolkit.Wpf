![logo](./imgs/logo.png)



# インストール

本ライブラリはNuget.orgにてパッケージが公開されているので<br />
VisualStudioのNugetパッケージマネージャー等を使用してインストールしてください。



## 二つのアセンブリの解説

| ダウンロード数                                                           | アセンブリ                                              | 備考                                                      |
| ------------------------------------------------------------ | ------------------------------------------------------------ | ------------------------------------------------------------ |
| <img src="https://img.shields.io/nuget/dt/EnkuToolkit.Wpf?color=indigo&logo=Nuget&style=plastic" alt="Nuget" style="zoom:200%;" /> | [EnkuToolkit.Wpf](https://www.nuget.org/packages/EnkuToolkit.Wpf/) | WPFに依存するカスタムコントロールなどが記されたアセンブリ。  |
| <img src="https://img.shields.io/nuget/dt/EnkuToolkit.UiIndependent?color=indigo&logo=Nuget&style=plastic" alt="Nuget" style="zoom:200%;" /> | [EnkuToolkit.UiIndependent](https://www.nuget.org/packages/EnkuToolkit.UiIndependent/) | ViewModel層で呼び出すことを想定したWPFに依存しない部分が記されたアセンブリ。 |

本ライブラリは上記二つのアセンブリから構成されており、<br />
EnkuToolkit.Wpfは内部でEnkuToolkit.UiIndependentに依存しているので、<br />
ViewとViewModelを一つのプロジェクトで管理する場合は<br />
EnkuToolkit.Wpfのみをインストールしてください。<br />
ViewとViewModelが別のプロジェクトの場合はView側のプロジェクトに<br />
EnkuToolkit.Wpfをインストールして、<br />
ViewModel側のプロジェクトにEnkuToolkit.UiIndependentをインストールしてください。<br />

# 機能一覧

xaml上から本ライブラリのすべてのクラスにアクセスするには下記のxml名前空間からアクセスしてください。

```xaml
xmlns:et="https://github.com/StdEnku/EnkuToolkit"
```

## カスタムコントロール

| コントロール名                                               | 備考                                                         |
| ------------------------------------------------------------ | ------------------------------------------------------------ |
| [AnimatedFrame](https://github.com/StdEnku/EnkuToolkitExamples/tree/main/00.AnimatedFrame) | 画面遷移時にアニメーションが可能なFrameクラス                |
| [AnimatedNavigationWindow](https://github.com/StdEnku/EnkuToolkitExamples/tree/main/01.AnimatedNavigationWindow) | 画面遷移時にアニメーションが可能なNavigationWindowクラス     |
| [CustomTitlebarWindow](https://github.com/StdEnku/EnkuToolkitExamples/tree/main/02.CustomTitlebarWindow) | タイトルバーをカスタマイズ可能なWindowクラス                 |
| [CustomTitlebarAnimatedNavigationWindow](https://github.com/StdEnku/EnkuToolkitExamples/tree/main/03.CustomTitlebarAnimatedNavigationWindow) | タイトルバーをカスタマイズ可能なAnimatedNavigationWindowクラス |
| [TransformContentControl](https://github.com/StdEnku/EnkuToolkitExamples/tree/main/04.TransformContentControl) | 移動、変形、拡大、等の変形操作が簡単に行えるContentControl   |
| [NormalizedTransformContentControl](https://github.com/StdEnku/EnkuToolkitExamples/tree/main/05.NormalizedTransformContentControl) | 変形用プロパティを0~1までの値で操作可能にしたTransformContentControl |

## 添付ビヘイビア

| ビヘイビア名                                                 | 備考                                                         |
| ------------------------------------------------------------ | ------------------------------------------------------------ |
| [WindowStateSaveBehavior](https://github.com/StdEnku/EnkuToolkitExamples/tree/main/06.WindowStateSaveBehavior) | Windowに添付すると終了時に現在の位置、サイズ、WidnowStateプロパティを保存して、次回起動時に以前の状態を復元させるためのビヘイビア |
| [NavigatedParamSendBehavior](https://github.com/StdEnku/EnkuToolkitExamples/tree/main/11.NavigatedParamSendBehavior) | FrameやNavigationWindowにて画面遷移を行う際に前の画面から渡されたパラメータをViewModelで取得するためのビヘイビア |


## View Service

| View Service名                                               | 備考                                                         |
| ------------------------------------------------------------ | ------------------------------------------------------------ |
| [MessageBoxService](https://github.com/StdEnku/EnkuToolkitExamples/tree/main/07.MessageBoxService) | メッセージボックスの操作をViewModelから行えるようにするためのViewServce |
| [AbstractNavigationService](https://github.com/StdEnku/EnkuToolkitExamples/tree/main/08.AbstractNavigationService) | MainWindow内のFrameなどをViewModelから画面遷移させるためのViewService |
| [MainNavigationWindowNavigationService](https://github.com/StdEnku/EnkuToolkitExamples/tree/main/09.MainNavigationWindowNavigationService) | App.Current.MainWindowがNavigationWindowの場合使用可能なViewModelから画面遷移させるためのViewService |
| [ApplicationPropertyiesService](https://github.com/StdEnku/EnkuToolkitExamples/tree/main/10.ApplicationPropertyiesService) | ViewModelからApplication.Propertyisプロパティを操作可能にするためのViewService |

マークアップ拡張

| マークアップ拡張名         | 備考               |
| -------------------------- | ------------------ |
| ViewModelProviderExtension | 近いうちに書きます |

