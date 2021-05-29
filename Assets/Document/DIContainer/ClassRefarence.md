# デザインパターンについて(in Unity)

Unityでよくある、Managerクラスなどの他のGamaObjectを利用する場合のデザインパターンについての説明するドキュメント。

## 要約

DIコンテナでConstructInjectionしよう。

1. 全てのMonoBehaviorのSerializeFieldにManagerクラスの参照を入れるといったことになりがち(それはそれとしてFat Managerはアンチパターン)
2. managerクラスはSingleton(デザインパターン)やstatic fieldといった手段でコードから読み込むことができる。(でも、public static valueもSingletonもアンチパターン)
3. ServiceLocatorやDIコンテナを使って参照を自動的(半手動)に解決しよう。(小規模開発以外でServiceLocatorを使うのはアンチパターン。学習コストがかかるが、DIコンテナを使おう。)

## SerializeField

サンプルプロジェクトは、こちら

SerializeFieldに設定した変数に、エディタから設定していく方式。  
プロジェクトの規模が大きくなり、SerializeFieldの数が多くなると設定漏れや間違いが増えるため、次の項目以降の方法を取られることが多いらしい。(業務経験がないので...)  

なお、現在SerializeFieldにはInterfaceを型とした変数を設定することができないので、実装の差し替えをしたい場合はDIコンテナ等を使いましょう。

## static field(property)

サンプルプロジェクトは、こちら

public static fieldやpropertyを利用して、外部から直接アクセスする方法。

```c#
public class ResourceManager: MonoBehavior
{
	public static ResourceManager Instance()
	{
		return this;
	}

	private readonly var resourceList = new List<Resource>();
	public void AddResource(resource)
	{
		resourceList.Add(resource);
	}
}

internal class Resource() : MonoBehavior
{
	private void Start()
	{
		// staticなので、こういう呼び方ができる。
		ResourceManager.Instance().AddResource(this);
	}
}
```

## Singleton

Singleton嫌いなので、サンプルプロジェクトはありません。(やる気が起きない)  
Unityでは基本的にstatic fieldで事足りることが多く、増えるコードの複雑性に対して、私の観測範囲ではメリットが少ない(ほぼ無い)。
基本的にアンチパターンとされることが多く、学習コストをかけられるのであれば、後述のDIコンテナを導入する方が良い。

## Interfaceが必要なもの

サンプルプロジェクトは、こちら

これ以降説明するServiceLocator、DIコンテナは使用する型とインスタンスが1対1対応する必要がある。(n対1だと割り当てを機械的に決定できないため)  
目印として専用のinterfaceを作成します。(直接具象classでもできるのですが、テスト時などで実装の入れ替えがしづらくなるので、interfaceを挟むようにしましょう)  
※抽象クラス: 大まかな実装(API等)のみが行われているクラス。実際にどう処理をするかは定義されない。C#ではinterfaceやabstractが使われる。
※具象クラス: 詳細な処理が実装されているクラス(処理を行うクラス)

なお、Unityでは、せっかくInterfaceを作っても、SerializeFieldにセットできないため、コード内で実装したクラスを直接参照する必要が有る。  
せっかくInterfaceを実装して、クラス間の依存を切っても、コード内で使用する実装クラスを明示する必要があり、依存が発生する。  
こういった問題を解決するためにも、ServiceLocatorやDIコンテナを使う。

```c#
internal interface IMovable
{
	internal void Move();
}

internal class MovableCube : MonoBehavior, IMovable
{
	internal void Move()
	{
		transform.localPosition += new Vector3(1.0f, 0.0f, 0.0f);
	}
}

class MoveObject : MonoBehavior
{
	[SerializeField, ToolTip("実装したクラスのインスタンス")]
	private MovableCube;
	// これができれば小規模なプロジェクトは楽で良いんだけど...
	// [SerializeField, ToolTip("実装したクラスのインスタンス")]
	// private IMovable movableObject;
	
	private void FixedUpdate()
	{
		movableObject.Move();
	}
}
```

### ServiceLocator

サンプルプロジェクトは、こちら

注:  
ServiceLocatorはInterfaceと実装(具象)classの対応付けが散らばるので、可読性が低く、小規模以外ではアンチパターン。  
Dictionaryベースで、理解しやすいので、DIコンテナまでのステップとして記述する。

素朴なServiceLocatorの実装は以下のようになり、型をキーに、インスタンスを値に持つDictionaryを作って適宜、登録、検索を行う。

```c#
// プロジェクト内の依存関係の隠蔽のために利用される
public static class ServiceLocator
{
	// 型とインスタンスを対応付ける。
	private var typeInstancePair = new Dictionary<Type, object>;
	
	public static void Resister(Type type, object instance)
	{
		typeInstancePair.Add(type, instance);
	}
	
	// TType: 適当に名前をつけたジェネリック型です。名前に特に意味はありません。
	public static TType Resolve(TType type)
	{
		return typeInstancePair[tyoeof(type)];
	}
}
```

```c#
internal interface IMovable
{
	internal void Move();
}

internal class MovableCube : MonoBehavior, IMovable
{
	internal void Move()
	{
		transform.localPosition += new Vector3(1.0f, 0.0f, 0.0f);
	}
	
	private void Start()
	{
		// こんな風にResisterがコード内に散らばりやすく、可読性が下がるので、アンチパターンとして扱われる。
		ServiceLocator.Resister(IMovable, this);
	}
}

class MoveObject : MonoBehavior
{
	private void FixedUpdate()
	{
		ServiceLocator.Resolve(IMovable).Move();
	}
}
```

上記の例では、MovableCubeのvoid Start()でServiceLocatorに登録している。  
ServiceLocatorでは、インスタンス宣言部分の近くで登録されやすく（上記の例然り）、登録処理がプロジェクトのあらゆる場所に散らばりやすい。  
その結果、可読性の低下が起こりうるため、アンチパターンとして扱われることも多い。

登録部分を１つの関数にまとめることができればアンチパターンにはなりにくい。(そこまでするとDIコンテナへの乗り換えが容易である)  
DIコンテナに勝る部分としては、ServiceLocatorは実装がハッシュテーブルなので、リフレクションが使われることが多い（らしい）DIコンテナより高速であること。

### DIコンテナ

サンプルプロジェクトは、こちら

DependencyInjection(DI)と呼ばれる、使用する具象クラスのオブジェクト(依存)を引数として、外部から与えよう(注入)というパターンがある。  
DIの機能を提供するフレームワークとしてDIコンテナが登場した。
(DI = DIコンテナを使うことではないので注意 UnityのSerializeFieldは一種のDIではないかという意見もある)

インスタンス生成時にDIするConstruct Injection、メソッド呼び出し時にDIするMethod Injectionが各ライブラリでほぼ共通する機能。  
ただし、Method InjectionはDIコンテナが無いと使えない(DIコンテナに依存する)ため、不要であれば使わないようにと注意書きするライブラリもある。(クラスを疎結合にするためにDIしているのに、より大きいDIコンテナに密結合するのはどうなのという意見)

ここでは、VContainerというライブラリを利用する。

