using UnityEngine;
using UnityEngine.UI;

namespace ChungjuWall.UI {
    /// <summary>
    ///     보급 아이템에 관련된 UI를 다루는 클래스
    /// </summary>
    public class SupplyItemUI : MonoBehaviour {
        /// <summary>
        ///     정보를 포시할 타겟
        /// </summary>
        [Header("Initial Data")] 
        [SerializeField]
        private Text target;

        /// <summary>
        ///     On Off 할 오브젝트
        /// </summary>
        [SerializeField]
        private GameObject _panelObject;

        public void OnSpawnDelayBegin() {
            _panelObject.SetActive(true);
        }

        public void OnSpawnDelayEnd() {
            _panelObject.SetActive(false);
        }

        public void OnSpawnDelayChanged(float time) {
            target.text = $"{time:0.0.#}초";
        }
    }
}