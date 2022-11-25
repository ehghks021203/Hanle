using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EP5
{
    public class CameraRay : MonoBehaviour
    {

        // Update is called once per frame
        void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                ///Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward * 3000, Color.red, .5f);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit))
                {
                    hit.transform.GetComponent<SpriteClick>().Catched(Input.mousePosition);
                    ToastShow(hit.transform.name);
                }
               // Debug.Log(Camera.main.name);
               //d Debug.Log(hit.transform.gameObject.name);
            }
        }

        void ToastShow(string message)
        {
            /*if (Application.platform == RuntimePlatform.Android)
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
    }
}

