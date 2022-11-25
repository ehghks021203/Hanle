using UnityEngine;

namespace ChungjuWall {
    public class Warehouse : GpuLocation {
        [Header("Initial Data")]
        [SerializeField] 
        private GameObject _gameObject;

        [SerializeField]
        private GameObject _ui;
        
        /// <summary>
        ///     ���� â�� ������ ��Ƴ��� ����ǰ���� �����Ѵ�.
        /// </summary>
        public void Interact() {
            Game.Instance.ApplySupplies();
        }

        public override void OnEnterLocation() {
            _gameObject.SetActive(true);
            _ui.SetActive(true);
        }

        public override void OnExitLocation() {
            _gameObject.SetActive(false);
            _ui.SetActive(false);
        }
    }
}