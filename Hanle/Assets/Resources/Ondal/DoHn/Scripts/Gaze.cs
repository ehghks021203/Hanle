using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Gaze : MonoBehaviour {
    const float MAX_DISTANCE = 20f;
    List<TextBoxBehavior> tbs = new List<TextBoxBehavior>();


    void Start() {
        tbs = FindObjectsOfType<TextBoxBehavior>().ToList();
    }

    void Update() {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, MAX_DISTANCE)) {
            GameObject obj = hit.collider.gameObject;
            if (obj.CompareTag("includeTextBox")) {
                OpenTextBox(obj.GetComponent<TextBoxBehavior>());
            }
        } else {
            CloseAll();
        }
    }

    void OpenTextBox(TextBoxBehavior textBox) {
        foreach (TextBoxBehavior tb in tbs) {
            if (tb == textBox) {
                tb.OpenTextBox();
            } else {
                tb.CloseTextBox();
            }
        }
    }

    void CloseAll() {
        foreach (TextBoxBehavior tb in tbs) {
            tb.CloseTextBox();
        }
    }
}
