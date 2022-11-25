using UnityEngine;

namespace ChungjuWall {
    /// <summary>
    ///     GPU 상의 위치에 대한 정보를 가지고 있는 클래스
    /// </summary>
    public class GpuLocation : MonoBehaviour {
        private bool _isInArea = false;
        public bool IsInArea {
            get => _isInArea;
            set => _isInArea = value;
        }
        
        public void Enter() {
            _isInArea = true;
            
            OnEnterLocation();
        }

        public void Exit() {
            _isInArea = false;
            
            OnExitLocation();
        }
        
        public virtual void OnEnterLocation() { }
        public virtual void OnExitLocation() { }
    }
}