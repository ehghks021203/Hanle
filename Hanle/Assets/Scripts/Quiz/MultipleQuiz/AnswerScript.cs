using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManager quizManager;

    public Color startColor;

    public bool isClicked;
    public void Click(bool b = false)
    {
        //GetComponent<Image>().color = Color.gray;
        //clicked;
        GetComponent<Image>().color = b ? Color.gray : startColor;
    }
}
