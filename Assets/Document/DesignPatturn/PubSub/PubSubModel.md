# Pub/Subモデル

配信用の口を作っておき、必要に応じて登録して、変更を受けとるモデル。  
配信側で、配信したいタイミングを設定しておき、動作側からは必要に応じて登録を行う。  
簡易的なPush通知と考えると分かりやすいかも。

利点としては、動作側から勝手に追加でき、スケールしやすい点、動作側でタイミングを認識する必要がない(push型)点があげられる。

push型: 変化があった際に、配信側から動作側に通知を送り、通知が来たら動作側が処理を行う
pull型: 一定の周期で動作側から変化がないか問い合わせて、変化があるときのみ処理をする

例えば、プレイヤーがテレポート移動したとき、敵キャラがプレイヤーの探索を開始するコードを考える。
プレイヤーのコードとしては、

```C#
class Player
{
    private Func<void, void> schedule;
    // 登録されている処理を実行する
    private void Publish()
    {
        if (schedule == null) return;

        foreach(event in schedule)
        {
            event;
        }
    }

    // 外部から登録する
    public void Subscrive(Func<void, void> event)
    {
        schedule += event;
    }
}
```

といったものが考えられる。  
このコードに、敵キャラのコードから動作を登録し

```C#
class Enemy
{
    private bool foundPlayer;
    public Enemy(Player player)
    {
        player.Subscrive(() => {
            foundPlayer = false;
            SerchPlayer();
        });
    }
    
    private void SerchPlayer()
    [
        // プレイヤーを見つける処理
        foundPlayer = true;
    }
}
```

プレイヤーの動作から敵キャラの動作を起こす。  
このモデルの利点は、操作のあったタイミングに勝手に敵キャラの動作が呼ばれる面にあり、タイミングが重要な場合に有効である。  
簡易的なpush通知のモデルと近似できるので、この操作はpush通知で動いてほしいかどうかで利用の可否を考えると良いかもしれない。
