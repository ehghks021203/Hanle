using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace EP5
{
    public class SliceManager : MonoBehaviour
    {

        public GameObject[] parts;
        public CanvasGroup canvasGroup;

        private bool isOffed = true;

        public CanvasGroup clear;

        public Text scoreText;
        private int score = 0;

        public GameObject clearUI;

        public void ClearPart(int type)
        {
            score++;
            scoreText.text = "<size=20>남은 갯수</size>\r\n" + score + " / 4";
            parts[type].SetActive(false);
            for(int i = 0; i < 4; i++)
            {
                if(parts[i].activeSelf)
                {
                    return;
                }
            }

            clearUI.SetActive(true);
            /*if(SceneManager.GetActiveScene().name == "EP5")
            {
                SceneManager.LoadScene("EP5_2");
            } else
            {
                //클리어임
                GameObject.Find("Ending").GetComponent<Canvas>().enabled = true;
            }*/
        }

        public void ClearUIAccept()
        {
            if (SceneManager.GetActiveScene().name == "EP5")
            {
                SceneManager.LoadScene("EP5_2");
            }
            else
            {
                //클리어임
                GameObject.Find("Ending").GetComponent<Canvas>().enabled = true;
            }
        }

        public void ToggleViewer()
        {
            isOffed ^= true;
            canvasGroup.DOKill();
            if (isOffed)
            {
                canvasGroup.interactable = false;
                canvasGroup.DOFade(0, 0.5f).OnComplete(delegate
                {
                    canvasGroup.gameObject.SetActive(false);
                });
                return;
            }
            canvasGroup.interactable = true;
            canvasGroup.gameObject.SetActive(true);
            canvasGroup.DOFade(1, 0.5f);
        }
    }
}

