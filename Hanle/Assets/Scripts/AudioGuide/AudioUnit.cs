using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioUnit : MonoBehaviour {
    [SerializeField] AudioLibrary m_AudioLibrary;
    [SerializeField] AudioPlayer m_AudioPlayer;
    public int unit_id = 0;

    private void Start() {
        m_AudioLibrary = GameObject.FindObjectOfType<AudioLibrary>();
        m_AudioPlayer = GameObject.FindObjectOfType<AudioPlayer>();
        Image audioImage = this.GetComponentInChildren<Image>();
        if(audioImage != null) {
            audioImage.sprite = m_AudioLibrary.audios[unit_id].audioSprite;
        }
        var child_txt = this.GetComponentsInChildren<Text>();
        child_txt[0].text = m_AudioLibrary.audios[unit_id].titleName;
        child_txt[1].text = m_AudioLibrary.audios[unit_id].subTitleName;
        if (child_txt[1].text.Length >= 18) {
            child_txt[1].fontSize = 18;
        }
    }

    public void OnClick() {
        m_AudioPlayer.OpenAudioPlayCanvas();
        if (m_AudioPlayer.m_CurrentAudioIndex != unit_id) {
            m_AudioPlayer.m_CurrentAudioIndex = unit_id;
            m_AudioPlayer.LoadAudio(unit_id);
            m_AudioPlayer.Play();
        }
    }
}
