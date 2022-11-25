using UnityEngine;
using UnityEngine.UI;

namespace ChungjuWall.UI {
    /// <summary>
    ///     이벤트를 받아서 시간으로 변환해 UI에 적용하는 클래스
    ///
    ///     분:초초 (0:00) 형식으로 표시된다 
    /// </summary>
    public class TimeUI : MonoBehaviour {
        [Header("Initial Data")] 
        [SerializeField]
        private Text target;

        public void OnValueChanged(int value) {
            int s = value % 60;
            int m = value / 60;

            target.text = $"{m}:{s:00}";
        }
    }
}
