using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPanelManager : MonoBehaviour {
    RectTransform rectTransform;

    bool isBeingHeld = false;
    //Vector2 loadedPos;
    float startPosY;
    float currentPosY;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        //loadedPos = this.rectTransform.position;
    }

    private void Update() {
        if (isBeingHeld) {
            Vector2 mousePos;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPosY = mousePos.y - startPosY;
            Debug.Log(currentPosY);
            this.rectTransform.anchoredPosition = new Vector2(this.rectTransform.anchoredPosition.x, currentPosY);
        }
    }

    private void OnMouseDown() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mousePos;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            startPosY = mousePos.y - this.rectTransform.anchoredPosition.y;
            isBeingHeld = true;
        }
    }
    
    private void OnMouseUp() {
        isBeingHeld = false;

        //if (currentPosY >)
    }
}
