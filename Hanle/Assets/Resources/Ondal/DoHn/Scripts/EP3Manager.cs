using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EP3Manager : MonoBehaviour {
    public int gameProgress = 0;
    bool isClear = false;
    [SerializeField] GameObject clearUI;

    void Update() {
        if (gameProgress == 7 && !isClear) {
            clearUI.SetActive(true);
            isClear = true;
        }
    }
}
