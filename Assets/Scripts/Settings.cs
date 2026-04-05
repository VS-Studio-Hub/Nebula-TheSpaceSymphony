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

    [Header("Scene Music Source")]
    public AudioSource musicSource;

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

        if (brightnessSlider != null)
            brightnessSlider.onValueChanged.AddListener(OnSlider);

        if (brightnessInput != null)
            brightnessInput.onEndEdit.AddListener(OnInput);

        if (resolution != null)
            resolution.onValueChanged.AddListener(OnResolutionChanged);

        if (screenMode != null)
            screenMode.onValueChanged.AddListener(OnScreenModeChanged);

        if (masterVolumeSlider != null)
            masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeSlider);

        if (masterVolumeInput != null)
            masterVolumeInput.onEndEdit.AddListener(OnMasterVolumeInput);

        if (musicVolumeSlider != null)
            musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeSlider);

        if (musicVolumeInput != null)
            musicVolumeInput.onEndEdit.AddListener(OnMusicVolumeInput);

        LoadAllSettings();
        LoadRebinds();
        RefreshUI();
    }

    private void OnDestroy()
    {
        if (brightnessSlider != null)
            brightnessSlider.onValueChanged.RemoveListener(OnSlider);

        if (brightnessInput != null)
            brightnessInput.onEndEdit.RemoveListener(OnInput);

        if (resolution != null)
            resolution.onValueChanged.RemoveListener(OnResolutionChanged);

        if (screenMode != null)
            screenMode.onValueChanged.RemoveListener(OnScreenModeChanged);

        if (masterVolumeSlider != null)
            masterVolumeSlider.onValueChanged.RemoveListener(OnMasterVolumeSlider);

        if (masterVolumeInput != null)
            masterVolumeInput.onEndEdit.RemoveListener(OnMasterVolumeInput);

        if (musicVolumeSlider != null)
            musicVolumeSlider.onValueChanged.RemoveListener(OnMusicVolumeSlider);

        if (musicVolumeInput != null)
            musicVolumeInput.onEndEdit.RemoveListener(OnMusicVolumeInput);

        if (rebindingOp != null)
        {
            rebindingOp.Dispose();
            rebindingOp = null;
        }
    }

    private void LoadAllSettings()
    {
        float defaultBrightness = Mathf.Lerp(minBrightness, maxBrightness, 0.5f);
        float savedBrightness = PlayerPrefs.GetFloat(BrightnessSaveKey, defaultBrightness);

        if (exposure != null)
        {
            exposure.compensation.value = savedBrightness;
            UpdateBrightnessUI(savedBrightness);
        }

        int savedResolution = PlayerPrefs.GetInt(ResolutionSaveKey, 0);

        if (resolution != null)
        {
            resolution.value = savedResolution;
            resolution.RefreshShownValue();
            SetResolution(savedResolution);
        }

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

    private void OnSlider(float value)
    {
        if (exposure == null) return;

        value = Mathf.Clamp01(value);

        float brightness = Mathf.Lerp(minBrightness, maxBrightness, value);
        exposure.compensation.value = brightness;

        UpdateBrightnessUI(brightness);
        SaveBrightness(brightness);
    }

    private void OnInput(string text)
    {
        if (exposure == null) return;
        if (!float.TryParse(text, out float percent)) return;

        percent = Mathf.Clamp(percent, 0f, 100f);

        float brightness = Mathf.Lerp(minBrightness, maxBrightness, percent / 100f);
        exposure.compensation.value = brightness;

        UpdateBrightnessUI(brightness);
        SaveBrightness(brightness);
    }

    public void IncreaseBrightness()
    {
        ChangeBrightness(5f);
    }

    public void DecreaseBrightness()
    {
        ChangeBrightness(-5f);
    }

    private void ChangeBrightness(float percentStep)
    {
        if (exposure == null) return;

        float currentPercent = Mathf.InverseLerp(minBrightness, maxBrightness, exposure.compensation.value) * 100f;
        currentPercent = Mathf.Clamp(currentPercent + percentStep, 0f, 100f);

        float brightness = Mathf.Lerp(minBrightness, maxBrightness, currentPercent / 100f);
        exposure.compensation.value = brightness;

        UpdateBrightnessUI(brightness);
        SaveBrightness(brightness);
    }

    private void UpdateBrightnessUI(float brightness)
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

    private void LoadMasterVolume()
    {
        float savedVolume = PlayerPrefs.GetFloat(MasterVolumeSaveKey, 1f);
        savedVolume = Mathf.Clamp01(savedVolume);

        AudioListener.volume = savedVolume;
        UpdateAudioUI(masterVolumeSlider, masterVolumeInput, savedVolume);
    }

    private void OnMasterVolumeSlider(float value)
    {
        value = Mathf.Clamp01(value);

        AudioListener.volume = value;
        UpdateAudioUI(masterVolumeSlider, masterVolumeInput, value);

        PlayerPrefs.SetFloat(MasterVolumeSaveKey, value);
        PlayerPrefs.Save();
    }

    private void OnMasterVolumeInput(string text)
    {
        if (!float.TryParse(text, out float percent)) return;

        percent = Mathf.Clamp(percent, 0f, 100f);
        float value = percent / 100f;

        AudioListener.volume = value;
        UpdateAudioUI(masterVolumeSlider, masterVolumeInput, value);

        PlayerPrefs.SetFloat(MasterVolumeSaveKey, value);
        PlayerPrefs.Save();
    }

    private void LoadMusicVolume()
    {
        float savedVolume = PlayerPrefs.GetFloat(MusicVolumeSaveKey, 1f);
        savedVolume = Mathf.Clamp01(savedVolume);

        if (musicSource != null)
            musicSource.volume = savedVolume;

        UpdateAudioUI(musicVolumeSlider, musicVolumeInput, savedVolume);
    }

    private void OnMusicVolumeSlider(float value)
    {
        value = Mathf.Clamp01(value);

        if (musicSource != null)
            musicSource.volume = value;

        UpdateAudioUI(musicVolumeSlider, musicVolumeInput, value);

        PlayerPrefs.SetFloat(MusicVolumeSaveKey, value);
        PlayerPrefs.Save();
    }

    private void OnMusicVolumeInput(string text)
    {
        if (!float.TryParse(text, out float percent)) return;

        percent = Mathf.Clamp(percent, 0f, 100f);
        float value = percent / 100f;

        if (musicSource != null)
            musicSource.volume = value;

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

    public void RebindLane1() => StartRebind(lane1, lane1Text);
    public void RebindLane2() => StartRebind(lane2, lane2Text);
    public void RebindLane3() => StartRebind(lane3, lane3Text);
    public void RebindLane4() => StartRebind(lane4, lane4Text);

    private void StartRebind(InputActionReference actionRef, TMP_Text label)
    {
        if (actionRef == null || actionRef.action == null || label == null) return;

        if (rebindingOp != null)
        {
            rebindingOp.Dispose();
            rebindingOp = null;
        }

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
                rebindingOp = null;
                action.Enable();
                SaveRebinds();
                RefreshUI();
            })
            .OnCancel(op =>
            {
                op.Dispose();
                rebindingOp = null;
                action.Enable();
                RefreshUI();
            })
            .Start();
    }

    private void RefreshUI()
    {
        if (lane1Text != null) lane1Text.text = GetBindingName(lane1);
        if (lane2Text != null) lane2Text.text = GetBindingName(lane2);
        if (lane3Text != null) lane3Text.text = GetBindingName(lane3);
        if (lane4Text != null) lane4Text.text = GetBindingName(lane4);
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
        if (settingsMenu != null)
            settingsMenu.SetActive(false);
    }

    public void GeneralBtn()
    {
        if (content != null)
            content.transform.localPosition = new Vector3(0, -305, 0);
    }

    public void AudioBtn()
    {
        if (content != null)
            content.transform.localPosition = new Vector3(0, 30, 0);
    }

    public void AccessibilityBtn()
    {
        if (content != null)
            content.transform.localPosition = new Vector3(0, 286, 0);
    }

    public void CustomizationBtn()
    {
        if (content != null)
            content.transform.localPosition = new Vector3(0, 305, 0);
    }
}