# YuruMobPrograming

## What is this

Unity初心者向けに行うモブプログラミングのテンプレートプロジェクト(のつもり)  
注意事項と必要そうなコードを順次追加していく予定

## Document

### 作成上の注意点

- [DebugLog.md](./Assets/Document/DebugLog/DebugLog.md)
  - Debug.Logは重いよ。使ったら消そう。という話。
- [GCCollect.md](./Assets/Document/GCCollect/GCCollect.md)
  - GC Collectで時々フレームレートがガクッと落ちるよという話。
  - 2020.3ではインクリメンタルGCが標準でOnになっているので、そこまでシビアにならなくても良い。
- [ObjectPool.md](./Assets/Document/ObjectPool/ObjectPool.md)
  - 同じシーンで何回も使いまわせるような物は、使いまわそうという話。

### C#の知識

- [Eventてなに.md](./Assets/Document/WhatIsEvent/WhatIsEvent.md)
  - `some.parameter += Function;`みたいなコードに出会って、解読できなかった時用。
- (todo)設計: もしかするとDIまで行くかも

### パッケージの使い方

- InputSystem:  
  テラシュールブログを見て。 初めて使うときは[こっち](http://tsubakit1.hateblo.jp/entry/2019/10/13/143530), 他の使い方は[こっち](http://tsubakit1.hateblo.jp/entry/2019/01/09/001510)
- (todo)Cinemachine:  
  多分他に良いドキュメントがあるのでそれにリンクする

## UtilityCode

- ObjectPool: [./Assets/ObjectPool/ObjectPool.cs](./Assets/ObjectPool/ObjectPool.cs)
