using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Document.DebugLog
{
    public class DebugLog : MonoBehaviour
    {
        private void Update()
        {
            // Debug.Log()はとても重いので、使い終わったらできるだけ削除する。
            // スマートフォンアプリケーション等で、1フレームに2桁回呼ぶと処理落ちの要因になる。
            Debug.Log("動いている確認。今はもう必要ないけど...");

            // ラッピングする
            // MyUtility.PrintDebugger.Log("動いている確認。今はもう必要ないけど...");
        }
    }

    // // ラッピングする
    namespace MyUtility
    {
        internal static class PrintDebugger
        {
            // UnityEditor PlayerSettings -> Player -> スクリプトコンパイル -> スクリプティング定義シンボルに
            // DebugModeが打ち込んである場合呼び出す。
            [Conditional("DebugMode")]
            public static void Log(object message)
            {
                Debug.Log(message);
            }
        }
    }
}