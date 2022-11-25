using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

namespace ChungjuWall {
    public class SupplySpawner : MonoBehaviour {
        /// <summary>
        ///     위경도를 가지고 있는 클래스
        /// </summary>
        [Serializable]
        private class Position {
            /// <summary>
            ///     경도
            /// </summary>
            [SerializeField]
            private double _longtitude;

            /// <summary>
            ///     위도
            /// </summary>
            [SerializeField]
            private double _latitude;

            /// <summary>
            ///     경도
            /// </summary>
            public double Longtitude {
                get => _longtitude;
                set => _longtitude = value;
            }

            /// <summary>
            ///     위도
            /// </summary>
            public double Latitude {
                get => _latitude;
                set => _latitude = value;
            }
        }

        /// <summary>
        ///     오브젝트를 스폰시킬 정보
        /// </summary>
        [Serializable]
        private class SpawnData {
            /// <summary>
            ///     오브젝트의 스폰 위치
            /// </summary>
            [SerializeField]
            private Position _position;
            
            /// <summary>
            ///     스폰될 오브젝트
            /// </summary>
            [SerializeField]
            private GpuLocation _spawnTarget;
            
            /// <summary>
            ///     오브젝트의 스폰 위치
            /// </summary>
            public Position Position => _position;
            
            /// <summary>
            ///     스폰될 오브젝트
            /// </summary>
            public GpuLocation SpawnTarget => _spawnTarget;

            public SpawnData(Position position, GpuLocation spawnTarget) {
                _position = position;
                _spawnTarget = spawnTarget;
            }


            /// <summary>
            ///     주어진 좌표하 스폰 좌표로부터 <see cref="limitMeter"/> 안에 존재하는지 여부를 반환한다.
            /// </summary>
            /// <param name="position">계산할 위치</param>
            /// <param name="limitMeter">범위</param>
            /// <returns>범위 안에 존재하는지 여부</returns>
            public bool IsNear(Position position, int limitMeter = 7) {
                return GetDistance(Position, position) < limitMeter;
            }

            /// <summary>
            ///     아이템을 스폰 후 반환한다
            /// </summary>
            /// <returns>스폰된 아이템</returns>
            public GameObject Spawn() {
                return Instantiate(SpawnTarget.gameObject);
            }

            public double GetDistance(Position pos) {
                return GetDistance(Position, pos);
            }
            
            #region Distance Calculate

            public double GetDistance(Position pos1, Position pos2)
            {
                double theta, dist;
                theta = pos1.Longtitude - pos2.Longtitude;

                dist = Math.Sin(deg2rad(pos1.Latitude)) * Math.Sin(deg2rad(pos2.Latitude)) 
                       + Math.Cos(deg2rad(pos1.Latitude)) * Math.Cos(deg2rad(pos2.Latitude)) * Math.Cos(deg2rad(theta));
                dist = Math.Acos(dist);
                dist = rad2deg(dist);

                dist = dist * 60 * 1.1515;
                dist = dist * 1.609344;    // 단위 mile 에서 km 변환.  
                dist = dist * 1000.0;      // 단위  km 에서 m 로 변환  

                return dist;
            }
            /// 
            private double deg2rad(double deg)
            {
                return (double)(deg * Math.PI / (double)180d);
            }
            /// 
            private double rad2deg(double rad)
            {
                return (double)(rad * (double)180d / Math.PI);
            }

            #endregion
        }

        /// <summary>
        ///     미리 스폰하여 캐싱한 오브젝트 리스트
        /// </summary>
        private List<SupplyItem> _spawnedObjects;

        /// <summary>
        ///     아이템을 스폰 정보들
        /// </summary>
        [SerializeField]
        private SpawnData[] _spawnDatas = new SpawnData[]
        {
            new SpawnData(new Position { Longtitude = 128.09481280d, Latitude = 36.82196450d }, null),
            new SpawnData(new Position { Longtitude = 128.09481280d, Latitude = 36.82196450d }, null),
        };

        /// <summary>
        ///     보급 창고 관련 정보
        /// </summary>
        [SerializeField] 
        private SpawnData _supplyDepot;

        [SerializeField] 
        private GameObject rootForSupplies;
        
        [SerializeField]
        private int lastIndex = -1;

        private void Start() {
            _spawnedObjects = new List<SupplyItem>();
            foreach (var spawnData in _spawnDatas) {
                var go = spawnData.Spawn();
                var item = go.GetComponent<SupplyItem>();
                
                go.transform.parent = rootForSupplies.transform;
                
                _spawnedObjects.Add(item);
                item.Exit();
            }
            
            _supplyDepot.SpawnTarget.Exit();

            Game.Instance.RemainingTimeChanged.AddListener(OnRemainingChanged);
            Game.Instance.WallHpChanged.AddListener(OnRemainingChanged);

            RequestPermission();
            StartCoroutine(GPSCheck());
            NativeGPSPlugin.StartLocation();
        }

        void OnRemainingChanged(int value) {
            if (value == 0) {
                for (int i = 0; i < _spawnDatas.Length; i++) {
                    _spawnedObjects[i].Exit();
                }

                _supplyDepot.SpawnTarget.Exit();
            }
        }

        IEnumerator GPSCheck() {
            if (!Input.location.isEnabledByUser) {
                Debug.Log("Location service disabled");
                yield break;
            }

            
            while (true) {
                if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation)) {
                    Debug.Log("Permission is not allowed");
                    yield break;
                }

                if (Game.Instance.RemainingTime != 0) {
                    yield break;
                }

                Position pos = new Position
                {
                    Latitude = NativeGPSPlugin.GetLatitude(),
                    Longtitude = NativeGPSPlugin.GetLongitude()
                };

                if (pos.Latitude == 0) {
                    // TODO: GPU 신호가 원활하지 않다는 메시지 띄우기
                    yield return new WaitForSeconds(1f);
                    continue;
                }

                for (int i = 0; i < _spawnDatas.Length; i++) {
                    if (_spawnedObjects[i].IsInArea) {
                        if (!_spawnDatas[i].IsNear(pos)) {
                            _spawnedObjects[i].Exit();
                            _spawnedObjects[i].gameObject.SetActive(true);
                        }
                    } else {
                        if (_spawnDatas[i].IsNear(pos)) {
                            _spawnedObjects[i].Enter();
                        }
                    }
                }

                if (_supplyDepot.SpawnTarget.IsInArea) {
                    if (!_supplyDepot.IsNear(pos)) {
                        _supplyDepot.SpawnTarget.Exit();
                        _supplyDepot.SpawnTarget.gameObject.SetActive(true);
                    }
                } else {
                    if (_supplyDepot.IsNear(pos)) {
                        _supplyDepot.SpawnTarget.Enter();
                    }
                }
                
                yield return new WaitForSeconds(1f);
            }
        }
        
        /// <summary>
        ///     권한을 요청한다
        /// </summary>
        private void RequestPermission() {
            if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation)) {
                Permission.RequestUserPermission(Permission.FineLocation);
            }
        }
    }
}