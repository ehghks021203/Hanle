using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace ChungjuWall {
    /// <summary>
    ///     보급품 베이스 클래스
    /// </summary>
    public class Supply : GpuLocation {
        public UnityEvent<float> SpawnDelayChanged = new UnityEvent<float>();
        public UnityEvent SpawnDelayBegin = new UnityEvent();
        public UnityEvent SpawnDelayEnd = new UnityEvent();

        /// <summary>
        ///     해당 보급품을 선택하면 차는 물자 량
        /// </summary>
        [Header("Initial Data")]
        [SerializeField]
        private int _supplyPoint;

        /// <summary>
        ///     해당 보급품이 다시 채워지기 까지 딜레이
        /// </summary>
        [SerializeField]
        private int _spawnDelayTime;

        /// <summary>
        ///     보급품이 다시 채워지기 까지 남은 시간
        /// </summary>
        [Header("Data")]
        [SerializeField]
        private float _remainingSpawnDelay = 0;

        /// <summary>
        ///     해당 보급품을 선택하면 차는 물자 량
        /// </summary>
        public int SupplyPoint {
            get => _supplyPoint;
        }

        /// <summary>
        ///     해당 보급품이 다시 채워지기 까지 딜레이
        /// </summary>
        public int SpawnDelayTime {
            get => _spawnDelayTime;
        }

        /// <summary>
        ///     보급품이 다시 채워지기 까지 남은 시간
        /// </summary>
        public float RemainingSpawnDelay {
            get => _remainingSpawnDelay;
            set => _remainingSpawnDelay = Math.Max(0, value);
        }

        /// <summary>
        ///     현재 스폰중인지 여부
        /// </summary>
        public bool IsInSpawnDelay => RemainingSpawnDelay != 0;

        /// <summary>
        ///     해당 보급품과 상호작용 함
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
        ///     스폰 딜레이 Coroutine
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
