using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GlobalMenu : MonoBehaviour
{

    private bool isOpened;

    public Sprite[] menuBtnSprites;

    private Image menuBtn;

    private CanvasGroup menuOptions;

    public Image epMiniMap;
    public Sprite[] ep5maps;

    private GameObject miniMap;
    private void Start()
    {
        if(GameObject.FindGameObjectsWithTag("GlobalUI").Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        if(SceneManager.GetActiveScene().name != "PhotoZone")
        {
            DontDestroyOnLoad(gameObject);
        }
        menuBtn = transform.GetChild(0).GetComponent<Image>();
        menuOptions = transform.GetChild(1).GetComponent<CanvasGroup>();
        miniMap = transform.GetChild(3).gameObject;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void Toggle()
    {
        isOpened ^= true;

        menuBtn.sprite = isOpened ? menuBtnSprites[0] : menuBtnSprites[1];
        menuOptions.interactable = isOpened;
        menuOptions.blocksRaycasts = isOpened;
        menuOptions.DOFade(isOpened ? 1 : 0, .25f);
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainScene")
        {
            Destroy(gameObject);
            return;
        }
        switch(SceneManager.GetActiveScene().name)
        {
            case "ARGame":
                SceneChangeMap(0);
                break;
            case "ARGame2":
                SceneChangeMap(1);
                break;
            case "ARGame3":
                SceneChangeMap(2);
                break;
            case "EP4_Game":
                SceneChangeMap(3);
                break;
            case "ARGame5":
                SceneChangeMap(4);
                break;
        }
        GetComponent<CanvasGroup>().alpha = (scene.name == "GameChoice" || scene.name == "QuizMenu") ? 0 : 1;
        GetComponent<CanvasGroup>().interactable = scene.name != "GameChoice" && scene.name != "QuizMenu";
        GetComponent<CanvasGroup>().blocksRaycasts = scene.name != "GameChoice" && scene.name != "QuizMenu";
    }

    public void SceneChangeMap(int i)
    {
        epMiniMap.sprite = ep5maps[i];
    }

    public void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void BackButton()
    {
        if(SceneManager.GetActiveScene().name == "QuizStory1" ||
            SceneManager.GetActiveScene().name == "QuizStory2" ||
            SceneManager.GetActiveScene().name == "QuizStory3" ||
            SceneManager.GetActiveScene().name == "QuizStory4")
        {
            SceneManager.LoadScene("QuizMenu");
            isOpened = true;
            Toggle();
            return;
        }
        if(SceneManager.GetActiveScene().name == "PhotoZone") {
            SceneManager.LoadScene("MainScene");
            isOpened = true;
            Toggle();
            return;
        }
        SceneManager.LoadScene("GameChoice");
        isOpened = true;
        Toggle();
    }

    public void ButtonClick(int i)
    {
        isOpened = true;
        Toggle();
        switch (i)
        {
            case 0:
                SceneManager.LoadScene("MainScene");
                break;
            case 1:
                GameObject.Find("GuideUI").GetComponent<Canvas>().enabled = true;
                break;
            case 2:
                GameObject.Find("Inventory").GetComponent<Canvas>().enabled = true;
                break;
            case 3:
                //GameObject.Find("MiniMap").GetComponent<Canvas>().enabled = true;
                miniMap.SetActive(true);
                break;
        }
    }

    public void MapClose()
    {
        epMiniMap.transform.parent.parent.gameObject.SetActive(false);
    }
}
