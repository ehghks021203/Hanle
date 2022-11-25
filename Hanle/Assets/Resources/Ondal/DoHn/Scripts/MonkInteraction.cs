using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkInteraction : MonoBehaviour
{
    const float SPEED = 6f;
    bool isInteraction = false;
    [SerializeField] Transform SectionTextBox;
    Vector3 textBoxScale = Vector3.zero;
    Vector3 resize = Vector3.one;
    EP3Manager ep3Manager;

    void Start() {
        resize = Vector3.zero;
        ep3Manager = GameObject.FindObjectOfType<EP3Manager>().GetComponent<EP3Manager>();
    }

    void Update() {
        SectionTextBox.localScale = Vector3.Lerp(SectionTextBox.localScale, textBoxScale, Time.deltaTime * SPEED);
        this.transform.localScale = Vector3.Lerp(this.transform.localScale, resize, Time.deltaTime * SPEED);

        if (ep3Manager.gameProgress == 6)   CreObj();

#region Interaction_inEditor
        if (Application.platform == RuntimePlatform.WindowsEditor) {
            if (Input.GetMouseButtonDown(0) && !isInteraction) {
                RaycastHit hit;
                Ray touchRay = Camera.main.ScreenPointToRay(Input.mousePosition);

                Physics.Raycast(touchRay, out hit, 20f);

                if (hit.collider != null) {
                    if (hit.collider.gameObject == this.gameObject) {
                        this.GetComponent<BoxCollider>().enabled = false;
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
                    this.GetComponent<BoxCollider>().enabled = false;
                    StartCoroutine(DelObj());
                    isInteraction = true;
                }
            }
        }
#endregion
    }

    public void OpenTextBox() {
        textBoxScale = Vector3.one;
    }

    public void CloseTextBox() {
        textBoxScale = Vector3.zero;
    }

    void CreObj() {
        resize = Vector3.one;
    }

    IEnumerator DelObj() {
        OpenTextBox();
        yield return new WaitForSeconds(2.0f);
        CloseTextBox();
        yield return new WaitForSeconds(1.0f);
        ep3Manager.gameProgress++;
        resize = Vector3.zero;
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }
}
