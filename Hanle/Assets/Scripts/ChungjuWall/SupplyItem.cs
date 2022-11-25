using UnityEngine;

namespace ChungjuWall {
    /// <summary>
    ///     보급 아이템을 관리하는 클래스
    /// </summary>
    public class SupplyItem : Supply {
        /// <summary>
        ///     On, Off 할 오브젝트
        /// </summary>
        [Header("Initial Data")] 
        [SerializeField]
        private GameObject _itemsObject;

        [SerializeField]
        private GameObject _supplyBox;

        [SerializeField]
        private GameObject _ui;

        void Start() {
            SpawnDelayBegin.AddListener(OnSpawnDelayBegin);
            SpawnDelayEnd.AddListener(OnSpawnDelayEnd);

            SpawnDelayEnd.Invoke();
        }

        public void OnSpawnDelayBegin() {
            if (IsInArea) {
                _supplyBox.SetActive(true);
                _ui.SetActive(true);
                _itemsObject.SetActive(false);
            }
        }

        public void OnSpawnDelayEnd() {
            if (IsInArea) {
                _supplyBox.SetActive(true);
                _ui.SetActive(false);
                _itemsObject.SetActive(true);
            }
        }

        public override void OnEnterLocation() {
            _supplyBox.SetActive(true);
            _ui.SetActive(IsInSpawnDelay);
            _itemsObject.SetActive(!IsInSpawnDelay);
        }

        public override void OnExitLocation() {
            _supplyBox.SetActive(false);
            _ui.SetActive(false);
            _itemsObject.SetActive(false);
        }
    }
}
