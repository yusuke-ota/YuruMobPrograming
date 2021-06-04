﻿# デザインパターン

このディレクトリでは、Unityで便利そうなデザインパターン等を説明します。

もっと抽象的(大きな枠組み)で設計を勉強したいときは、クリーンアーキテクチャがおすすめ。  
いろんなデザインパターンを使って、どのように依存関係を組むと変更に強い設計になるか学べる。  
ただし、その前段としてSOLID原則やデザインパターンの説明が大量にあるので、本を読む前にそのあたりは勉強しておいた方が良い。(経験則。つらかった)

## Componentパターン

詳細実装の差し替えをしやすくしたい場合、ステート(状態)がたくさんあるクラスを切り分けたい場合などに有効なパターン。

* 類似パターン
  * Strategyパターン
* 関係のあるもの
  * (一般的な≠Unityの)ステートマシーン
  * リコリス置換則

[詳細](./ComponentPatturn/ComponentPatturn.md)

## Pub/Subモデル

Push通知のように、何かあったときに、何かあった側から処理を行うモデル。  
いつ条件を満たすかわからない場合や、数をスケールさせたいときに有効なモデル。

* 関係のあるもの
  * イベント駆動
  * Rx

[詳細](./PubSub/PubSubModel.md)