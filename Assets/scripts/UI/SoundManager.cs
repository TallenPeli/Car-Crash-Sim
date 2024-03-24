using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject SettingsManager;
    public AudioListener MasterAudio;
    public Slider MasterVolume;

    public void ChangeMasterVolume()
    {
        AudioListener.volume = MasterVolume.value/100;
        SettingsManager.GetComponent<SettingsManager>().gameSettings.audioSettings.masterVolume = MasterVolume.value;
    }
}
