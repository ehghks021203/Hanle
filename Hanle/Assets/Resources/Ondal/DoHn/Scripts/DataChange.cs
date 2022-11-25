using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataChange : MonoBehaviour {
    public int[] list;
    void Start() {
        DataManager.Instance.Load();
    }

    public void Change() {
        for (int i = 0; i < list.Length; i++) {
            DataManager.Instance.Unlock(list[i]);
        }
        DataManager.Instance.Save();
    }
}
