using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionButton : MonoBehaviour
{
    Canvas optionCanvas;

    void Start() {
        optionCanvas = GameObject.Find("OptionCanvas").GetComponent<Canvas>();
    }

    public void OpenOption() {
        optionCanvas.enabled = true;
    }

    public void CloseOption() {
        optionCanvas.enabled = false;
    }
}
