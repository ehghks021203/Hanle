using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectClickEvent : MonoBehaviour
{
    public GameObject decodingGame;
    public DecodingGame correct;
    public string id;

    private void Start()
    {
        decodingGame = GameObject.Find("DecodingGameCanvas"+id);
        correct = decodingGame.GetComponent<DecodingGame>();
        Debug.Log("DecodingGameCanvas" + id);
    }

    private void Update()
    {
        if (correct.isAnswer == true)
            Destroy(this.gameObject);
    }

    private void OnMouseDown()
    {
        Debug.Log("click");
        decodingGame.GetComponent<Canvas>().enabled = true;
    }
}
