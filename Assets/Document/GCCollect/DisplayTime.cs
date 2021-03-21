using UnityEngine;
using UnityEngine.UI;

namespace Document.GCCollect
{
    public class DisplayTime: MonoBehaviour {
        [Header("UI設定部分")]
        [SerializeField, Tooltip("タイマー表示UIのテキスト部分をアタッチしてください")]
        private Text timerTextUI;

        private float _totalTime = 0;
        private void Update(){
            _totalTime += Time.deltaTime;

            // 今から4回GC.Allocします。
            var timerText =　"起動してから\n"; // 1
            timerText += _totalTime; // float->string: 2, +=: 3
            timerText += "\n秒が経ちました。"; // 4

            timerTextUI.text = timerText;

            // 文字列補完
            // 今から2回GC.Allocします。
            // string timerText = $"起動してから\n{_totalTime}\n秒が経ちました。"; // float->string: 1, 文字列結合: 2
            //
            // timerTextUI.text = timerText;

            // StringBuilder
            // StringBuilder timerText = new StringBuilder("起動してから\n", 40); // 1
            // timerText.Append(_totalTime); // float->string: 2
            // timerText.Append("\n秒が経ちました。");
            //
            // timerTextUI.text = timerText.ToString(); // .ToString(): 3
        }
    }
}
