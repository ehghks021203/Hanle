using UnityEngine;

namespace ChungjuWall {
    /// <summary>
    ///     GPU ���� ��ġ�� ���� ������ ������ �ִ� Ŭ����
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