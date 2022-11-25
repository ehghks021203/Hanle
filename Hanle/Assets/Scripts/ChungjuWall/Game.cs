using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace ChungjuWall {
    /// <summary>
    ///     게임에 대한 전반적인 정보를 가지고 있는 클래스
    /// </summary>
    public class Game : MonoBehaviour {
        public static Game Instance { get; private set; }

        /// <summary>
        ///     벽의 HP가 변화될 때 호출되는 이벤트.
        ///     남은 성벽 채력을 전달한다
        /// </summary>
        public UnityEvent<int> WallHpChanged = new UnityEvent<int>();

        /// <summary>
        ///     보급품의 양이 줄어들 때 호출되는 이벤트.
        ///     남은 보급품을 전달한다.
        /// </summary>
        public UnityEvent<int> SuppliesChanged = new UnityEvent<int>();

        /// <summary>
        ///     남은 시간이 변경되었을 때 호출되는 이벤트.
        ///     남은 시간을 전달한다.
        /// </summary>
        public UnityEvent<int> RemainingTimeChanged = new UnityEvent<int>();

        /// <summary>
        ///     수집한 보급품이 변경될 때 호출되는 이벤트
        /// </summary>
        public UnityEvent<int> CollectedSuppliesChanged = new UnityEvent<int>();

        /// <summary>
        ///     성벽의 최대 채력
        /// </summary>
        [Header("Initial Data")]
        [SerializeField]
        private int _maxWallHp = 100;

        /// <summary>
        ///     최대 물자
        /// </summary>
        [SerializeField] 
        private int _maxSupplies = 200;

        /// <summary>
        ///     시작 시간
        /// </summary>
        [SerializeField]
        private int _initTime = 60;

        /// <summary>
        ///     성벽 채력 차감 량
        /// </summary>
        [SerializeField] 
        private int _wallHpDeduction = 10;

        /// <summary>
        ///     물자 게이지 차감 량
        /// </summary>
        [SerializeField] 
        private int _suppliesDeduction = 5;

        /// <summary>
        ///     게임 클리어 시 나올 UI
        /// </summary>
        [SerializeField] 
        private GameObject _gameClearObject;

        /// <summary>
        ///     게임 오버 시 나올 UI
        /// </summary>
        [SerializeField] 
        private GameObject _gameOverObject;

        /// <summary>
        ///     현재 성벅의 채력
        /// </summary>
        [Header("Data")] 
        [SerializeField] 
        private int _wallHp = 100;

        /// <summary>
        ///     현재 성벅의 채력
        /// </summary>
        [SerializeField]
        private int _remainingSupplies = 100;

        /// <summary>
        ///     남은 시간. 초단위로 표시
        /// </summary>
        [SerializeField]
        private int _remainingTime = 60;

        /// <summary>
        ///     현재 수집한 보급품 량
        /// </summary>
        [SerializeField] 
        private int _collectedSupplies = 0;

        /// <summary>
        ///     현재 보여지는 보급품
        /// </summary>
        [SerializeField] 
        private int _displaySupplies = 100;

        /// <summary>
        ///     게임이 현재 실행 중인지 여부
        /// </summary>
        public bool Running { get; private set; } = true;

        /// <summary>
        ///     현재 성벅의 채력
        /// </summary>
        public int WallHp {
            get => _wallHp;
            set {
                _wallHp = Clamp(value, 0, _maxWallHp);
                WallHpChanged.Invoke(_wallHp);
            }
        }

        /// <summary>
        ///     현재 성벅의 채력
        /// </summary>
        public int RemainingSupplies {
            get => _remainingSupplies;
            set => _remainingSupplies = Clamp(value, 0, _maxSupplies);
        }

        /// <summary>
        ///     남은 시간. 초단위로 표시
        /// </summary>
        public int RemainingTime {
            get => _remainingTime;
            set {
                _remainingTime = Math.Max(0, value);
                RemainingTimeChanged.Invoke(_remainingTime);
            }
        }
        /// <summary>
        ///     현재 보여지는 보급품
        /// </summary>
        public int DisplaySupplies {
            get => _displaySupplies;
            set {
                _displaySupplies = Clamp(value, 0, _maxSupplies);
                SuppliesChanged.Invoke(_displaySupplies);
            }
        }
        /// <summary>
        ///     현재 수집한 보급품 량
        /// </summary>
        public int CollectedSupplies {
            get => _collectedSupplies;
            set {
                _collectedSupplies = value;
                CollectedSuppliesChanged.Invoke(_collectedSupplies);
            }
        }

        private void Awake() {
            if (Instance != null) {
                Debug.LogError("Game instance duplicated");
            } else {
                Instance = this;
            }
        }

        private void Start() {
            WallHp = _maxWallHp;
            RemainingSupplies = _maxSupplies;
            RemainingTime = _initTime;

            _displaySupplies = _remainingSupplies;
            
            WallHpChanged.Invoke(WallHp);
            SuppliesChanged.Invoke(RemainingSupplies);
            RemainingTimeChanged.Invoke(RemainingTime);

            StartCoroutine(UpdateGameValuesPerSecond());
            StartCoroutine(UpdateDisplaySupplies());
        }

        /// <summary>
        ///     보급품을 수집한다
        /// </summary>
        /// <param name="amount"></param>
        public void CollectSupplies(int amount) {
            CollectedSupplies += amount;
        }

        /// <summary>
        ///     수집한 보급품을 적용한다
        /// </summary>
        public void ApplySupplies() {
            RemainingSupplies += CollectedSupplies;
            DisplaySupplies += CollectedSupplies;

            RemainingSupplies -= 5;

            CollectedSupplies = 0;
        }

        /// <summary>
        ///     1초마다 각 값들을 업데이트 시켜준다.
        /// </summary>
        private IEnumerator UpdateGameValuesPerSecond() {
            while (true) {
                yield return new WaitForSeconds(1);
                RemainingTime -= 1;

                // 게임 시간이 0이 되면 게임 클리어
                if (RemainingTime <= 0) {
                    GameClear();
                    yield break;
                }

                if (WallHp <= 0) {
                    GameOver();
                    yield break;
                }

                if (RemainingSupplies <= 0) {
                    WallHp -= _wallHpDeduction;
                }

                RemainingSupplies -= _suppliesDeduction;
            }
        }

        /// <summary>
        ///     0.2초마다 1씩 물자 값을 차감시켜준다.
        /// </summary>
        /// <returns></returns>
        private IEnumerator UpdateDisplaySupplies() {
            while (true) {
                yield return new WaitForSeconds(0.2f);

                if (DisplaySupplies != RemainingSupplies) {
                    if (RemainingSupplies == 0) {
                        DisplaySupplies = 0;
                    } else {
                        DisplaySupplies -= 1;
                    }
                }
            }
        }

        /// <summary>
        ///     게임 오버 시 호출되는 함수
        /// </summary>
        private void GameOver() {
            _gameOverObject.SetActive(true);
            Running = false;
        }

        /// <summary>
        ///     게임 클리어 시 호출되는 함수
        /// </summary>
        private void GameClear() {
            _gameClearObject.SetActive(true);
            Running = false;
        }

        /// <summary>
        ///     주어진 값을 min max 안에 고정 시킨다
        /// </summary>
        /// <param name="value">고정 시킬 값</param>
        /// <param name="min">최저 값</param>
        /// <param name="max">최대 값</param>
        /// <returns>보정 된 값</returns>
        private static T Clamp<T>(T value, T min, T max) where T : IComparable<T> {
            return value.CompareTo(min) < 0 ? min : value.CompareTo(max) > 0 ? max : value;
        }
    }
}