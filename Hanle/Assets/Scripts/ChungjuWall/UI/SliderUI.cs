using UnityEngine;
using UnityEngine.UI;

namespace ChungjuWall.UI {
    /// <summary>
    ///     이벤트 값을 받아서 슬라이드 값으로 변환하는 클래스
    /// </summary>
    public class SliderUI : MonoBehaviour {
        [Header("Initial Data")] 
        [SerializeField]
        private Slider target;

        public void OnValueChanged(int value) {
            target.value = value;
        }
    }
}
