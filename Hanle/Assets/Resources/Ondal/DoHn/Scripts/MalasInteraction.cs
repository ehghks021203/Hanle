using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalasInteraction : MonoBehaviour
{
    const float SPEED = 6f;
    bool isInteraction = false;
    Vector3 resize = Vector3.one;
    EP3Manager ep3Manager;

    void Start() {
        resize = Vector3.zero;
        ep3Manager = GameObject.FindObjectOfType<EP3Manager>().GetComponent<EP3Manager>();
    }

    void Update() {
        this.transform.localScale = Vector3.Lerp(this.transform.localScale, resize, Time.deltaTime * SPEED);

        if (ep3Manager.gameProgress == 5)   CreObj();

#region Interaction_inEditor
        if (Application.platform == RuntimePlatform.WindowsEditor) {
            if (Input.GetMouseButtonDown(0) && !isInteraction) {
                RaycastHit hit;
                Ray touchRay = Camera.main.ScreenPointToRay(Input.mousePosition);

                Physics.Raycast(touchRay, out hit, 20f);

                if (hit.collider != null) {
                    if (hit.collider.gameObject == this.gameObject) {
                        ShowToast("염주 / 목탁을 획득했습니다!");
                        StartCoroutine(DelObj());
                        isInteraction = true;
                    }
                }
            }
        }
#endregion

#region Interaction_inMobile
        if (Input.touchCount > 0 && !isInteraction) {
            Touch touch = Input.GetTouch(0);
            RaycastHit hit;
            Ray touchRay = Camera.main.ScreenPointToRay(touch.position);

            Physics.Raycast(touchRay, out hit, 20f);

            if (hit.collider != null) {
                if (hit.collider.gameObject == this.gameObject) {
                    ShowToast("염주 / 목탁을 획득했습니다!");
                    StartCoroutine(DelObj());
                    isInteraction = true;
                }
            }
        }
#endregion
    }
    
    void CreObj() {
        resize = Vector3.one;
    }

    IEnumerator DelObj() {
        yield return new WaitForSeconds(2.0f);
        ep3Manager.gameProgress++;
        resize = Vector3.zero;
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }

#region Toast
    private void ShowToast(string message) {
        /*
        if(Application.platform == RuntimePlatform.Android) {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            if (unityActivity != null) {
                AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
                unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, message, 0);
                    toastObject.Call("show");
                }));
            }
        }
        Debug.Log(message);
        */
    }
#endregion
}
