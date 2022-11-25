using KKU.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

namespace KKU
{
    public sealed class GPSManager : MonoBehaviour
    {
        private List<GPSEventListener> listeners = new List<GPSEventListener>();

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

        private double[] locationConst = new double[18] {
            36.82274948013407d, 128.09479828595838d, //�����~
            36.82246086834928d, 128.09482779794746d,
            36.82237249206833d, 128.09512915166619d,
            36.82267441163932d, 128.0951222463276d,
            0d, 0d,
            0d, 0d,
            0d, 0d,
            0d, 0d,
            0d, 0d, //�ϴ��� ����
        };
        private const double METER = 8;
        public GPSMoveEvent.Place lastPlace = GPSMoveEvent.Place.None;

        // Start is called before the first frame update

        private static GPSManager instance;

        public static GPSManager GetInstance()
        {
            if(instance == null)
            {
                instance = new GameObject().AddComponent<GPSManager>();
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
            RequestPermission();
            StartCoroutine(GPSCheck());
            NativeGPSPlugin.StartLocation();
        }

        #region Permission Callback
        public void RequestPermission()
        {
            if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                var callbacks = new PermissionCallbacks();
                callbacks.PermissionDenied += PermissionDenied;
                callbacks.PermissionGranted += PermissionGranted;
                callbacks.PermissionDeniedAndDontAskAgain += PermissionDontAskAgain;

                Permission.RequestUserPermission(Permission.FineLocation);
            }
        }

        public void PermissionDontAskAgain(string permission)
        {
            foreach (GPSEventListener listener in listeners) listener.GPSEvent(new PermissionCallbackEvent() { isGranted = false, neverAskAgain = true });
        }

        public void PermissionGranted(string name)
        {
            foreach (GPSEventListener listener in listeners) listener.GPSEvent(new PermissionCallbackEvent() { isGranted = true });
            //permissionGranted = true;
            foreach (GPSEventListener listener in listeners) listener.GPSEvent(new GPSStartEvent() { isStarted = true });
        }

        public void PermissionDenied(string name)
        {
            foreach (GPSEventListener listener in listeners) listener.GPSEvent(new PermissionCallbackEvent() { isGranted = false, neverAskAgain = false });
        }
        #endregion

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
            while(true)
            {
                yield return new WaitForSecondsRealtime(5f);
                if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
                {
                    continue;
                }

                double lat = NativeGPSPlugin.GetLatitude();
                double lng = NativeGPSPlugin.GetLongitude();
                if(lat == 0)
                {
                    ShowToast("GPS ��ȣ�� ��Ȱ���� �ʽ��ϴ�.");
                    continue;
                }

                if (lastPlace == GPSMoveEvent.Place.None)
                {
                    for (int i = 0; i < 8; i += 2) //TODO 18�� �÷�����
                    {
                        double d = GetDistance(lat, lng, locationConst[i], locationConst[i + 1]);
                        if (d < METER)
                        {
                            lastPlace = (GPSMoveEvent.Place)(i / 2 + 1);
                            foreach (GPSEventListener listener in listeners) listener.GPSEvent(new GPSMoveEvent() { place = (GPSMoveEvent.Place)(i / 2 + 1), isArrive = true });
                            break;
                        }
                    }
                    continue;
                }
                if (GetDistance(lat, lng, locationConst[(int)(lastPlace - 1) * 2], locationConst[(int)(lastPlace - 1) * 2 + 1]) > METER)
                {
                    ShowToast(GetDistance(lat, lng, locationConst[(int)(lastPlace - 1) * 2], locationConst[(int)(lastPlace - 1) * 2 + 1]) + " LEAVE");
                    foreach (GPSEventListener listener in listeners) listener.GPSEvent(new GPSMoveEvent() { place = lastPlace, isArrive = true });
                    lastPlace = GPSMoveEvent.Place.None;
                }
            }
        }

        public void RegisterListener(GPSEventListener eventListener)
        {
            listeners.Add(eventListener);
            if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                ShowToast("GPS ������ ���ε��� �ʾҽ��ϴ�.");
            }
        }

        public void UnRegisterListener(GPSEventListener eventListener)
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
            dist = Math.Acos(dist);
            dist = rad2deg(dist);

            dist = dist * 60 * 1.1515;
            dist = dist * 1.609344;    // ���� mile ���� km ��ȯ.  
            dist = dist * 1000.0;      // ����  km ���� m �� ��ȯ  

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

}
