using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoObject : MonoBehaviour {
    void Start() {
        StartCoroutine(Disable());
    }

    IEnumerator Disable() {
        yield return new WaitForSeconds(3.0f);
        this.gameObject.SetActive(false);
    }
}
