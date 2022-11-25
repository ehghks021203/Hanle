using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class TakeScreenshot : MonoBehaviour
{
    [SerializeField]
    GameObject blink;

    private bool onCapture = false;

    private void OnPostRender() {
        if (onCapture) {
            if (Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite) == false) {
                Permission.RequestUserPermission(Permission.ExternalStorageWrite);
            }

            string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
            string fileLocate =  "mnt/sdcard/DCIM/Screenshots";
            string fileName = "/Screenshot" + timeStamp + ".png";
            string pathToSave = fileLocate + fileName;
            if (!Directory.Exists(fileLocate))
                Directory.CreateDirectory(fileLocate);

            Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            Rect area = new Rect(0f, 0f, Screen.width, Screen.height);
            screenShot.ReadPixels(area, 0, 0);
            
            File.WriteAllBytes(pathToSave, screenShot.EncodeToPNG());
            Destroy(screenShot);
            onCapture = false;
        }
    }

    public void TakeAShot() {
        onCapture = true;
    }

    /*
    public void TakeAShot ()
    {
        StartCoroutine("CaptureIt");
    }
    IEnumerator CaptureIt()
    {
        string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
        string fileName = "/Screenshot" + timeStamp + ".png";
        string pathToSave = Application.persistentDataPath + fileName;
        ScreenCapture.CaptureScreenshot(pathToSave);
        yield return new WaitForEndOfFrame();
        Instantiate(blink, new Vector2(0f, 0f), Quaternion.identity);
    }
    */
}
