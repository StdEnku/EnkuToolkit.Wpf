# Change Log



## [1.0.7] - 2023-02-28

### Fixed

- CustomTitlebar系ウィンドウコントロールにて、画面を最大化するとタイトルバーとコンテンツとの間に余白ができてしまう問題を修正



## [1.0.8] - 2023-02-28

### Changed

- 画面遷移時にパラメータをViewModelで受け取るためのINavigatedParamReceiveインターフェースのNavigatedメソッドに実行された画面遷移の種類(BackやForwardなど)を取得するための第二引数modeを追加



## [1.0.9] - 2023-02-28

### Changed

- `INavigationService`インターフェースの`RemoveAllHistory`メソッドが履歴をすべて削除するのではなくBack方向への履歴をすべて削除するメソッドであるため紛らわしいので名前を`RemoveAllBackEntry`へ修正



## [1.0.10] - 2023-02-28

### Removed

- v1.0.8で実装されたINavigatedParamReceiveインターフェースのNavigatedメソッドから画面遷移の種類を取得できる機能を削除

### Changed

- NavigatedParamSendBehaviorを使用して画面遷移した際遷移先INavigatedParamReceiveインターフェースが実装されたViewModelのNavigatedメソッドはパラメータが以前の画面から渡されていない場合実行されないように修正



## [1.0.11] - 2023-02-28

### Changed

- v1.0.7の頃と同じようにNavigatedParamSendBehaviorを使用して画面遷移した際遷移先INavigatedParamReceiveインターフェースが実装されたViewModelのNavigatedメソッドがパラメータが以前の画面から渡されていない場合でも実行されるように修正して、引数extraDataをNull許容型に変更
