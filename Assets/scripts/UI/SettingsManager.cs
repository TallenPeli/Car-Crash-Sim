using UnityEngine;
using System.IO;

public class SettingsManager : MonoBehaviour {

    public string filePath;
    public Settings gameSettings;

    [System.Serializable]
    public class Settings
    {
        public CameraSettings cameraSettings;
        public GraphicsSettings graphicsSettings;
    }

    [System.Serializable]
    public class CameraSettings
    {
        public int FOV = 90;
    }

    [System.Serializable]
    public class GraphicsSettings
    {
        public int graphicsQuality = 2;
        public int ShadowQuality = 1;
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
        File.WriteAllText(filePath, JsonUtility.ToJson(gameSettings, true));
    }
    void Start()
    {
        filePath = Path.Combine(Application.dataPath, "Assets/Configurations/settings.json");

        // Create an instance of the Settings class

        if(File.Exists(filePath))
        {
            LoadSettings();
        }
        else
        {
            SaveSettings();
        }
    }
}