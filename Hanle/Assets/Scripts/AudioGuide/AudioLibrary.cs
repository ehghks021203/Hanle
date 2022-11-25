using System.Collections.Generic;
using UnityEngine;

public class AudioLibrary : MonoBehaviour {
    public List<Audio> audios;
}

[System.Serializable]
public class Audio {
    public string titleName;
    public string subTitleName;
    public Sprite audioSprite;
    public AudioClip audioResource;
    public Sprite audioText;
}