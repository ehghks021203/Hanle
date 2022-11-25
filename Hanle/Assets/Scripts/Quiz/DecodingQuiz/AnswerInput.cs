using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class AnswerInput : MonoBehaviour
{
    [SerializeField]
    private string[] Question_str;
    [SerializeField]
    private string[] Answer_str;

    public Text QuestionText;
    public InputField Answer;
    public Button AnswerCheck;
    
    private int currentQuestionID = 0;

    public GameObject UI;
    public GameObject NextButton;
    public GameObject NextCanvas;

    bool isCheck = false;

    public Image image;

    private void Start()
    {
        makeQuestion(true);
        QuestionText.text = Question_str[currentQuestionID];
    }

    private void Update()
    {
        if (currentQuestionID == Question_str.Length && !isCheck)
        {
            isCheck = true;
            Debug.Log("�Ϸ�");
            GameOver();
        }
    }

    public void makeQuestion(bool isFirst = false)
    {
        if(!isFirst) currentQuestionID++;

        if (currentQuestionID == Question_str.Length)
        {
            Debug.Log("�Ϸ�");
            //NextCanvas.SetActive(true);
            gameObject.SetActive(false);
            return;
        }
        QuestionText.text = Question_str[currentQuestionID];
        Answer.text = "";
    }
    

    public void AnswerCheckClick()
    {
        if (Answer.text == Answer_str[currentQuestionID])
        {
            Debug.Log("����");
            image.color = Color.green;
            AnswerCheck.gameObject.SetActive(false);
            StartCoroutine(waitForNext());
        }
        else
        {
            Debug.Log("����");
            image.color = Color.red;
            StartCoroutine(waitForNext());
        }
    }

    public void GameOver()
    {
        NextButton.SetActive(true);
    }

    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator waitForNext()
    {
        yield return new WaitForSeconds(1);
        NextButton.SetActive(true);
        image.color = Color.white;
    }
}
