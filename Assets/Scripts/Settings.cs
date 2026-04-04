using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public GameObject settingsMenu;

    [Header("Brightness")]
    public Volume globalVolume;
    public Slider brightnessSlider;
    public TMP_InputField brightnessInput;

    private float minBrightness = -10f;
    private float maxBrightness = -5f;
    private Exposure exposure;

    [Header("Display")]
    public TMP_Dropdown resolution;
    public TMP_Dropdown screenMode;

    [Header("Audio")]
    public Slider masterVolumeSlider;
    public TMP_InputField masterVolumeInput;

    public Slider musicVolumeSlider;
    public TMP_InputField musicVolumeInput;

    [Header("Input Asset (for Save/Load)")]
    [SerializeField] private InputActionAsset inputActionsAsset;

    [Header("Lane Actions")]
    [SerializeField] private InputActionReference lane1;
    [SerializeField] private InputActionReference lane2;
    [SerializeField] private InputActionReference lane3;
    [SerializeField] private InputActionReference lane4;

    [Header("UI Text")]
    [SerializeField] private TMP_Text lane1Text;
    [SerializeField] private TMP_Text lane2Text;
    [SerializeField] private TMP_Text lane3Text;
    [SerializeField] private TMP_Text lane4Text;

    public GameObject content;

    private InputActionRebindingExtensions.RebindingOperation rebindingOp;

    private const string RebindSaveKey = "rhythm_rebinds";
    private const string BrightnessSaveKey = "settings_brightness";
    private const string ResolutionSaveKey = "settings_resolution";
    private const string ScreenModeSaveKey = "settings_screenmode";
    private const string MasterVolumeSaveKey = "settings_mastervolume";
    private const string MusicVolumeSaveKey = "settings_musicvolume";

    private void Awake()
    {
        if (globalVolume != null && globalVolume.profile != null)
        {
            globalVolume.profile.TryGet(out exposure);
        }

        if (exposure != null)
        {
            exposure.mode.value = ExposureMode.Fixed;
        }

        brightnessSlider.onValueChanged.AddListener(OnSlider);
        brightnessInput.onEndEdit.AddListener(OnInput);
        resolution.onValueChanged.AddListener(OnResolutionChanged);
        screenMode.onValueChanged.AddListener(OnScreenModeChanged);
        masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeSlider);
        masterVolumeInput.onEndEdit.AddListener(OnMasterVolumeInput);
        musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeSlider);
        musicVolumeInput.onEndEdit.AddListener(OnMusicVolumeInput);

        LoadAllSettings();
        LoadRebinds();
        RefreshUI();
    }

    private void LoadAllSettings()
    {
        // Brightness
        float defaultBrightness = Mathf.Lerp(minBrightness, maxBrightness, 0.5f);
        float savedBrightness = PlayerPrefs.GetFloat(BrightnessSaveKey, defaultBrightness);

        if (exposure != null)
        {
            exposure.compensation.value = savedBrightness;
            UpdateUI(savedBrightness);
        }

        // Resolution
        int savedResolution = PlayerPrefs.GetInt(ResolutionSaveKey, 0);

        if (resolution != null)
        {
            resolution.value = savedResolution;
            resolution.RefreshShownValue();
            SetResolution(savedResolution);
        }

        // Screen Mode
        int savedScreenMode = PlayerPrefs.GetInt(ScreenModeSaveKey, 0);

        if (screenMode != null)
        {
            screenMode.value = savedScreenMode;
            screenMode.RefreshShownValue();
            SetScreenMode(savedScreenMode);
        }

        LoadMasterVolume();
        LoadMusicVolume();
    }

    // Resolution
    private void OnResolutionChanged(int index)
    {
        SetResolution(index);
        SaveResolution(index);
    }

    public void SetResolution(int index)
    {
        var refreshRate = Screen.currentResolution.refreshRateRatio;

        switch (index)
        {
            case 0:
                Screen.SetResolution(1920, 1080, Screen.fullScreenMode, refreshRate);
                break;

            case 1:
                Screen.SetResolution(1280, 720, Screen.fullScreenMode, refreshRate);
                break;
        }
    }

    private void SaveResolution(int index)
    {
        PlayerPrefs.SetInt(ResolutionSaveKey, index);
        PlayerPrefs.Save();
    }

    // Screen Mode
    private void OnScreenModeChanged(int index)
    {
        SetScreenMode(index);
        SaveScreenMode(index);
    }

    public void SetScreenMode(int index)
    {
        switch (index)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;

            case 1:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
        }
    }

    private void SaveScreenMode(int index)
    {
        PlayerPrefs.SetInt(ScreenModeSaveKey, index);
        PlayerPrefs.Save();
    }

    // Brightness
    private void OnSlider(float value)
    {
        if (exposure == null) return;

        float brightness = Mathf.Lerp(minBrightness, maxBrightness, value);
        exposure.compensation.value = brightness;
        UpdateUI(brightness);
        SaveBrightness(brightness);
    }

    private void OnInput(string text)
    {
        if (exposure == null) return;
        if (!float.TryParse(text, out float percent)) return;

        percent = Mathf.Clamp(percent, 0, 100);

        float brightness = Mathf.Lerp(minBrightness, maxBrightness, percent / 100f);
        exposure.compensation.value = brightness;

        UpdateUI(brightness);
        SaveBrightness(brightness);
    }

    public void IncreaseBrightness()
    {
        Change(5);
    }

    public void DecreaseBrightness()
    {
        Change(-5);
    }

    private void Change(float percentStep)
    {
        if (exposure == null) return;

        float currentPercent = Mathf.InverseLerp(minBrightness, maxBrightness, exposure.compensation.value) * 100f;
        currentPercent = Mathf.Clamp(currentPercent + percentStep, 0, 100);

        float brightness = Mathf.Lerp(minBrightness, maxBrightness, currentPercent / 100f);
        exposure.compensation.value = brightness;

        UpdateUI(brightness);
        SaveBrightness(brightness);
    }

    private void UpdateUI(float brightness)
    {
        float percent = Mathf.InverseLerp(minBrightness, maxBrightness, brightness) * 100f;

        if (brightnessSlider != null)
            brightnessSlider.value = percent / 100f;

        if (brightnessInput != null)
            brightnessInput.text = Mathf.RoundToInt(percent).ToString();
    }

    private void SaveBrightness(float brightness)
    {
        PlayerPrefs.SetFloat(BrightnessSaveKey, brightness);
        PlayerPrefs.Save();
    }

    //Master Volume
    private void LoadMasterVolume()
    {
        float savedVolume = PlayerPrefs.GetFloat(MasterVolumeSaveKey, 1f);
        AudioListener.volume = savedVolume;
        UpdateAudioUI(masterVolumeSlider, masterVolumeInput, savedVolume);
    }

    private void OnMasterVolumeSlider(float value)
    {
        AudioListener.volume = value;
        UpdateAudioUI(masterVolumeSlider, masterVolumeInput, value);

        PlayerPrefs.SetFloat(MasterVolumeSaveKey, value);
        PlayerPrefs.Save();
    }

    private void OnMasterVolumeInput(string text)
    {
        if (!float.TryParse(text, out float percent)) return;

        percent = Mathf.Clamp(percent, 0, 100) / 100f;
        float value = percent / 100f;

        AudioListener.volume = value;
        UpdateAudioUI(masterVolumeSlider, masterVolumeInput, value);

        PlayerPrefs.SetFloat(MasterVolumeSaveKey, value);
        PlayerPrefs.Save();
    }

    // Music Volume

    private void LoadMusicVolume()
    {
        float savedVolume = PlayerPrefs.GetFloat(MusicVolumeSaveKey, 1f);

        if(GameManager.instance != null && GameManager.instance.musicSource != null)
            GameManager.instance.musicSource.volume = savedVolume;

        UpdateAudioUI(musicVolumeSlider, musicVolumeInput, savedVolume);
    }
    private void OnMusicVolumeSlider(float value)
    {
        if (GameManager.instance != null && GameManager.instance.musicSource != null)
            GameManager.instance.musicSource.volume = value;

        UpdateAudioUI(musicVolumeSlider, musicVolumeInput, value);

        PlayerPrefs.SetFloat(MusicVolumeSaveKey, value);
        PlayerPrefs.Save();
    }

    private void OnMusicVolumeInput(string text)
    {
        if (!float.TryParse(text, out float percent)) return;

        percent = Mathf.Clamp(percent, 0f, 100f);
        float value = percent / 100f;

        if (GameManager.instance != null && GameManager.instance.musicSource != null)
            GameManager.instance.musicSource.volume = value;

        UpdateAudioUI(musicVolumeSlider, musicVolumeInput, value);

        PlayerPrefs.SetFloat(MusicVolumeSaveKey, value);
        PlayerPrefs.Save();
    }

    private void UpdateAudioUI(Slider slider, TMP_InputField input, float value)
    {
        if (slider != null)
            slider.value = value;

        if (input != null)
            input.text = Mathf.RoundToInt(value * 100f).ToString();
    }

    // Rebinding
    public void RebindLane1() => StartRebind(lane1, lane1Text);
    public void RebindLane2() => StartRebind(lane2, lane2Text);
    public void RebindLane3() => StartRebind(lane3, lane3Text);
    public void RebindLane4() => StartRebind(lane4, lane4Text);

    private void StartRebind(InputActionReference actionRef, TMP_Text label)
    {
        if (actionRef == null || actionRef.action == null) return;

        var action = actionRef.action;

        label.text = "Press a key...";
        action.Disable();

        rebindingOp = action.PerformInteractiveRebinding(0)
            .WithCancelingThrough("<Keyboard>/escape")
            .WithControlsExcluding("<Mouse>/position")
            .WithControlsExcluding("<Mouse>/delta")
            .OnComplete(op =>
            {
                op.Dispose();
                action.Enable();
                SaveRebinds();
                RefreshUI();
            })
            .OnCancel(op =>
            {
                op.Dispose();
                action.Enable();
                RefreshUI();
            })
            .Start();
    }

    private void RefreshUI()
    {
        lane1Text.text = GetBindingName(lane1);
        lane2Text.text = GetBindingName(lane2);
        lane3Text.text = GetBindingName(lane3);
        lane4Text.text = GetBindingName(lane4);
    }

    private string GetBindingName(InputActionReference actionRef)
    {
        if (actionRef == null || actionRef.action == null) return "-";
        return actionRef.action.GetBindingDisplayString(0);
    }

    public void ResetDefaults()
    {
        if (lane1 != null && lane1.action != null) lane1.action.RemoveAllBindingOverrides();
        if (lane2 != null && lane2.action != null) lane2.action.RemoveAllBindingOverrides();
        if (lane3 != null && lane3.action != null) lane3.action.RemoveAllBindingOverrides();
        if (lane4 != null && lane4.action != null) lane4.action.RemoveAllBindingOverrides();

        SaveRebinds();
        RefreshUI();
    }

    private void SaveRebinds()
    {
        if (inputActionsAsset == null) return;

        string json = inputActionsAsset.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString(RebindSaveKey, json);
        PlayerPrefs.Save();
    }

    private void LoadRebinds()
    {
        if (inputActionsAsset == null) return;
        if (!PlayerPrefs.HasKey(RebindSaveKey)) return;

        string json = PlayerPrefs.GetString(RebindSaveKey);
        inputActionsAsset.LoadBindingOverridesFromJson(json);
    }

    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
    }

    public void GeneralBtn()
    {
        content.transform.localPosition = new Vector3(0, -305, 0);
    }
    public void AudioBtn()
    {
        content.transform.localPosition = new Vector3(0, 30, 0);
    }
    public void AccessibilityBtn()
    {
        content.transform.localPosition = new Vector3(0, 286, 0);
    }
    public void CustomizationBtn()
    {
        content.transform.localPosition = new Vector3(0, 305, 0);
    }
}