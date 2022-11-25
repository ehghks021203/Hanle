using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public static AudioController instance = null;

    public AudioMixer masterMixer;
    public Slider BGMSlider;
    public Slider SESlider;
    public Text BGMText;
    public Text SEText;

    private float BGM_volume = 1f;
    private float SE_volume = 1f;
    private int BGM = 100;
    private int SE = 100;

    private void Awake() {
        if (instance == null) 
            instance = this;
        else if (instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        AudioInit(masterMixer, BGMSlider, BGMText, "BGM");
        AudioInit(masterMixer, SESlider, SEText, "SE");
    }

    private void AudioInit(AudioMixer audioMixer, Slider slider, Text text, string type) {
        var volume = PlayerPrefs.GetFloat(type, 0.5f);
        slider.value = volume;
        AudioSetting(audioMixer, slider, text, type);
    }

    private void AudioSetting(AudioMixer audioMixer, Slider slider, Text text, string type) {
        var volume = slider.value;
        if (volume == 0f)   audioMixer.SetFloat(type, -80);
        else    audioMixer.SetFloat(type, -40 + volume * 40);
        text.text = ((int)Mathf.Floor(volume*100)).ToString();
    }

    public void BGMAudioControl() {
        AudioSetting(masterMixer, BGMSlider, BGMText, "BGM");
    }

    public void SEAudioControl() {
        AudioSetting(masterMixer, SESlider, SEText, "SE");
    }
}
