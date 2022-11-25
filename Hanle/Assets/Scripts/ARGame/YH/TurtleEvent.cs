using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleEvent : MonoBehaviour
{
    public GameObject ending;

    private void Start()
    {
        ending = GameObject.Find("Ending");

        GameObject.Find("ScoreManager").GetComponent<ScoreManager>().Turtle = gameObject;
        gameObject.SetActive(false);
    }
    private void OnMouseDown()
    {
        Debug.Log("click");

        ending.GetComponent<Canvas>().enabled = true;
    }
}