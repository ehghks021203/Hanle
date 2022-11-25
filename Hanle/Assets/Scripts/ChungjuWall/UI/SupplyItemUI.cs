using UnityEngine;
using UnityEngine.UI;

namespace ChungjuWall.UI {
    /// <summary>
    ///     ���� �����ۿ� ���õ� UI�� �ٷ�� Ŭ����
    /// </summary>
    public class SupplyItemUI : MonoBehaviour {
        /// <summary>
        ///     ������ ������ Ÿ��
        /// </summary>
        [Header("Initial Data")] 
        [SerializeField]
        private Text target;

        /// <summary>
        ///     On Off �� ������Ʈ
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
            target.text = $"{time:0.0.#}��";
        }
    }
}