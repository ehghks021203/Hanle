using System;
using UnityEngine;

namespace ChungjuWall.UI {
    /// <summary>
    ///     UI 가 특정 게임 오브젝트를 따라다니도록 하는 클래스
    /// </summary>
    public class AttachedUI : MonoBehaviour {
        /// <summary>
        ///     따라다닐 오브젝트
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