using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public GameObject settingsMenu;

    public Volume globalVolume;
    public Slider brightnessSlider;      
    public TMP_InputField brightnessInput; 

    float minBrightness = -10f;
    float maxBrightness = -5f;

    Exposure exposure;


    public TMP_Dropdown resolution;

    public TMP_Dropdown screenMode;



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

    private InputActionRebindingExtensions.RebindingOperation rebindingOp;

    private const string SaveKey = "rhythm_rebinds";



    void Start()
    {
        globalVolume.profile.TryGet(out exposure);
        if (exposure == null) return;

        exposure.mode.value = ExposureMode.Fixed;

        brightnessSlider.onValueChanged.AddListener(OnSlider);
        brightnessInput.onEndEdit.AddListener(OnInput);

        UpdateUI(exposure.compensation.value);

        resolution.onValueChanged.AddListener(SetResolution);

        resolution.value = 0;
        resolution.RefreshShownValue();
        SetResolution(0);

        screenMode.onValueChanged.AddListener(SetScreenMode);

        screenMode.value = 0;
        screenMode.RefreshShownValue();
        SetScreenMode(0);

        LoadRebinds();
        RefreshUI();

    }
    public void SetResolution(int index)
    {
        var resolution = Screen.currentResolution.refreshRateRatio;

        switch (index)
        {
            case 0:
                Screen.SetResolution(1280, 720, Screen.fullScreenMode, resolution);
                break;

            case 1:
                Screen.SetResolution(1920, 1080, Screen.fullScreenMode, resolution);
                break;

            //case 2:
            //    Screen.SetResolution(2560, 1440, Screen.fullScreenMode, resolution);
            //    break;
        }
    }

    public void SetScreenMode(int index)
    {
        switch (index)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;

            case 1:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;

            case 2:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
        }
    }

    void OnSlider(float value)
    {
        float brightness = Mathf.Lerp(minBrightness, maxBrightness, value);
        exposure.compensation.value = brightness;
        UpdateUI(brightness);
    }

    void OnInput(string text)
    {
        if (!float.TryParse(text, out float percent)) return;

        percent = Mathf.Clamp(percent, 0, 100);

        float brightness = Mathf.Lerp(minBrightness, maxBrightness, percent / 100f);
        exposure.compensation.value = brightness;

        UpdateUI(brightness);
    }

    public void IncreaseBrightness() => Change(5);
    public void DecreaseBrightness() => Change(-5);

    void Change(float percentStep)
    {
        float currentPercent = Mathf.InverseLerp(minBrightness, maxBrightness, exposure.compensation.value) * 100f;
        currentPercent = Mathf.Clamp(currentPercent + percentStep, 0, 100);

        float brightness = Mathf.Lerp(minBrightness, maxBrightness, currentPercent / 100f);
        exposure.compensation.value = brightness;

        UpdateUI(brightness);
    }

    void UpdateUI(float brightness)
    {
        float percent = Mathf.InverseLerp(minBrightness, maxBrightness, brightness) * 100f;

        brightnessSlider.value = percent / 100f;
        brightnessInput.text = Mathf.RoundToInt(percent).ToString();
    }





    // UI Buttons call these:
    public void RebindLane1() => StartRebind(lane1, lane1Text);
    public void RebindLane2() => StartRebind(lane2, lane2Text);
    public void RebindLane3() => StartRebind(lane3, lane3Text);
    public void RebindLane4() => StartRebind(lane4, lane4Text);

    private void StartRebind(InputActionReference actionRef, TMP_Text label)
    {
        if (actionRef == null || actionRef.action == null) return;

        var action = actionRef.action;

        // show message
        label.text = "Press a key...";

        // disable while rebinding (prevents gameplay hit)
        action.Disable();

        // bindingIndex 0 = first binding (your keyboard binding)
        rebindingOp = action.PerformInteractiveRebinding(0).WithCancelingThrough("<Keyboard>/escape").WithControlsExcluding("<Mouse>/position").WithControlsExcluding("<Mouse>/delta").OnComplete(op =>
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
        lane1.action.RemoveAllBindingOverrides();
        lane2.action.RemoveAllBindingOverrides();
        lane3.action.RemoveAllBindingOverrides();
        lane4.action.RemoveAllBindingOverrides();

        SaveRebinds();
        RefreshUI();
    }

    private void SaveRebinds()
    {
        if (inputActionsAsset == null) return;

        string json = inputActionsAsset.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save();
    }

    private void LoadRebinds()
    {
        if (inputActionsAsset == null) return;
        if (!PlayerPrefs.HasKey(SaveKey)) return;

        string json = PlayerPrefs.GetString(SaveKey);
        inputActionsAsset.LoadBindingOverridesFromJson(json);
    }









    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
    }
}
