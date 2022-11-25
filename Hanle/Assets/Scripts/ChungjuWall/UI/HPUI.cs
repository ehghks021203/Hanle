using UnityEngine;
using UnityEngine.UI;

namespace ChungjuWall.UI {
    /// <summary>
    ///     이벤트 값을 받아서 채력 값으로 변환하는 클래스 
    /// </summary>
    public class HPUI : MonoBehaviour {
        [Header("Initial Data")] 
        [SerializeField]
        private Text target;

        public void OnValueChanged(int value) {
            target.text = $"채력 ({value}/100)";
        }
    }
}