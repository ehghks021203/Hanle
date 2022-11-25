using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioList : MonoBehaviour
{
    [SerializeField] GameObject audio_prefab;
    [SerializeField] Transform audioListPanel;

    [SerializeField] AudioLibrary m_audioLibrary;

    private void Awake() {
        for (int i = 0; i < m_audioLibrary.audios.Count; i++) {
            GameObject instance  = GameObject.Instantiate(audio_prefab, audioListPanel);
            instance.GetComponent<AudioUnit>().unit_id = i;
        }
    }
}
