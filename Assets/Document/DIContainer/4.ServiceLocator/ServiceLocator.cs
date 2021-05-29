using System.Diagnostics;

namespace Document.DebugLog
{
    public static class ServiceLocator
    {
        // 型とインスタンスを対応付ける。
        private var typeInstancePair = new Dictionary<Type, object>;

        /// <summary>
        /// 型と具象オブジェクトを対応付ける。
        /// ただし自動的に解決するため、1対1である必要がある。同じ型に違う具象オブジェクトを何度も入れることはできない。
        /// </summary>
        /// <param name="type">登録する型(インターフェース)</param>
        /// <param name="instance">具象オブジェクト</param>
        /// <returns>void</returns>
        public static void Resister(Type type, object instance)
        {
			// typeとinstanceは1対1である必要があるため、すでにあった場合は入れ替える処理を行う。
			// エラーを出しても良い。
            if (typeInstancePair.Containe(type))
            {
                typeInstancePair[type] = instance;
            }
            else
            {
                typeInstancePair.Add(type, instance);
            }
        }

        // TType: 適当に名前をつけたジェネリック型です。名前に特に意味はありません。
        /// <summary>
        /// 登録しておいた型とオブジェクトの対応テーブルを用いて、型情報から生成済みのオブジェクトを受け取る。
        /// </summary>
        /// <param name="type">欲しいオブジェクトの型(インターフェース)</param>
        /// <returns>型(インターフェース)に対応する具象クラスのオブジェクト</returns>
        public static TType Resolve(TType type)
        {
            return typeInstancePair[tyoeof(type)];
        }
    }
}