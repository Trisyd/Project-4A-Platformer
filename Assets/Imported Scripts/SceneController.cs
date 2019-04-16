using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    //This script handles all scene changes, and maintains the mainCamera as well as itself

    public static SceneController Instance;

    private Text hstext;

    public GameObject mainCamera;
    public AudioClip masterAudioClip;

    public void Awake() { Instance = this; }

    void Start()
    {
        if (!PlayerPrefs.HasKey("Master Volume")) { PlayerPrefs.SetFloat("Master Volume", 1f); }
        if (!PlayerPrefs.HasKey("SFX Volume")) { PlayerPrefs.SetFloat("SFX Volume", 1f); }
        DontDestroyOnLoad(this.gameObject);
        //DontDestroyOnLoad(mainCamera);
    }

    public void LoadMenu() { SceneManager.LoadScene("Menu"); }

    public void LoadGame() { SceneManager.LoadScene("Primary"); }

    public void CloseGame() { Application.Quit(); }
}
