# Eventてなに

次のコードのようなものに出会ってよくわからなかった時に読むドキュメントです。

```C#
private void OnEnable{
    some.parameter += Function;
    // または
    some.parameta += _ =>　{/*do something*/};
}
```

Unityでの簡単な使用例が見たいときは、シーン[WhatIsEvent.unity](./WhatIsEvent.unity)を確認してください。

これ関係はdelegate, event, action, funcとありますが、delegateがわかっていれば他は理解できます。

## C#の機能 - delegate

C#には関数、メソッドを代入できるdelegateという型があります。  
関数を内部、または外部から代入して利用します。  

このメリットは、delegateを宣言している部分では、delegateから呼ばれる関数の実装を知らなくても良いことです。  
実装を知らなくても良いことで、実装の切り替えが簡単に行えます。(具体的にはdelegateに += で追加する関数を変えるだけ)

また、イベントハンドラの実装として使われることがあります。
(何か起きた時にdelegateが呼ばれるようにしておいて、そのdelegateに外部から関数を登録しておくといった使い方です)

簡単な使い方は次の通りです。  
動作を見てみたい方は
[SharpLab上で動くもの](https://sharplab.io/#v2:C4LgTgrgdgNAJiA1AHwPSoASDW5QdgyHqGQS4ZBOhkGkGQSIZAghkH95QMQZAWDUAgVQewY4BTAG1YHMBDYVwEkMAAQDMgawZA6gyAzBkBSDIHkGQEAMAWABQojG069+GIQBYMAEVYAjCFwAyAey7GtfVgAohARgAMGALasAzj55crACUANyqqkIATLouAOwA3qoYyTEAbLoGALI8AJZQjkGJKiklRqbm1rYc3A4a5ZY2djX8YcWlyeh1Zg1V9jqAMgxpGIDmDIBWDIAiDIAWDOOAigwTOMOAx3KAporS44B+DICaDP2AgAxJ7RidAKpQOcAAnsOAdmaAWAlitIBgSmJMGIgAvCOAgsoPw5vKbe02N1Kk1tKwMB8AMJWKA+KycADqYDOrFaB0OmEAhgyjQD+DGJAMoM+OGQIqjWqYLEkUAJgyAItTAA6mAkAv/GAAqU1lt/ujOppmqwmO9hoBwSLpOHpAkAtFGALO1hoBZBnZ+0B9RB5Nq7ww0Nh8NYSJRaIO8tKXMVZL6rDEgG83QBMydJAEIMgGiGUWAC0UpIAfFUAbgwcg4knqghyOABEzsAzgzrCSAfQY/aF9QBfcIAjAAB2RADdaq50vo1TC4YjkfxnO4vL5/IFCvqSq4AHSuACcjm8fgCwV1yRjKijQA==)
をどうぞ

```C#
// 他のクラスからも使う場合はdelegateをpublicにしておく。
// 現在の戻り値はvoidですがvoidじゃなくてもいいです。
public delegate void DebugLogDelegate(string message);

class Program{
    static void Main(){
        DebugLogDelegate debugLogDelegate;
        // debugLogDelegate が static でないといけないので代入していますが、
        // Unityで普通に使う分には += で十分です。
        debugLogDelegate = ConsoleWrite;
        // ちなみにここでdebugLogDelegateに2つ関数を追加しています。
        // delegateは+=で複数の関数を設定できます。
        debugLogDelegate += ConsoleWrite;

        // debugLogDelegateに登録してある関数を全て呼ぶ。
        debugLogDelegate("呼びました");
    }

    private static void ConsoleWrite(string message){
        System.Console.WriteLine(message);
    }
}
```

## C#の機能 - Event

ざっくりいうと、Delegateの呼び出し機能を宣言したクラス内のみに制限する機能。  
Delegateと大きな違いはないので、とりあえずDelegateを理解しておけば良いと思う。

publicなDelegateだと、外部から実行されてしまうので困る。  
Eventはそんな時に使うと良い。

### delegateで外部から実行されてしまう例

動作を見たい場合は、[.NET Fiddle上で動くもの](https://dotnetfiddle.net/IEPoXn)
をどうぞ
(Sharp Labでasyncがうまく使えなかったので、.NET Fiddleを使っています)

以下のように、Mainの中からMyDelegateのフィールドDebugLogDelegateを呼べてしまう。

```C#
using System;
using System.Threading.Tasks;

delegate void DebugLogDelegate(string message);

public class Program
{
    static async Task Main()
    {
        var myDelegate = new MyDelegate();
        myDelegate.DebugLogDelegate += ConsoleWrite;

        // 1秒後にDebugLogDelegate?.Invokeされます。
        var taskHandle = myDelegate.LateInvoke();

        // ここに注目
        // DebugLogDelegateがpublicなので、
        // こちらからもmyDelegate.DebugLogDelegate.invoke()出来てしまう。
        myDelegate.DebugLogDelegate("こっちからも呼べてしまう");
        await taskHandle;
    }

    private static void ConsoleWrite(string message) => Console.WriteLine(message);
}

class MyDelegate
{
    public DebugLogDelegate DebugLogDelegate;

    // 簡易実装なのでCancellation Tokenは使っていません。
    public async Task LateInvoke()
    {
        // 1秒待つ
        await Task.Delay(1000);
        DebugLogDelegate?.Invoke("asyncの中から呼んでいます");
    }
}
```

### eventで書き換えた例

```C#
using System;
using System.Threading.Tasks;

delegate void DebugLogDelegate(string message);

public class Program
{
    static async Task Main()
    {
        var myDelegate = new MyDelegate();
        myDelegate.DebugLogDelegate += ConsoleWrite;

        // 1秒後にDebugLogDelegate?.Invokeされます。
        var taskHandle = myDelegate.LateInvoke();

```

```diff
-       // ここに注目
-       // DebugLogDelegateがpublicなので、
-       // こちらからもmyDelegate.DebugLogDelegate.invoke()出来てしまう。
-       myDelegate.DebugLogDelegate("こっちからも呼べてしまう");
+       // コンパイルエラー
+       // eventは外部から実行できない
+       // myDelegate.DebugLogDelegate("こっちからも呼べてしまう");
```

```C#
        await taskHandle;
    }

    private static void ConsoleWrite(string message) => Console.WriteLine(message);
}

class MyDelegate
{
```

```diff
-   public DebugLogDelegate DebugLogDelegate;
+   public event DebugLogDelegate DebugLogDelegate;
```

```C#

    // 簡易実装なのでCancellation Tokenは使っていません。
    public async Task LateInvoke()
    {
        // 1秒待つ
        await Task.Delay(1000);
        DebugLogDelegate?.Invoke("asyncの中から呼んでいます");
    }
}
```

## Action / Func

delegate, eventの省略記法みたいなものです。  
基本、delegateがわかっていれば理解できます。

### microsoftのドキュメント

- [Action](https://docs.microsoft.com/ja-jp/dotnet/api/system.action?view=net-5.0)
- [Func](https://docs.microsoft.com/ja-jp/dotnet/api/system.func-2?view=net-5.0)
