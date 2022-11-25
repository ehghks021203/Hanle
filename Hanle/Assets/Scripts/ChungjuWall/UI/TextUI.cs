using UnityEngine;
using UnityEngine.UI;

namespace ChungjuWall.UI {
    /// <summary>
    ///     이벤트로 넘어온 값을 텍스트에 적용시키는 클래스
    /// </summary>
    public class TextUI : MonoBehaviour
    {
        [Header("Initial Data")] 
        [SerializeField]
        private Text target;

        public void OnValueChanged(int value) {
            target.text = $"{value}";
        }
    }
}
