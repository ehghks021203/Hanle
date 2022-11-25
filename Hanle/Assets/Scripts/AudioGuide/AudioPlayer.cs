using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class AudioPlayer : MonoBehaviour {
    [Header("Audio Library & Source")]
    [SerializeField] AudioLibrary m_AudioLibrary;
    [SerializeField] AudioSource m_AudioSource;

    [Header("Audio Setting (MAIN)")]
    [SerializeField] Image m_AudioImage;
    [SerializeField] Text m_TitleText;
    [SerializeField] Text m_SubTitleText;

    [Header("Audio Setting (TOPBAR)")]
    [SerializeField] Image m_AudioImage_t;
    [SerializeField] Text m_TitleText_t;
    [SerializeField] Text m_SubTitleText_t;

    [Header("Audio Setting (BOTTOMBAR)")]
    [SerializeField] Image m_AudioImage_b;
    [SerializeField] Text m_TitleText_b;
    [SerializeField] Text m_SubTitleText_b;

    // SLIDERS
    [Header("Slider")]
    [SerializeField] Slider m_CurrentTimeSlider;
    [SerializeField] Slider m_CurrentTimeSlider_t;
    [SerializeField] Slider m_CurrentTimeSlider_b;
    [SerializeField] Slider m_InteractiveSlider;

    [SerializeField] Text m_CurrentTimeText;
    [SerializeField] Text m_TotalTimeText;

    // PLAY
    [Header("Play & Pause Button(N)")]
    [SerializeField] Image m_PlayImage;
    [SerializeField] Image m_PauseImage;

    [Header("Play & Pause Button(T)")]
    [SerializeField] Image m_PlayImage_t;
    [SerializeField] Image m_PauseImage_t;

    [Header("Play & Pause Button(B)")]
    [SerializeField] Image m_PlayImage_b;
    [SerializeField] Image m_PauseImage_b;

    // PLAY SPEED
    [Header("Play Speed Setting")]
    [SerializeField] GameObject m_PlaySpeedSelectPanel;
    [SerializeField] RectTransform m_CurrentSelectIcon;
    private float m_PlaySpeed = 1.0f;
    private bool m_IsPlaySpeedPanelOpen = false;

    // AUTO PLAY
    [Header("Auto Play")]
    [SerializeField] Image m_AutoPlayImage;
    [SerializeField] Color32 m_AutoPlayButtonColor_Off = Color.white;
    [SerializeField] Color32 m_AutoPlayButtonCOlor_On = new Color32(246, 87, 86, 255);
    [SerializeField] bool m_AutoPlay = true;

    [Header("AudioText")]
    [SerializeField] Image m_AudioTextImage;

    [Header("Canvas")]
    [SerializeField] Canvas m_AudioPlayCanvas;
    [SerializeField] Canvas m_AudioListCanvas;
    [SerializeField] GameObject m_TextPanel;

    private bool m_IsPlaying = false;
    public static int currentPlayIndex = 0;

    private Audio m_CurrentAudio;
    public int m_CurrentAudioIndex = 0;

    private bool m_IsDragging = false;
    private LoadScene loadScene;

    [Header("Audio Sound Effect")]
    [SerializeField] AudioSource sceneEnterSoundEffect;

    [SerializeField] StandaloneInputModule m_StandaloneInputModule;
    
    // have to implement our custom timer as audiosource is playing is not reliable
    private float m_AudioClipLength = 0;

    private void Start()
    {
        sceneEnterSoundEffect.Play();
        loadScene = this.GetComponent<LoadScene>();
    }

    private void AudioSource_CheckComplete()
    {
        if (m_IsPlaying && m_AudioSource.time >= m_AudioClipLength)
        {
            m_StandaloneInputModule.DeactivateModule();
            m_StandaloneInputModule.ActivateModule();

            m_IsPlaying = false;

            if (m_AutoPlay) {
                Reset();
                IncrementIndex();
                LoadAudio(m_CurrentAudioIndex);
                Play();
            }
            else {
                Pause();
                Reset();
            }
        }
    }

    private void Update()
    {
        if(m_IsPlaying)
        {
            AudioSource_CheckComplete();

            if(!m_IsDragging) {
                MovePlayHead();
            }

            SetCurrentTimeUI();
        }
    }

    private void MovePlayHead() {
        m_CurrentTimeSlider.value = m_AudioSource.time;
        m_CurrentTimeSlider_t.value = m_CurrentTimeSlider.value;
        m_CurrentTimeSlider_b.value = m_CurrentTimeSlider.value;
    }

    public void LoadAudio(int audio_id)
    {
        if (audio_id > m_AudioLibrary.audios.Count) {
            Debug.Log("Index was out of range.");
            return;
        }

        m_CurrentAudio = m_AudioLibrary.audios[audio_id];

        if(m_CurrentAudio.audioSprite != null)
            SetAudioImage(m_CurrentAudio.audioSprite);
        else
            Debug.Log("Could not find album image");
        
        m_AudioSource.clip = m_CurrentAudio.audioResource;

        m_CurrentTimeSlider.maxValue = m_CurrentAudio.audioResource.length;
        m_CurrentTimeSlider_t.maxValue = m_CurrentTimeSlider.maxValue;
        m_CurrentTimeSlider_b.maxValue = m_CurrentTimeSlider.maxValue;
        m_InteractiveSlider.maxValue = m_CurrentAudio.audioResource.length;

        m_TotalTimeText.text = m_CurrentAudio.audioResource.length.ToString();

        UpdateTitleLabels(m_CurrentAudio.titleName, m_CurrentAudio.subTitleName);
        SetAudioText(m_CurrentAudio.audioText);

        m_AudioClipLength = m_CurrentAudio.audioResource.length;

        SetTotalTimeText();
        Reset();
    }

    public void OnScrubBarHeadMove()
    {
        if(!m_IsPlaying) {
            if ((int)m_InteractiveSlider.value == 0 || (int)m_InteractiveSlider.value == m_AudioClipLength)
                return;

            if ((int)m_InteractiveSlider.value != m_AudioClipLength)
                Play();
            else
                return;
        }


        m_CurrentTimeSlider.value = (int)m_InteractiveSlider.value;
        m_CurrentTimeSlider_t.value = m_CurrentTimeSlider.value;
        m_CurrentTimeSlider_b.value = m_CurrentTimeSlider.value;
        m_IsDragging = true;
    }

    public void OnPointerUp() {
        m_AudioSource.time = (int)m_InteractiveSlider.value;
        m_IsDragging = false;
    }


    public void OnClick_PreviousSong() {
        if (m_AudioSource.time > 5f) {
            Reset();
        }
        else {
            DecrementIndex();
            LoadAudio(m_CurrentAudioIndex);
        }
        Play();
    }

    public void OnClick_NextSong()
    {
        IncrementIndex();
        LoadAudio(m_CurrentAudioIndex);
        Play();
    }

    public void OnClick_TogglePlay() {
        m_IsPlaying = !m_IsPlaying;

        if(m_IsPlaying)
            Play();
        else
            Pause();
    }

    public void OnClick_AutoPlay() {
        m_AutoPlay = !m_AutoPlay;
        if (m_AutoPlay)
            m_AutoPlayImage.color = m_AutoPlayButtonCOlor_On;
        else
            m_AutoPlayImage.color = m_AutoPlayButtonColor_Off;
    }

    public void OnClick_PlaySpeed() {
        m_IsPlaySpeedPanelOpen = !m_IsPlaySpeedPanelOpen;
        if (m_IsPlaySpeedPanelOpen)
            m_PlaySpeedSelectPanel.SetActive(true);
        else
            m_PlaySpeedSelectPanel.SetActive(false);
    }

    public void OnClick_TextPanel() {
        //m_TextPanel.enabled = !m_TextPanel.enabled;
        //m_TextPanel.gameObject.SetActive(!m_TextPanel.gameObject.activeInHierarchy);
        if(m_TextPanel.gameObject.activeInHierarchy)
        {
            m_TextPanel.GetComponent<RectTransform>().DOKill();
            m_TextPanel.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, -1120), 0.5f).OnComplete(delegate
            {
                m_TextPanel.gameObject.SetActive(false);
            });
        } else
        {
            m_TextPanel.gameObject.SetActive(true);
            m_TextPanel.GetComponent<RectTransform>().DOKill();
            m_TextPanel.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 960), 0.5f);
        }

        SetAudioText(m_CurrentAudio.audioText);
    }

    public void OnClick_BackButton() {
        if (m_AudioPlayCanvas.gameObject.activeInHierarchy == true) {
            m_AudioListCanvas.gameObject.SetActive(true);
            m_AudioPlayCanvas.gameObject.SetActive(false);
        }
        else {
            loadScene.Load();
        }
    }

    public void SetPlaySpeed(float pitch) {
        var buttonHeight = 92;
        switch (pitch) {
            case 0.5f:
                m_CurrentSelectIcon.anchoredPosition = new Vector2(-192, -358 + buttonHeight*0);
                break;
            case 0.75f:
                m_CurrentSelectIcon.anchoredPosition = new Vector2(-192, -358 + buttonHeight*1);
                break;
            case 1.0f:
                m_CurrentSelectIcon.anchoredPosition = new Vector2(-192, -358 + buttonHeight*2);
                break;
            case 1.25f:
                m_CurrentSelectIcon.anchoredPosition = new Vector2(-192, -358 + buttonHeight*3);
                break;
            case 1.5f:
                m_CurrentSelectIcon.anchoredPosition = new Vector2(-192, -358 + buttonHeight*4);
                break;
            default:
                Debug.Log("Wrong value");
                return;
        }
        m_AudioSource.pitch = pitch;
        m_PlaySpeedSelectPanel.SetActive(false);
    }
 
    public void OpenAudioPlayCanvas() {
        m_AudioPlayCanvas.gameObject.SetActive(true);
        m_AudioListCanvas.gameObject.SetActive(false);
    }

    private void Pause() {
        m_PlayImage.gameObject.SetActive(true);
        m_PauseImage.gameObject.SetActive(false);
        m_PlayImage_t.gameObject.SetActive(true);
        m_PauseImage_t.gameObject.SetActive(false);
        m_PlayImage_b.gameObject.SetActive(true);
        m_PauseImage_b.gameObject.SetActive(false);
        m_AudioSource.Pause();
        m_IsPlaying = false;
    }

    public void Play() {
        m_PlayImage.gameObject.SetActive(false);
        m_PauseImage.gameObject.SetActive(true);
        m_PlayImage_t.gameObject.SetActive(false);
        m_PauseImage_t.gameObject.SetActive(true);
        m_PlayImage_b.gameObject.SetActive(false);
        m_PauseImage_b.gameObject.SetActive(true);
        m_AudioSource.Play();
        m_IsPlaying = true;
    }

    // sets audio clip to 0
    private void Reset() {
        m_AudioSource.time = 0;
        m_CurrentTimeSlider.value = 0;
        m_CurrentTimeSlider_t.value = m_CurrentTimeSlider.value;
        m_CurrentTimeSlider_b.value = m_CurrentTimeSlider.value;
        m_InteractiveSlider.value = 0;
    }

    private void IncrementIndex() {
        m_CurrentAudioIndex++;

        if (m_CurrentAudioIndex >= m_AudioLibrary.audios.Count) {
            m_CurrentAudioIndex = 0;
            return;
        }
    }

    private bool IsLastAudio()
    {
        if(m_CurrentAudioIndex + 1 >= m_AudioLibrary.audios.Count)
            return true;
        return false;
    }

    private void DecrementIndex()
    {
        m_CurrentAudioIndex--;

        if (m_CurrentAudioIndex < 0) {
            m_CurrentAudioIndex = m_AudioLibrary.audios.Count - 1;
            return;
        }
    }

    private void SetCurrentTimeUI()
    {
        string minutes = Mathf.Floor((int)m_AudioSource.time / 60).ToString("00");
        string seconds = ((int)m_AudioSource.time % 60).ToString("00");

        m_CurrentTimeText.text = minutes + ":" + seconds;
    }

    private void SetTotalTimeText()
    {
        string minutes = Mathf.Floor((int)m_AudioSource.clip.length / 60).ToString("00");
        string seconds = ((int)m_AudioSource.clip.length % 60).ToString("00");

        m_TotalTimeText.text = minutes + ":" + seconds;
    }

    private void UpdateTitleLabels(string title, string subtitle)
    {
        if (subtitle.Length >= 18) {
            m_SubTitleText.fontSize = 36;
            m_SubTitleText_b.fontSize = 28;
            m_SubTitleText_t.fontSize = 28;
        }
        else {
            m_SubTitleText.fontSize = 48;
            m_SubTitleText_b.fontSize = 36;
            m_SubTitleText_t.fontSize = 36;
        }
        m_TitleText.text = title;
        m_TitleText_t.text = title;
        m_TitleText_b.text = title;
        m_SubTitleText.text = subtitle;
        m_SubTitleText_t.text = subtitle;
        m_SubTitleText_b.text = subtitle;
    }

    private void SetAudioImage(Sprite sprite)
    {
        if(m_AudioImage != null && m_AudioImage_t != null && m_AudioImage_b != null) {
            m_AudioImage.sprite = sprite;
            m_AudioImage_t.sprite = sprite;
            m_AudioImage_b.sprite = sprite;
        }
        else
            Debug.Log("Warning album image wont be set as m_AudioImage == null");
    }

    private void SetAudioText(Sprite sprite) {
        m_AudioTextImage.sprite = m_CurrentAudio.audioText;
        m_AudioTextImage.SetNativeSize();
    }
}
