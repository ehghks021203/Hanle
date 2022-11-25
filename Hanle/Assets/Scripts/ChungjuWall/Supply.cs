using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace ChungjuWall {
    /// <summary>
    ///     ����ǰ ���̽� Ŭ����
    /// </summary>
    public class Supply : GpuLocation {
        public UnityEvent<float> SpawnDelayChanged = new UnityEvent<float>();
        public UnityEvent SpawnDelayBegin = new UnityEvent();
        public UnityEvent SpawnDelayEnd = new UnityEvent();

        /// <summary>
        ///     �ش� ����ǰ�� �����ϸ� ���� ���� ��
        /// </summary>
        [Header("Initial Data")]
        [SerializeField]
        private int _supplyPoint;

        /// <summary>
        ///     �ش� ����ǰ�� �ٽ� ä������ ���� ������
        /// </summary>
        [SerializeField]
        private int _spawnDelayTime;

        /// <summary>
        ///     ����ǰ�� �ٽ� ä������ ���� ���� �ð�
        /// </summary>
        [Header("Data")]
        [SerializeField]
        private float _remainingSpawnDelay = 0;

        /// <summary>
        ///     �ش� ����ǰ�� �����ϸ� ���� ���� ��
        /// </summary>
        public int SupplyPoint {
            get => _supplyPoint;
        }

        /// <summary>
        ///     �ش� ����ǰ�� �ٽ� ä������ ���� ������
        /// </summary>
        public int SpawnDelayTime {
            get => _spawnDelayTime;
        }

        /// <summary>
        ///     ����ǰ�� �ٽ� ä������ ���� ���� �ð�
        /// </summary>
        public float RemainingSpawnDelay {
            get => _remainingSpawnDelay;
            set => _remainingSpawnDelay = Math.Max(0, value);
        }

        /// <summary>
        ///     ���� ���������� ����
        /// </summary>
        public bool IsInSpawnDelay => RemainingSpawnDelay != 0;

        /// <summary>
        ///     �ش� ����ǰ�� ��ȣ�ۿ� ��
        /// </summary>
        public void Interact() {
            if (RemainingSpawnDelay != 0) {
                return;
            }

            // RemainingSpawnDelay = SpawnDelayTime;
            Game.Instance.CollectSupplies(SupplyPoint);
            gameObject.SetActive(false);

            // StartCoroutine(SpawnDelay());
        }

        /// <summary>
        ///     ���� ������ Coroutine
        /// </summary>
        IEnumerator SpawnDelay() {
            SpawnDelayBegin.Invoke();

            while (RemainingSpawnDelay != 0) {
                if (!Game.Instance.Running) {
                    yield break;
                }
                
                RemainingSpawnDelay -= 0.1f;
                SpawnDelayChanged.Invoke(RemainingSpawnDelay);

                yield return new WaitForSeconds(0.1f);
            }

            SpawnDelayEnd.Invoke();
            yield break;
        }
    }
}
