using UnityEngine;

namespace ChungjuWall {
    public class Wall : GpuLocation {
        [Header("Initial Data")]
        [SerializeField] 
        private GameObject _gameObject;

        public override void OnEnterLocation() {
            _gameObject.SetActive(true);
        }

        public override void OnExitLocation() {
            _gameObject.SetActive(false);
        }
    }
}