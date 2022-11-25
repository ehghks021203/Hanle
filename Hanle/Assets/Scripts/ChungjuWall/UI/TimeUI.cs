using UnityEngine;
using UnityEngine.UI;

namespace ChungjuWall.UI {
    /// <summary>
    ///     �̺�Ʈ�� �޾Ƽ� �ð����� ��ȯ�� UI�� �����ϴ� Ŭ����
    ///
    ///     ��:���� (0:00) �������� ǥ�õȴ� 
    /// </summary>
    public class TimeUI : MonoBehaviour {
        [Header("Initial Data")] 
        [SerializeField]
        private Text target;

        public void OnValueChanged(int value) {
            int s = value % 60;
            int m = value / 60;

            target.text = $"{m}:{s:00}";
        }
    }
}
