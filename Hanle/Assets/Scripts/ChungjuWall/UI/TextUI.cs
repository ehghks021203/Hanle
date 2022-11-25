using UnityEngine;
using UnityEngine.UI;

namespace ChungjuWall.UI {
    /// <summary>
    ///     �̺�Ʈ�� �Ѿ�� ���� �ؽ�Ʈ�� �����Ű�� Ŭ����
    /// </summary>
    public class TextUI : MonoBehaviour
    {
        [Header("Initial Data")] 
        [SerializeField]
        private Text target;

        public void OnValueChanged(int value) {
            target.text = $"{value}";
        }
    }
}
