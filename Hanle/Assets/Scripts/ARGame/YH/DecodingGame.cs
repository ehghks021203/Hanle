using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DecodingGame : MonoBehaviour
{
    [SerializeField]
    private string[] Question_str;
    [SerializeField]
    private string[] Answer_str;
    [SerializeField]
    private ScoreManager scoreManager;

    public Text ScoreText;
    public Text QuestionText;
    public InputField Answer;
    public Button AnswerCheck;

    public int currentQuestionID = 0;
    public bool isAnswer;
    

    public GameObject NextButton;
    public GameObject NextCanvas;

    bool isCheck = false;

    public Image image;

    private void Start()
    {
        //makeQuestion(true);
        QuestionText.text = Question_str[0];
    }

    //private void Update()
    //{
    //    if (currentQuestionID == Question_str.Length && !isCheck)
    //    {
    //        isCheck = true;
    //        Debug.Log("완료");
    //    }
    //}

    //public void makeQuestion(bool isFirst = false)
    //{
    //    if (!isFirst) currentQuestionID++;

    //    if (currentQuestionID == Question_str.Length)
    //    {
    //        Debug.Log("완료");
    //        //NextCanvas.SetActive(true);
    //        gameObject.SetActive(false);
    //        return;
    //    }
    //    QuestionText.text = Question_str[currentQuestionID];
    //}


    public void AnswerCheckClick()
    {
        if (Answer.text == Answer_str[0])
        {
            AnswerCheck.interactable = false;
            
            scoreManager.score++;
            isAnswer = true;
            scoreManager.ScoreCheck();
            Debug.Log("정답");
            image.color = Color.green;
            
            StartCoroutine(waitForNext());
        }
        else
        {
            Debug.Log("오답");
            image.color = Color.red;
            StartCoroutine(waitForNext());
        }
    }

    IEnumerator waitForNext()
    {
        yield return new WaitForSeconds(1);
        NextButton.SetActive(true);
        image.color = Color.white;
    }
}
