using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text ScoreText;
    public int score;
    public GameObject Turtle;
    public GameObject FindTurtle;
    bool isCheck = false;

    private void Update()
    {
        ScoreCheck();
    }

    public void ScoreCheck()
    {
        if (!isCheck)
        {
            ScoreText.text = score + " / 3";
            if (score == 3)
            {
                //Turtle.GetComponent<GameObject>().SetActive(true);
                //Turtle = GameObject.Find("ARLocationRoot").transform.Find("Turtle()")
                Turtle.SetActive(true);
                FindTurtle.SetActive(true);
                isCheck = true;
            }
        }
    }
}
