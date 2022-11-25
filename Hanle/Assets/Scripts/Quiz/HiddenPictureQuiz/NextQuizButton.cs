using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextQuizButton : MonoBehaviour
{
    public GameObject nextQuiz;

    public void NextQuiz()
    {
        nextQuiz.SetActive(true);
    }
}
