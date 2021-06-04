using UnityEngine;

namespace Document.WhatIsEvent
{
    public class MovableCube: MonoBehaviour
    {
        // 同じフォルダ内の Delegate.cs内の Delegateクラスをさす。
        [SerializeField] private Delegate myDelegate;
        
        // MovableCubeか消えてもまだDelegateが生きている場面が考えられる。(MovableCubeをDestroyしたときとか)
        // そういった場面で、DelegateからMoveCubeが呼ばれ続けるのは良くない(Null参照エラーが流れ続ける)
        // なので、OnEnableで関数を追加、OnDisableで関数を削除する必要がある。
        private void OnEnable() => myDelegate.MoveDelegate += MoveCube;
        private void OnDisable() => myDelegate.MoveDelegate -= MoveCube;

        // このメソッドはDelegate.cs内の Delegateクラスのupdateで呼ばれる
        private void MoveCube(Vector3 moveAmount)
        {
            transform.localPosition += moveAmount * Time.deltaTime;
        }

        private void Update()
        {
            // 外部から触れるdelegateだとこんなことが出来てしまってよろしくない
            myDelegate.DebugLogDelegate("こっちからも呼べてしまう");
            // eventだとこれはできない(コンパイルエラー)
            // myDelegate.MoveDelegate(new Vector3(1, 0, 1));
        }
    }
}