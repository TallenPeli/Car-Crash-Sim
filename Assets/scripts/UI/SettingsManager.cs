using UnityEngine;
using System.Collections;
using System.IO;
using System;
using TMPro;

public class SettingsManager : MonoBehaviour {

    public string filePath;
    public Settings gameSettings;
    public Camera gameCamera;
    public GameObject UIHandler;

    [System.Serializable]
    public class Settings
    {
        public CameraSettings cameraSettings;
        public GraphicsSettings graphicsSettings;
    }

    [System.Serializable]
    public class CameraSettings
    {
        public float FOV = 90f;
    }

    [System.Serializable]
    public class GraphicsSettings
    {
        public int graphicsQuality = 2;
        public int ShadowQuality = 1;
        public bool HDR_Enabled = false;
        
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
        }
        catch(Exception e)
        {
            Debug.LogError("Error writing settings file: " + e.Message);
            StartCoroutine(UIHandler.GetComponent<UIHandler>().ShowErrorMessage("Error. Could not save settings file."));
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
    void Start()
    {
        filePath = Path.Combine(Application.dataPath, "Configurations/settings.json");

        // Create an instance of the Settings class

    }
}