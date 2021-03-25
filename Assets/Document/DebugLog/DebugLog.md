# Debug.Log(Message)について

## Debug.Logとは

ざっくりいうと、UnityEditor, Android LogCat, Browser Development toolなど、各実行環境に合わせた出力先に文字列を表示させることができる関数。  
プリントデバッグしたいときや、簡単に変数の中身を確認したいときなどに重宝する。

その利便性から、よく使い、放置しがちであるが、(特にスマートフォン)Debug.Logは処理にかなりのペナルティーがかかる。  
そのため、不要になったらこまめに消す癖を付けておいた方が良い。

計測例: [Qiita Debug.Log() や Instantiate() などの速度を計測してみる](https://qiita.com/Gok/items/fcfc26fa84df42c9f65d#%E8%A8%88%E6%B8%AC%E7%B5%90%E6%9E%9C)  
いくつかの関数を1000回呼び出したときの所要秒数が表になっており、Debug.Logがどのくらい重いか直観的にわかります。  
一読をおすすめします。(Debug.Log()を残そうと思わなくなること間違いなし)

### よくない例

```C#
public class CheckRun: MonoBehaviour{
    private void Update() {
        // Debug.Log()はとても重いので、使い終わったらできるだけ削除する。
        // スマートフォンアプリケーション等で、1フレームに2桁回呼ぶと処理落ちの要因になる。
        Debug.Log("動いている確認。今はもう必要ないけど...");
    }
}
```

### よくない例 改善例

1. 使い終わったDebug.Log()を消す
2. ラッピングする

#### 使い終わったDebug.Log()を消す

```Diff
public class CheckRun: MonoBehaviour{
    private void Update() {
        // Debug.Log()はとても重いので、使い終わったらできるだけ削除する。
        // スマートフォンアプリケーション等で、1フレームに2桁回呼ぶと処理落ちの要因になる。
-       Debug.Log("動いている確認。今はもう必要ないけど...");
    }
}
```

#### ラッピングする

参考: [Qiita 【Unity】 UnityEditorの時のみDebug.Logを出す方法](https://qiita.com/Gok/items/fcfc26fa84df42c9f65d)  
元は`#if ~ #endif`を使っていましたが、もっといい方法があるということで参考人させていただきました。

```diff
public class CheckRun: MonoBehaviour{
    private void Update() {
        // Debug.Log()はとても重いので、使い終わったらできるだけ削除する。
        // スマートフォンアプリケーション等で、1フレームに2桁回呼ぶと処理落ちの要因になる。
-       Debug.Log("動いている確認。今はもう必要ないけど...");
+       MyUtility.PrintDebugger.Log("動いている確認。今はもう必要ないけど...");
    }
}
```

```C#
namespace  MyUtility{
    static class PrintDebugger{
        // UnityEditor PlayerSettings -> Player -> スクリプトコンパイル -> スクリプティング定義シンボルに
        // DebugModeが打ち込んである場合呼び出す。
        [Conditional("DebugMode")]
        public static void Log(object message){
            UnityEngine.Debug.Log(message);
        }
    }
}
```
