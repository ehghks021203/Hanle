using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBoxInteraction : MonoBehaviour {
    const float SPEED = 6f;
    bool isInteraction = false;
    [SerializeField] int id;
    [SerializeField] GameObject parent;
    [SerializeField] LayerMask textBoxLayer;
    Vector3 resize = Vector3.one;

    // Game Manager
    EP3Manager ep3Manager;

    void Start() {
        resize = Vector3.zero;
        ep3Manager = GameObject.FindObjectOfType<EP3Manager>().GetComponent<EP3Manager>();
    }

    void Update() {
        parent.transform.localScale = Vector3.Lerp(parent.transform.localScale, resize, Time.deltaTime * SPEED);

        if (ep3Manager.gameProgress == id)  CreObj();

#region Interaction_inEditor
        if (Application.platform == RuntimePlatform.WindowsEditor) {
            if (Input.GetMouseButtonDown(0) && !isInteraction) {
                RaycastHit hit;
                Ray touchRay = Camera.main.ScreenPointToRay(Input.mousePosition);

                Physics.Raycast(touchRay, out hit, 50f, textBoxLayer);

                if (hit.collider != null) {
                    if (hit.collider.gameObject == this.gameObject) {
                        Interactive(id);
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

            Physics.Raycast(touchRay, out hit, 50f, textBoxLayer);

            if (hit.collider != null) {
                if (hit.collider.gameObject == this.gameObject) {
                    Interactive(id);
                }
            }
        }
#endregion
    }

    public void Interactive(int id) {
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(true);
        StartCoroutine(DelObj());
        isInteraction = true;
    }

    void CreObj() {
        resize = Vector3.one;
    }

    IEnumerator DelObj() {
        yield return new WaitForSeconds(2.0f);
        ep3Manager.gameProgress++;
        resize = Vector3.zero;
        yield return new WaitForSeconds(1.0f);
        Destroy(parent.gameObject);
    }
}