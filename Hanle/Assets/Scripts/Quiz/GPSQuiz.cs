using KKU;
using KKU.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GPSQuiz : MonoBehaviour, GPSEventListener
{

    public Button[] storyButtons;

    public void GPSEvent(BaseEvent baseEvent)
    {
        if (baseEvent is GPSMoveEvent)
        {
            var pk = (GPSMoveEvent)baseEvent;
            if(SceneManager.GetActiveScene().name == "QuizMenu")
            {
                if (pk.isArrive)
                {
                    ToastShow(pk.place.ToString() + " 도착");
                    storyButtons[(int)(pk.place - 1)].interactable = true;
                }
                else
                {
                    ToastShow(pk.place.ToString() + " 떠남");
                    storyButtons[(int)(pk.place - 1)].interactable = false;
                }
            } else
            {
                if(!pk.isArrive)
                {
                    SceneMove("QuizMenu");
                }
            }
        }
    }

    public void SceneMove(string name)
    {
        ToastShow(name + " 씬 진입하였습니다.");
        SceneManager.LoadScene(name);
    }

    void ToastShow(string message)
    {
        if (Application.platform == RuntimePlatform.Android)
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
        }
        Debug.Log(message);
    }

    // Start is called before the first frame update
    void Start()
    {
        GPSManager.GetInstance().RegisterListener(this);
        if(GPSManager.GetInstance().lastPlace != GPSMoveEvent.Place.None)
        {
            storyButtons[(int)(GPSManager.GetInstance().lastPlace - 1)].interactable = true;
        }
    }

    void OnDestroy()
    {
        GPSManager.GetInstance().UnRegisterListener(this);
    }
}
