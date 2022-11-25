using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.ProceduralImage;
using UnityEngine.Android;
using System.IO;
using DG.Tweening;

public class ScreenShot : MonoBehaviour
{
    bool isCoroutinePlaying;
    bool onCapture = false;


    string albumName = "Hanle";     // 파일 불러올 때 필요
    [SerializeField] GameObject screenshotViewPanel;
    [SerializeField] Canvas captureCanvas;
    [SerializeField] Canvas galleryCanvas;

    [SerializeField] Image flashImage;
    [SerializeField] ProceduralImage toastImage;
    Text toastText;

    [SerializeField] GameObject effect;

    string[] files = null;
    int whichScreenShotIsShown = 0;

    private void Start() {
        toastText = toastImage.GetComponentInChildren<Text>();
    }

    public void OnClick_Capture() {
        StartCoroutine(Flash(0.2f));
        StartCoroutine(Toast(0.2f, 0.7f));
        //Instantiate(effect, new Vector2(0f, 0f), Quaternion.identity);
        onCapture = true;
    }

    private void OnPostRender() {
        if (onCapture) {
            onCapture = false;
            ScreenshotAndGallery();
        }
    }

    public void OnClick_Gallery() {
        GetPictureAndShowIt();
        captureCanvas.enabled = false;
        galleryCanvas.enabled = true;
    }
    
    public void OnClick_Gallery_Back() {
        captureCanvas.enabled = true;
        galleryCanvas.enabled = false;
    }

    void ScreenshotAndGallery() {
        Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        Rect area = new Rect(0f, 0f, Screen.width, Screen.height);
        screenShot.ReadPixels(area, 0, 0);

        Debug.Log("" + NativeGallery.SaveImageToGallery(screenShot, albumName, "Screenshot_" + System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + "{0}.png"));

        Destroy(screenShot);
    }

    void GetPictureAndShowIt() {
        string pathToFile = GetPicture.GetLastPicturePath();
        if (pathToFile == null) return;
        Texture2D texture = GetPictureImage(pathToFile);
        Rect area = new Rect(0f, 0f, Screen.width, Screen.height);
        Sprite spr = Sprite.Create(texture, area, new Vector2(0.5f, 0.5f));
        screenshotViewPanel.GetComponent<Image>().sprite = spr;
    }

    Texture2D GetPictureImage(string filePath)
    {
        Texture2D texture = null;
        byte[] fileBytes;
        if (File.Exists(filePath))
        {
            fileBytes = File.ReadAllBytes(filePath);
            texture = new Texture2D(2, 2, TextureFormat.RGB24, false);
            texture.LoadImage(fileBytes);
        }
        return texture;
    }

    public void NextPicture()
    {
        if (files.Length > 0)
        {
            whichScreenShotIsShown += 1;
            if (whichScreenShotIsShown > files.Length - 1)
                whichScreenShotIsShown = 0;
            GetPictureAndShowIt();
        }
    }

    public void PreviousPicture()
    {
        if(files.Length > 0)
        {
            whichScreenShotIsShown -= 1;
            if (whichScreenShotIsShown < 0)
                whichScreenShotIsShown = files.Length - 1;
            GetPictureAndShowIt();
        }
    }

    IEnumerator Flash(float time) {
        flashImage.enabled = true;
        flashImage.DOFade(1.0f, time);
        yield return new WaitForSeconds(time);
        flashImage.DOFade(0.0f, time);
        yield return new WaitForSeconds(time);
        flashImage.enabled = false;
    }

    IEnumerator Toast(float fadetime, float waittime) {
        toastImage.enabled = true;
        toastText.enabled = true;
        toastImage.DOFade(1.0f, fadetime);
        toastText.DOFade(1.0f, fadetime);
        yield return new WaitForSeconds(fadetime + waittime);
        toastImage.DOFade(0.0f, fadetime);
        toastText.DOFade(0.0f, fadetime);
        yield return new WaitForSeconds(fadetime);
        toastImage.enabled = false;
        toastText.enabled = false;
    }
}
