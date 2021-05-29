using System;
using System.Collections.Generic;

namespace Document.DIContainer._4.ServiceLocator
{
    public static class ServiceLocator
    {
        // 型とインスタンスを対応付ける。
        private static readonly Dictionary<Type, object> TypeInstancePair = new Dictionary<Type, object>();

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
            if (TypeInstancePair.ContainsKey(type))
            {
                TypeInstancePair[type] = instance;
            }
            else
            {
                TypeInstancePair.Add(type, instance);
            }
        }

        // TType: 適当に名前をつけたジェネリック型です。名前に特に意味はありません。
        /// <summary>
        /// 登録しておいた型とオブジェクトの対応テーブルを用いて、型情報から生成済みのオブジェクトを受け取る。
        /// objectから目的の型へのキャストがあるので、ちょっと速度が落ちる。(機械側からは安全にキャストできるか分からないので)
        /// </summary>
        /// <param name="type">登録済みの欲しいオブジェクトの型(インターフェース)</param>
        /// <returns>型(インターフェース)に対応する具象クラスのオブジェクト</returns>
        public static TType Resolve<TType>(TType type)
        {
            return (TType)TypeInstancePair[typeof(TType)];
        }
    }
}