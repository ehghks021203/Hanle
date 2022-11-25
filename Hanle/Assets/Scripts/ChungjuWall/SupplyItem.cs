using UnityEngine;

namespace ChungjuWall {
    /// <summary>
    ///     ���� �������� �����ϴ� Ŭ����
    /// </summary>
    public class SupplyItem : Supply {
        /// <summary>
        ///     On, Off �� ������Ʈ
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
