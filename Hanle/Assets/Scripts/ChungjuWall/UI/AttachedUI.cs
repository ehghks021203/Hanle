using System;
using UnityEngine;

namespace ChungjuWall.UI {
    /// <summary>
    ///     UI �� Ư�� ���� ������Ʈ�� ����ٴϵ��� �ϴ� Ŭ����
    /// </summary>
    public class AttachedUI : MonoBehaviour {
        /// <summary>
        ///     ����ٴ� ������Ʈ
        /// </summary>
        [Header("Initial Data")]
        public GameObject target;

        [SerializeField]
        private Vector3 offset = new Vector3(0, -0.8f, 0);

        private void OnEnable() {
            if (target != null) {
                transform.position = Camera.main.WorldToScreenPoint(target.transform.position + offset);
            }
        }

        // Update is called once per frame
        void FixedUpdate() {
            if (target != null) {
                transform.position = Camera.main.WorldToScreenPoint(target.transform.position + offset);
            }
        }
    }
}