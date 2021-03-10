# Unity / C#のGCについて

## GCとは

G(ガーベッジ)C(コレクション)

使用していないメモリを回収して、利用可能なメモリを増やす機能。  

多くの高級言語に標準搭載されている。  
例: Basic, C#, Go, Java, Python, Ruby etc..

...というより、標準搭載していない言語が少ない。  
C, C++, Fortran, Rust ...あと何があるっけ?

## C#におけるGC

GC.Alloc: メモリを確保する  
GC.Collect: 確保したメモリを開放する

固定長配列(`T[]`,`string`)や可変配列(`List<T>`)、クラスを使うときにメモリを確保する。

## C#アプリケーションのメモリ構成

スタック領域(固定長データの領域)とヒープ領域(可変長データの領域)に分けられる。

todo: 図示

## UnityにおけるGC

C#のGCが動いている

GC.Alloc:  
ちょっと重い(一度に沢山使わなければ問題ない)

GC.Collect:  
とても重い。めちゃくちゃ重い。超絶重い。  
処理落ちの要因の一つ。  
VRの敵。FPSの敵。アクションゲームの敵。

一時的にGC.Allocして捨ててを繰り返すと、GC.Collectが重くなる。  
メモリを確保(Alloc)したら、その分開放(Collect)するのは自明だよね。

### よくない例1

```C#
public class DisplayTime: MonoBehaviour {
    [Header("UI設定部分")]
    [SerializeField, Tooltip("タイマー表示UIのテキスト部分をアタッチしてください")]
    private Text timerTextUI;

    private float _totalTime = 0;
    private void Update(){
        _totalTime += Time.deltaTime;

        // 今から3回GC.Allocします。
        string timerText =　"起動してから"; // 1
        timerText += _totalTime; // 2
        timerText += "秒が経ちました。"; // 3

        timerTextUI.text = timerText;
    }
}
```

### よくない例1 改善例

決まった文字列を結合するなら文字列補完を使おう。  
for文で結合するならStringBuilderで作ろう。  

参考: MicroSoft: C# のコーディング規則 - 文字列型 (String)  
URL: <https://docs.microsoft.com/ja-jp/dotnet/csharp/programming-guide/inside-a-program/coding-conventions#string-data-type>

#### 文字列補完

```diff
-       // 今から4回GC.Allocします。
-       string timerText =　"起動してから"; // 1
-       timerText += _totalTime; // float->string: 2, +=: 3
-       timerText += "秒が経ちました。"; // 4
-
-       timerTextUI.text = timerText;
+       // 今から2回GC.Allocします。
+       string timerText = $"起動してから{_totalTime}秒が経ちました。"; // float->string: 1, 文字列結合: 2
+
+       timerTextUI = timerText;
```

#### StringBuilder

```diff
-       // 今から4回GC.Allocします。
-       string timerText =　"起動してから"; // 1
-       timerText += _totalTime; // float->string: 2, +=: 3
-       timerText += "秒が経ちました。"; // 4
-
-       timerTextUI.text = timerText;
+       // 今から3回GC.Allocします。
+       StringBuilder timerText = new StringBuilder("起動してから", 40); // 1
+       timerText.Append(_totalTime); // float->string: 2
+       timerText.Append("秒が経ちました。");
+
+       timerTextUI.text = timerText.ToString(); // .ToString(): 3
```
