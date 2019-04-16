using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsManager : MonoBehaviour
{
    /// <summary>
    /// This Script is attached to a specific audio manager object in the "Menu" scene of the game. 
    /// It deals directly with any changes made the sliders, and saves those changes using player prefs
    /// It maintains all volumes so they are accurately loaded at whatever the player decided
    /// </summary>

    public GameObject neededSceneControllerObject;
    public AudioSource masterAudioSource;

    [SerializeField] Slider masterSlider;
    [SerializeField] Slider vfxSlider;

    // Start is called before the first frame update
    void Start()
    {
        neededSceneControllerObject = GameObject.FindGameObjectWithTag("GameController");
        masterAudioSource = neededSceneControllerObject.GetComponent<AudioSource>();

        if (PlayerPrefs.HasKey("Master Volume")) { masterSlider.value = PlayerPrefs.GetFloat("Master Volume"); }
        else { masterSlider.value = 1; }
        if (PlayerPrefs.HasKey("SFX Volume")) { vfxSlider.value = PlayerPrefs.GetFloat("SFX Volume"); }
        else { vfxSlider.value = 1; }
    }

    public void SetMasterVolume()
    {
        float masterVolume = masterSlider.value;
        PlayerPrefs.SetFloat("Master Volume", masterVolume);
        masterAudioSource.volume = masterVolume;
        Debug.Log("Master Volume has changed to:" + masterVolume);
    }

    public void SetSFXVolume()
    {
        float sfxVolume = vfxSlider.value;
        PlayerPrefs.SetFloat("SFX Volume", sfxVolume);
        Debug.Log("SFX Volume has changed to:" + sfxVolume);
    }
}
