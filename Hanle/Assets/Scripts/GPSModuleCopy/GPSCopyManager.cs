using KKUCopy.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

namespace KKUCopy
{
    public sealed class GPSCopyManager : MonoBehaviour
    {
        private List<GPSCopyEventListener> listeners = new List<GPSCopyEventListener>();

        /*private bool _p;
        private bool permissionGranted
        {
            get
            {
                return _p;
            }
            set
            {
                if (_p == value) { return; }

                if (value)
                {
                    StartCoroutine(GPSCheck());
                    
                } else
                {
                    StopAllCoroutines();
                    foreach (GPSEventListener listener in listeners) listener.GPSEvent(new GPSStartEvent() { isStarted = false });
                }
                _p = value;
            }
        }*/

        private double[] locationConst = new double[10] {
            36.82206590d, 128.09506300d, //�����~
            36.82186708d, 128.09509980d,
            36.82241850d, 128.09516650d,
            36.82175687d, 128.09509426d,
            36.82287070d, 128.09505310d
        };
        private const double METER = 10;
        private GPSCopyMoveEvent.Place lastPlace = GPSCopyMoveEvent.Place.None;

        // Start is called before the first frame update

        private static GPSCopyManager instance;

        public static GPSCopyManager GetInstance()
        {
            if(instance == null)
            {
                instance = new GameObject().AddComponent<GPSCopyManager>();
            }
            return instance;
        }

        void Awake()
        {
            if(instance != null)
            {
                Debug.LogError("GPSManager�� ���� �� ������ �� �����ϴ�.");
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);

        }

        void Start()
        {
            StartCoroutine(GPSCheck());
            NativeGPSPlugin.StartLocation();
        }


        #region Toast
        private void ShowToast(string message)
        {
            /*if(Application.platform == RuntimePlatform.Android)
            {
                AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                if (unityActivity != null)
                {
                    AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
                    unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                    {
                        AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, message, 0);
                        toastObject.Call("show");
                    }));
                }
            }*/
            Debug.Log(message);
        }
        #endregion

        IEnumerator GPSCheck()
        {
            yield return new WaitForSeconds(3f);
            while(true)
            {
                yield return new WaitForSecondsRealtime(1.0f);
                if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
                {
                    continue;
                }

                double lat = 0d;
                double lng = 0d;
                if(Application.platform == RuntimePlatform.WindowsEditor) {
                    lat = 36.82175687d;
                    lng = 128.09509435d;
                } else {
                    lat = NativeGPSPlugin.GetLatitude();
                    lng = NativeGPSPlugin.GetLongitude();
                }

                if (lastPlace == GPSCopyMoveEvent.Place.None)
                {
                    string total = "";
                    for (int i = 0; i < 10; i += 2) //TODO 18�� �÷�����
                    {
                        double d = GetDistance(lat, lng, locationConst[i], locationConst[i + 1]);
                        total += d + " M \r\n";
                        if (d < METER)
                        {
                            lastPlace = (GPSCopyMoveEvent.Place)(i / 2) + 1;
                            foreach (GPSCopyEventListener listener in listeners) listener.GPSEvent(new GPSCopyMoveEvent() { place = (GPSCopyMoveEvent.Place)(i / 2), isArrive = true });
                            break;
                        }
                    }
                    ShowToast(total);
                    continue;
                }
                if (GetDistance(lat, lng, locationConst[(int)(lastPlace - 1) * 2], locationConst[(int)(lastPlace - 1) * 2 + 1]) > METER)
                {
                    ShowToast(GetDistance(lat, lng, locationConst[(int)(lastPlace - 1) * 2], locationConst[(int)(lastPlace - 1) * 2 + 1]) + " LEAVE");
                    foreach (GPSCopyEventListener listener in listeners) listener.GPSEvent(new GPSCopyMoveEvent() { place = lastPlace, isArrive = true });
                    lastPlace = GPSCopyMoveEvent.Place.None;
                }
            }
        }

        public void RegisterListener(GPSCopyEventListener eventListener)
        {
            listeners.Add(eventListener);
            if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                ShowToast("GPS ������ ���ε��� �ʾҽ��ϴ�.");
            }
        }

        public void UnRegisterListener(GPSCopyEventListener eventListener)
        {
            listeners.Remove(eventListener);
        }

        #region Distance Calculate

        public double GetDistance(double lat1, double lon1, double lat2, double lon2)
        {
            double theta, dist;
            theta = lon1 - lon2;

            dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1))
                 * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            
        

            dist = acos(dist);
                        
            dist = rad2deg(dist);

            dist = dist * 60 * 1.1515;
            dist = dist * 1.609344;    // ���� mile ���� km ��ȯ.  
            dist = dist * 1000.0;      // ����  km ���� m �� ��ȯ  

            return dist;
        }

        double acos(double n) {
            if (n > 1) {
                return Math.Acos(1);
            } else if (n < -1) {
                return Math.Acos(-1);
            } else {
                return Math.Acos(n);
            }
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

}
