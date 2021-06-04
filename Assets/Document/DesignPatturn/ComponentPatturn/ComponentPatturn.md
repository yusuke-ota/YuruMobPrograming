# Componentパターン

キーワードは「継承するな委譲しろ」(ここでいう委譲はC#のdelegateとは別なので注意)

詳細を変数(参照)として外部に持っておくパターン。  
詳細部分を外部においているので、差し替えが楽。  
多分一番身近なのはUnityのインスペクタ上のコンポーネントの操作。(GameObjectにTransformなどのコンポーネントがついており、必要に応じて、コライダーやメッシュを追加、削除できる)

類似パターン: Strategyパターン  
関係のあるもの: StateMachine、リコリス置換則

```C#
class Businessman
{
    private uint _id;
    private ICompany _company;
    private IJob _job;
    private IPost _post;

    public Businessman(uint id, ICompany company, IJob job, IPost post)
        => (_id, _company, _job, _post) = (id, company, job, post);
    public void Outplace(ICompany newCompany) => _company = newCompany;
    public void JobChenge(IJob newJob) => _job = newJob;
    public void ChangePost(IPost newPost) => _post = newPost;

    public void Work() => _job.Work();
}

// 省略

public interface IJob
{
    public void Work();
}

public class CompanyA_PG: IJob
{
    // 仕事内容は職種で変わるので、入れ替えられるようにしたい
    public void Work(){/* お仕事する */}
}
// 以下略
```
