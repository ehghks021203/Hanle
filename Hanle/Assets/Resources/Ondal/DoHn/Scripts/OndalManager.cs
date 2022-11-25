using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class OndalManager : MonoBehaviour
{
    public bool isBattle = false; 

    [Header("GET BALL")]
    [SerializeField] GameObject ballWMLoader;
    [SerializeField] GameObject getBallNoticeObj;
    [SerializeField] GameObject getBallCount;
    //private bool isTimeOver = false;
    private bool isGBNoticeOn = false;
    public int getBalls = 0;
    

    [Header("BATTLE")]
    [SerializeField] GameObject enemyWMLoader;
    [SerializeField] GameObject intoBattleUI;
    [SerializeField] GameObject battleNoticeObj;
    [SerializeField] GameObject throwBallObj;
    public int totalEnemy = 5;
    public bool isClear = false;

    [Header("UI")]
    [SerializeField] GameObject clearUI;


    private void Update() {
        if (!isBattle) {
            if (getBalls < 5) {
                getBallCount.transform.GetChild(0).GetComponent<Text>().text = string.Format("{0} / 5", getBalls.ToString());
            }
            else {
                getBallCount.transform.GetChild(0).GetComponent<Text>().text = string.Format("{0} / 5", getBalls.ToString());
                intoBattleUI.SetActive(true);
            }
        }
        else if (isBattle) {
            getBallCount.transform.GetChild(1).GetComponent<Text>().text = "남은 병사";
            getBallCount.transform.GetChild(0).GetComponent<Text>().text = string.Format("{0} / 5", totalEnemy.ToString());
        }
        if (!isClear && totalEnemy <= 0) {
            StartCoroutine(OndalClear());
            throwBallObj.SetActive(false);
        }
    }

#region BALL_GET
    public void Start_GetBall() {
        // Create WebMapLoader
        Instantiate(ballWMLoader, this.transform.position, Quaternion.identity);
    }

    public void ShowGetBallWarning() {
        if (!isGBNoticeOn)
            StartCoroutine(GetBallNotice(getBallNoticeObj));
    }

    IEnumerator GetBallNotice(GameObject obj) {
        isGBNoticeOn = true;
        obj.SetActive(true);
        var obj_cg = obj.GetComponent<CanvasGroup>();
        obj_cg.alpha = 0f;
        obj_cg.DOFade(1.0f, 1.0f);
        yield return new WaitForSeconds(1.5f);
        obj_cg.DOFade(0.0f, 1.0f);
        yield return new WaitForSeconds(1.0f);
        obj.SetActive(false);
        isGBNoticeOn = false;
    }
#endregion

#region BATTLE
    public void Start_Battle() {
        isBattle = true;
        StartCoroutine(BattleNotice(battleNoticeObj));
        Instantiate(enemyWMLoader, this.transform.position, Quaternion.identity);
        throwBallObj.SetActive(true);
    }

    IEnumerator BattleNotice(GameObject obj) {
        obj.SetActive(true);
        var obj_cg = obj.GetComponent<CanvasGroup>();
        obj_cg.DOFade(1.0f, 1.0f);
        yield return new WaitForSeconds(5.0f);
        obj_cg.DOFade(0.0f, 1.0f);
        yield return new WaitForSeconds(1.0f);
        obj.SetActive(false);
    }

    IEnumerator OndalClear() {
        clearUI.SetActive(true);
        var obj_cg = clearUI.GetComponent<CanvasGroup>();
        obj_cg.DOFade(1.0f, 1.0f);
        yield return null;
    }
#endregion

#region UI
    public void ChangeBallCount(int change) {
        getBalls += change;
    }
#endregion
}