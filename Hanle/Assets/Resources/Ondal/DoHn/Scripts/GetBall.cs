using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBall : MonoBehaviour
{
    OndalManager ondalManager;
    private void Start() {
        ondalManager = FindObjectOfType<OndalManager>().GetComponent<OndalManager>();
    }

    private void Update() {
        if (ondalManager.isBattle == true)  Destroy(this.gameObject);
#region BallTouch_inEditor
        if (Application.platform == RuntimePlatform.WindowsEditor) {
            if (Input.GetMouseButtonDown(0)) {
                RaycastHit hit, hit_check;
                Ray touchRay = Camera.main.ScreenPointToRay(Input.mousePosition);

                Physics.Raycast(touchRay, out hit, 10f);
                Physics.Raycast(touchRay, out hit_check);
                if (hit_check.collider != null) {
                    if (hit_check.collider.gameObject == this.gameObject) {
                        if (hit.collider != null) {
                            Destroy(this.gameObject);
                            ondalManager.ChangeBallCount(1);
                        }
                        else
                            ondalManager.ShowGetBallWarning();
                    }
                }
            }
        }
#endregion

#region BallTouch_inMobile
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            RaycastHit hit, hit_check;
            Ray touchRay = Camera.main.ScreenPointToRay(touch.position);

            Physics.Raycast(touchRay, out hit, 20f);
            Physics.Raycast(touchRay, out hit_check);

            if (hit_check.collider != null) {
                if (hit_check.collider.gameObject == this.gameObject) {
                    if (hit.collider != null) {
                        Destroy(this.gameObject);
                        ondalManager.ChangeBallCount(1);
                    }
                    else
                        ondalManager.ShowGetBallWarning();
                }
            }
        }
#endregion
    }
}
