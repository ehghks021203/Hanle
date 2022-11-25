using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionData : MonoBehaviour
{
    public int id;

    private void Start() {
        DataManager.Instance.Load();
    }

    private void Update() {
        if (DataManager.Instance.data.isUnlock[id]) {
            this.GetComponent<Button>().interactable = true;
        }
    }
}
