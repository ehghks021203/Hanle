using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public List<QuestionAndAnswer> QnA;
    public GameObject[] options;

    public Text QuestionText;

    public GameObject UI;
    public GameObject NextButton;
    public GameObject NextCanvas;

    public bool isCorrect = false;

    private void Start()
    {
        makeQuestion(true);
    }

    public void makeQuestion(bool b = false)
    {
        if(!b) QnA.RemoveAt(0);

        if (QnA.Count > 0)
        {
            QuestionText.text = QnA[0].Question;
            SetAnswer();
        }
        else
        {
            Debug.Log("¿Ï·á");
            NextCanvas.SetActive(true);
            transform.parent.gameObject.SetActive(false);
            GameOver();
        }
    }

    void SetAnswer()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<Image>().color = options[i].GetComponent<AnswerScript>().startColor;
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[0].Answer[i];

            if (QnA[0].CorrectAnswer == i + 1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    public void Click(int t)
    {
        for (int i = 0; i < options.Length; i++)
        {
            AnswerScript v = options[i].GetComponent<AnswerScript>();
            v.isClicked = false;
            v.Click();
            if(i == t)
            {
                v.Click(true);
                v.isClicked = true;
                continue;
            }
        }
    }

    public void AnswerCheck()
    {
        for (int i = 0; i < options.Length; i++)
        {
            if (!options[i].GetComponent<AnswerScript>().isCorrect && options[i].GetComponent<AnswerScript>().isClicked)
            {
                options[i].GetComponent<Image>().color = Color.red;
                wrong();
                return;
            }
            if (options[i].GetComponent<AnswerScript>().isCorrect && options[i].GetComponent<AnswerScript>().isClicked)
            {
                options[i].GetComponent<Image>().color = Color.green;
                correct();
                return;
            }
        }
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<Image>().color = Color.red;
        }
        wrong();
    }

    public void correct()
    {
        StartCoroutine(waitForNext());
    }

    public void wrong()
    {
        StartCoroutine(waitForNext());
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
    }
}
