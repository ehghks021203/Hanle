using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoZoneInteraction : MonoBehaviour
{
    const float SPEED = 6f;
    [SerializeField] Transform PhotoObj;
    Vector3 textResize = Vector3.one;
    Vector3 resize = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        PhotoObj.localScale = Vector3.Lerp(PhotoObj.localScale, resize, Time.deltaTime * SPEED);
        this.transform.localScale = Vector3.Lerp(this.transform.localScale, textResize, Time.deltaTime * SPEED);
#region Interaction_inEditor
        if (Application.platform == RuntimePlatform.WindowsEditor) {
            if (Input.GetMouseButtonDown(0)) {
                RaycastHit hit;
                Ray touchRay = Camera.main.ScreenPointToRay(Input.mousePosition);

                Physics.Raycast(touchRay, out hit, 20f);

                if (hit.collider != null) {
                    if (hit.collider.gameObject == this.gameObject) {
                        this.GetComponent<BoxCollider>().enabled = false;
                        CreObj();
                    }
                }
            }
        }
#endregion

#region Interaction_inMobile
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            RaycastHit hit;
            Ray touchRay = Camera.main.ScreenPointToRay(touch.position);

            Physics.Raycast(touchRay, out hit, 20f);

            if (hit.collider != null) {
                if (hit.collider.gameObject == this.gameObject) {
                    this.GetComponent<BoxCollider>().enabled = false;
                    CreObj();
                }
            }
        }
#endregion
    }

    void CreObj() {
        resize = Vector3.one;
        textResize = Vector3.zero;
    }
}
