using UnityEngine;
using UnityEngine.UI;

namespace ChungjuWall.UI {
    /// <summary>
    ///     �̺�Ʈ ���� �޾Ƽ� �����̵� ������ ��ȯ�ϴ� Ŭ����
    /// </summary>
    public class SliderUI : MonoBehaviour {
        [Header("Initial Data")] 
        [SerializeField]
        private Slider target;

        public void OnValueChanged(int value) {
            target.value = value;
        }
    }
}
