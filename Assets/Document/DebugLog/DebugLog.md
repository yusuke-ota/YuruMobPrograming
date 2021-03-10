# Debug.Log(Message)について

## Debug.Logとは

ざっくりいうと、UnityEditor, Android LogCat, Browser Development toolなど、各実行環境に合わせた出力先に文字列を表示させることができる関数。  
プリントデバッグしたいときや、簡単に変数の中身を確認したいときなどに重宝する。

その利便性から、よく使い、放置しがちであるが、(特にスマートフォン)Debug.Logは処理にかなりのペナルティーがかかる。  
そのため、不要になったらこまめに消す癖を付けておいた方が良い。

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
-       Debug.Log("動いている確認。今はもう必要ないけど...");
```

#### ラッピングする

```C#
namespace  MyUtility{
    static class PrintDebugger{
        public static void Log(object message){
// UnityEditor PlayerSettings -> Player -> スクリプトコンパイル -> スクリプティング定義シンボルに
// DebugModeが打ち込んであるかどうかで分岐する。
#if DebugMode
            Debug.Log(message);
#endif
        }
    }
}
```

```diff
-       Debug.Log("動いている確認。今はもう必要ないけど...");
+       MyUtility.PrintDebugger.Log("動いている確認。今はもう必要ないけど...");
```
