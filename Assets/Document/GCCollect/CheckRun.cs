﻿using UnityEngine;

namespace Document.GCCollect
{
    public class CheckRun: MonoBehaviour{
        private void Update() {
            // Debug.Log()はとても重いので、使い終わったらできるだけ削除する。
            // スマートフォンアプリケーション等で、1フレームに2桁回呼ぶと処理落ちの要因になる。
            Debug.Log("動いている確認。今はもう必要ないけど...");
        
            // ラッピングする
            // MyUtility.PrintDebugger.Log("動いている確認。今はもう必要ないけど...");
        }
    }

    // ラッピングする
    // namespace  MyUtility{
    //     static class PrintDebugger{
    //         public static void Log(object message){
    // #if DebugMode
    //             Debug.Log(message);
    // #endif
    //         }
    //     }
    // }
}
