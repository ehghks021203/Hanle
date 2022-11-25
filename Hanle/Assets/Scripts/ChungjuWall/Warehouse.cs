using UnityEngine;

namespace ChungjuWall {
    public class Warehouse : GpuLocation {
        [Header("Initial Data")]
        [SerializeField] 
        private GameObject _gameObject;

        [SerializeField]
        private GameObject _ui;
        
        /// <summary>
        ///     물류 창고를 누르면 모아놓은 보급품들을 적용한다.
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