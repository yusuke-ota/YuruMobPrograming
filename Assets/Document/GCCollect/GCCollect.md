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

可変長のデータを使用するために、ヒープ領域に本体を置き、スタック領域にはヒープ領域にある本体の番地を置く。

![メモリ構造](./memory.drawio.svg)

GC.Alloc:  
ヒープ領域の空いている所の場所取りをする処理。

GC.Collect:  
GC.Allocで確保されたヒープ領域の中で、使用していない部分を手放す処理。  
ほとんどの言語で、GC Collectはとても重く、そのタイミングのみCPUの使用率が跳ね上がる。

(記憶の片隅に置いてもらえば良い知識)  
GC.AllocとGC.Collectを繰り返すと、ヒープ領域が虫食い状態になり、メモリの局所性を失う。  
キャッシュミスヒットが多くなり、メモリからキャッシュへの読み込み回数が上がり、処理速度が落ちる。  
この対策のためにUnityではNative Container(DOTS ECSの一部)が考えられている。

## UnityにおけるGC

C#のGCが動いている(はず)

GC.Alloc:  
ちょっと重い(一度に沢山使わなければ問題ない)

GC.Collect:  
とても重い。めちゃくちゃ重い。超絶重い。  
処理落ちの要因の一つ。  
VRの敵。FPSの敵。アクションゲームの敵。

一時的にGC.Allocして捨ててを繰り返すと、GC.Collectが重くなる。  
メモリを確保(Alloc)したら、その分開放(Collect)するのは自明だよね。

**過度に気にする必要はない**けど、Update等**毎フレーム呼ばれる場所に書く場合**はなるべく気にしよう。  
毎フレームGC.Allocして捨ててを繰り返すとGC.Collectが高頻度で呼ばれるようになる。

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
    private void Update(){
        _totalTime += Time.deltaTime;

-       // 今から4回GC.Allocします。
-       string timerText =　"起動してから"; // 1
-       timerText += _totalTime; // float->string: 2, +=: 3
-       timerText += "秒が経ちました。"; // 4
+       // 今から2回GC.Allocします。
+       string timerText = $"起動してから{_totalTime}秒が経ちました。"; // float->string: 1, 文字列結合: 2

        timerTextUI.text = timerText;
    }
```

#### StringBuilder

```diff
    private void Update(){
        _totalTime += Time.deltaTime;

-       // 今から4回GC.Allocします。
-       string timerText =　"起動してから"; // 1
-       timerText += _totalTime; // float->string: 2, +=: 3
-       timerText += "秒が経ちました。"; // 4
+       // 今から3回GC.Allocします。
+       StringBuilder timerText = new StringBuilder("起動してから", 40); // 1
+       timerText.Append(_totalTime); // float->string: 2
+       timerText.Append("秒が経ちました。");

-       timerTextUI.text = timerText;
+       timerTextUI.text = timerText.ToString(); // .ToString(): 3
    }
```

##### そもそもUpdateのたびにStringBuilderを作るのをやめる

```diff
+   // 初回のみ1回GC.Alloc
+   private StringBuilder timerText = new StringBuilder(40);
    private void Update(){
        _totalTime += Time.deltaTime;

-       // 今から3回GC.Allocします。
-       string timerText =　"起動してから"; // 1
-       timerText += _totalTime; // 2
-       timerText += "秒が経ちました。"; // 3
+       // 今から3回GC.Allocします。(2回目以降は2回GC.Alloc)
+       timerText.Append("起動してから");
+       timerText.Append(_totalTime); // float->string: 2(2回目からは1)
+       timerText.Append("秒が経ちました。");

-       timerTextUI.text = timerText;
+       timerTextUI.text = timerText.ToString(); // .ToString(): 3(2回目からは2)
+       timerText.Clear(); // timerTextの中身を空にして、入れ物だけ再利用する
    }
```
