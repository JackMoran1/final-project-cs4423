using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio; // Required for AudioMixer
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[System.Serializable]
public class GameSettings
{
    public int resolutionIndex;
    public bool isVsyncEnabled;
    public bool isFullscreen;
    public float masterVolume;
    public float musicVolume;
    public float sfxVolume;
    public float brightness;
    public float contrast;
    public float coloration;
}

public class OptionsManager : MonoBehaviour
{
    public Dropdown resolutionDropdown;
    public Toggle vsyncToggle;
    public Toggle fullscreenToggle;
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    public Button applyButton;
    public AudioMixer audioMixer; 
    public Slider brightnessSlider;
    public Slider contrastSlider;
    public Slider colorationSlider;

    private Resolution[] resolutions;
    public VolumeProfile postProcessingVolume;
    private ColorAdjustments colorAdjustments;
    private GameSettings gameSettings = new GameSettings();
    private string settingsFilePath;

    void Awake()
    {
        settingsFilePath = Path.Combine(Application.persistentDataPath, "gameSettings.json");
    }

    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        postProcessingVolume.TryGet(out colorAdjustments);
    
        LoadSettings();
        PopulateResolutionDropdown();

        applyButton.onClick.AddListener(ApplyAndSaveSettings);

        

    }

    void PopulateResolutionDropdown()
    {
        foreach (var res in resolutions)
        {
            resolutionDropdown.options.Add(new Dropdown.OptionData(res.width + "x" + res.height));
        }
        resolutionDropdown.value = gameSettings.resolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void ApplyAndSaveSettings()
    {
        Resolution selectedResolution = resolutions[resolutionDropdown.value];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, fullscreenToggle.isOn);
        QualitySettings.vSyncCount = vsyncToggle.isOn ? 1 : 0;
        audioMixer.SetFloat("Master", LinearToDecibel(masterVolumeSlider.value));
        audioMixer.SetFloat("Music",  LinearToDecibel(musicVolumeSlider.value));
        audioMixer.SetFloat("SFX",  LinearToDecibel(sfxVolumeSlider.value));
        colorAdjustments.postExposure.value = brightnessSlider.value;
        colorAdjustments.contrast.value = contrastSlider.value;
        colorAdjustments.saturation.value = colorationSlider.value;

        gameSettings.resolutionIndex = resolutionDropdown.value;
        gameSettings.isVsyncEnabled = vsyncToggle.isOn;
        gameSettings.isFullscreen = fullscreenToggle.isOn;
        gameSettings.masterVolume = masterVolumeSlider.value;
        gameSettings.musicVolume = musicVolumeSlider.value;
        gameSettings.sfxVolume = sfxVolumeSlider.value;
        gameSettings.brightness = brightnessSlider.value;
        gameSettings.contrast = contrastSlider.value;
        gameSettings.coloration = colorationSlider.value;
        string json = JsonUtility.ToJson(gameSettings, true);
        File.WriteAllText(settingsFilePath, json);
        SceneManager.LoadScene("MainMenu");
    }

    void LoadSettings()
    {
        if (File.Exists(settingsFilePath))
        {
            string json = File.ReadAllText(settingsFilePath);
            gameSettings = JsonUtility.FromJson<GameSettings>(json);

            resolutionDropdown.value = gameSettings.resolutionIndex;
            vsyncToggle.isOn = gameSettings.isVsyncEnabled;
            fullscreenToggle.isOn = gameSettings.isFullscreen;
            masterVolumeSlider.value = gameSettings.masterVolume;
            musicVolumeSlider.value = gameSettings.musicVolume;
            sfxVolumeSlider.value = gameSettings.sfxVolume;
            colorAdjustments.postExposure.value = brightnessSlider.value = gameSettings.brightness;
            colorAdjustments.contrast.value = contrastSlider.value = gameSettings.contrast;
            colorAdjustments.saturation.value = colorationSlider.value = gameSettings.coloration;
        }
        else
        {
            gameSettings.resolutionIndex = resolutionDropdown.value;
            gameSettings.isVsyncEnabled = vsyncToggle.isOn;
            gameSettings.isFullscreen = fullscreenToggle.isOn;
            gameSettings.masterVolume = masterVolumeSlider.value;
            gameSettings.musicVolume = musicVolumeSlider.value;
            gameSettings.sfxVolume = sfxVolumeSlider.value;
            gameSettings.brightness = brightnessSlider.value;
            gameSettings.contrast = contrastSlider.value; 
            gameSettings.coloration = colorationSlider.value;
        }
        audioMixer.SetFloat("Master", LinearToDecibel(masterVolumeSlider.value));
        audioMixer.SetFloat("Music",  LinearToDecibel(musicVolumeSlider.value));
        audioMixer.SetFloat("SFX",  LinearToDecibel(sfxVolumeSlider.value));
    }


    private float LinearToDecibel(float linear)
    {
        return linear != 0 ? 20.0f * Mathf.Log10(linear) : -80.0f;
    }
}
