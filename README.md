3/13
使うUnityのVerは2022.3.21f1LTS
まず、簡単なカメラアプリを作成
その後、各機能の追加の流れで開発する

UnityのテンプレートにはAR開発のものもあるが、今回初めてなので初期設定の行い方も学ぶ目的で３Dテンプレートスタートで作成する。

ARカメラを使う方法もあるみたいだけど一旦 WebCamTextureを使う方向で進める。
課題はモバイル対応

フロー
1、普通のカメラアプリ作る
2、Monotone加工をする
3、UIの実装、デザイン、カウントダウン含め
4、android対応（MacがないのでiPhoneは実機で確認できない）


3/15
android端末に画像を保存する為外部アセット導入
https://github.com/yasirkula/UnityNativeGallery
