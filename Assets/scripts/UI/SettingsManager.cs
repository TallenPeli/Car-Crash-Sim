using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

public class SettingsManager : MonoBehaviour {

    public string filePath;
    public Settings gameSettings;
    public Camera gameCamera;
    public GameObject UIHandler;
    public GameObject GameHandler;
    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    public int currentResolutionIndex;
    string operatingSystem = System.Environment.OSVersion.Platform.ToString();

    [System.Serializable]
    public class Settings
    {
        public CameraSettings cameraSettings;
        public GraphicsSettings graphicsSettings;
        public SystemSettings systemSettings;
        public AudioSettings audioSettings;
    }

    [System.Serializable]
    public class CameraSettings
    {
        public float FOV = 90f;
    }

    [System.Serializable]
    public class SystemSettings
    {
        public string operatingSystem = "";
    }

    [System.Serializable]
    public class GraphicsSettings
    {
        public int graphicsQuality = 2;
        public int shadowQuality = 1;
        public bool HDR_Enabled = false;
        public string resolution;
    }

    [System.Serializable]
    public class AudioSettings
    {
        public float masterVolume = 100f;
        public float menuVolume = 100f;
        public float engineVolume = 100f;
        public float physicsVolume = 100f;
    }

    public void LoadSettings()
    {
        string jsonText = File.ReadAllText(filePath);

        // Deserialize the JSON text into a Settings object
        gameSettings = JsonUtility.FromJson<Settings>(jsonText);

        Debug.Log("Found settings.json at : " + filePath);
    }

    public void SaveSettings()
    {
        try
        {
            File.WriteAllText(filePath, JsonUtility.ToJson(gameSettings, true));
            StartCoroutine(UIHandler.GetComponent<UIHandler>().ShowStatusMessage($"Settings saved to '{filePath}' Successfully!"));
        }
        catch(Exception e)
        {
            Debug.LogError("Error writing settings file: " + e.Message);
            StartCoroutine(UIHandler.GetComponent<UIHandler>().ShowErrorMessage($"Failed to save settings file to '{filePath}'"));
        }
    }
    public void FovChange()
    {
        gameCamera.fieldOfView = UIHandler.GetComponent<UIHandler>().FOV;
        gameSettings.cameraSettings.FOV = gameCamera.fieldOfView;
    }

    public void ToggleHDR()
    {
        gameSettings.graphicsSettings.HDR_Enabled = !gameSettings.graphicsSettings.HDR_Enabled;
        gameCamera.allowHDR = gameSettings.graphicsSettings.HDR_Enabled;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        gameSettings.graphicsSettings.resolution = resolution.width + " x " + resolution.height;
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    void Start()
    {
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        resolutions = Screen.resolutions;
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
                gameSettings.graphicsSettings.resolution = options[i];
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();

        resolutionDropdown.value = currentResolutionIndex;

        gameSettings.systemSettings.operatingSystem = operatingSystem;
        
        if (operatingSystem.Contains("Linux") || operatingSystem.Contains("Unix"))
        {
            // Linux file path
            string username = System.Environment.GetEnvironmentVariable("USER");
            filePath = $"/home/{username}/.car-sim/Configurations/settings.json";

            if(File.Exists(filePath))
            {
                LoadSettings();
            }
            else
            {
                SaveSettings();
            }
        }
        else if (operatingSystem.Contains("Win"))
        {
            // Windows file path
            string userProfilePath = System.Environment.GetEnvironmentVariable("USERPROFILE");
            filePath = $@"{userProfilePath}\AppData\LocalLow\car-sim\Configurations\settings.json";

            if(File.Exists(filePath))
            {
                LoadSettings();
            }
            else
            {
                SaveSettings();
            }
        }
        else if (operatingSystem.Contains("Mac"))
        {
            // macOS file path
            string username = System.Environment.GetEnvironmentVariable("USER");
            filePath = $"/Users/{username}/Library/Application Support/car-sim/Configurations/settings.json";
            if(File.Exists(filePath))
            {
                LoadSettings();
            }
            else
            {
                SaveSettings();
            }
        }
        else
        {
            // Default path if the operating system is not recognized
            Debug.LogWarning("Operating system not recognized. Using default file path.");
            filePath = "d/home/{username}/.car-sim/Configurations/settings.json";
        }

        string directoryPath = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
    }
}