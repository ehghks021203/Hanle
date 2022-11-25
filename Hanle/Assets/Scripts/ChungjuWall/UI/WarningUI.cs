using UnityEngine;

namespace ChungjuWall.UI {
    public class WarningUI : MonoBehaviour {
        [SerializeField] 
        private GameObject _target;
        
        // Start is called before the first frame update
        private void Start() {
            Game.Instance.SuppliesChanged.AddListener(OnSuppliesChanged);
        }

        private void OnSuppliesChanged(int value) {
            _target.SetActive(value == 0);
        }
    }
}