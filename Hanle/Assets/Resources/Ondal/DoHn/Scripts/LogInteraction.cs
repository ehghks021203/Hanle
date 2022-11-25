using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class LogInteraction : MonoBehaviour {
    const float SPEED = 6f;
    bool isInteraction = false;

    Rigidbody rb;
    bool isReady = true;
    bool isDrag = false;
    Vector2 startPos;
    
    Vector3 resize = Vector3.one;

    EP3Manager ep3Manager;

    void Start() {
        resize = Vector3.zero;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        ep3Manager = GameObject.FindObjectOfType<EP3Manager>().GetComponent<EP3Manager>();
    }

    void Update()
    {
        this.transform.localScale = Vector3.Lerp(this.transform.localScale, resize, Time.deltaTime * SPEED);
        if (!isReady)
        {
            return;
        }

        if (ep3Manager.gameProgress == 4)   CreObj();

#region ThrowBall_inEditor
        if (Application.platform == RuntimePlatform.WindowsEditor) {
            if (Input.GetMouseButtonDown(0) && !isInteraction) {
                RaycastHit hit;
                Ray touchRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                Physics.Raycast(touchRay, out hit, 20f);

                if (hit.collider != null) {
                    if (hit.collider.gameObject == transform.gameObject) {
                        startPos = Input.mousePosition;
                        isDrag = true;
                    }
                }
                
            }
            if (Input.GetMouseButtonUp(0) && isDrag) {
                float dragDistance = Input.mousePosition.y - startPos.y;
                print(dragDistance);

                if (dragDistance < 2000f) {
                    ShowToast("좀 더 쎄게 던져주세요!");
                    return;
                }
                Vector3 throwAngle = (Camera.main.transform.forward + Camera.main.transform.up).normalized;

                rb.isKinematic = false;
                isReady = false;

                rb.AddForce(throwAngle * dragDistance * 0.005f, ForceMode.VelocityChange);
                Invoke("DelObj", 5f);
            }
            return;
        }
#endregion
        
#region ThrowBall_inMobile
        if (Input.touchCount > 0 && isReady && !isInteraction) {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) {
                RaycastHit hit;
                Ray touchRay = Camera.main.ScreenPointToRay(touch.position);
                Physics.Raycast(touchRay, out hit, 20f);

                if (hit.collider != null) {
                    if (hit.collider.gameObject == transform.gameObject) {
                        startPos = touch.position;
                        isDrag = true;
                    }
                }
            }

            else if (touch.phase == TouchPhase.Ended) {
                if (isDrag) {
                    float dragDistance = touch.position.y - startPos.y;

                    Vector3 throwAngle = (Camera.main.transform.forward + Camera.main.transform.up).normalized;

                    rb.isKinematic = false;
                    isReady = false;

                    rb.AddForce(throwAngle * dragDistance * 0.005f, ForceMode.VelocityChange);
                    Invoke("DelObj", 5f);
                }
            }
        }
#endregion
    }

    void CreObj() {
        resize = Vector3.one;
    }

    void DelObj() {
        isInteraction = true;
        Destroy(transform.gameObject);
        ep3Manager.gameProgress++;
    }

#region Toast
    private void ShowToast(string message) {
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
    }
#endregion
}
