using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPicture : MonoBehaviour
{
    public GameObject canvas;

    private void Start()
    {
        canvas = GameObject.Find("Canvas");
    }

    private void OnMouseDown()
    {
        Debug.Log("click");
        canvas.GetComponent<Canvas>().enabled = true;
    }
}
