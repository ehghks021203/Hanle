using KKUCopy;
using KKUCopy.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoZoneLocate : MonoBehaviour, GPSCopyEventListener {
    public Image[] photoImage;
    int id;

    [SerializeField] GameObject[] gameObj;

    public void GPSEvent(BaseCopyEvent baseEvent) {
        if (baseEvent is GPSCopyMoveEvent) {
            var pk = (GPSCopyMoveEvent)baseEvent;
            id = (int)(pk.place - 1);
            if (pk.isArrive) {
                photoImage[id].gameObject.SetActive(true);
                gameObj[id].SetActive(true);
            }
            else {
                photoImage[id].gameObject.SetActive(false);
                gameObj[id].SetActive(false);
            }
        }
    }

    void Start() {
        GPSCopyManager.GetInstance().RegisterListener(this);
        StartCoroutine(ObjSet());
    }

    void OnDestroy() {
        GPSCopyManager.GetInstance().UnRegisterListener(this);
    }

    IEnumerator ObjSet() {
        yield return new WaitForSeconds(2.0f);
        gameObj[0] = GameObject.FindGameObjectWithTag("Photo1");
        gameObj[1] = GameObject.FindGameObjectWithTag("Photo2");
        gameObj[2] = GameObject.FindGameObjectWithTag("Photo3");
        gameObj[3] = GameObject.FindGameObjectWithTag("Photo4");
        gameObj[4] = GameObject.FindGameObjectWithTag("Photo5");
    }
}