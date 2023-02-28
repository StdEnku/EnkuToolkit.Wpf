# Change Log



## [1.0.7] - 2023-02-28

### Fixed

- CustomTitlebar系ウィンドウコントロールにて、画面を最大化するとタイトルバーとコンテンツとの間に余白ができてしまう問題を修正



## [1.0.8] - 2023-02-28

### Changed

- 画面遷移時にパラメータをViewModelで受け取るためのINavigatedParamReceiveインターフェースのNavigatedメソッドに実行された画面遷移の種類(BackやForwardなど)を取得するための第二引数modeを追加



## [1.0.8] - 2023-02-28

### Changed

- `INavigationService`インターフェースの`RemoveAllHistory`メソッドが履歴をすべて削除するのではなくBack方向への履歴をすべて削除するメソッドであるため紛らわしいので名前を`RemoveAllBackEntry`へ修正
