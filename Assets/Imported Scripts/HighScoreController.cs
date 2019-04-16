using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreController : MonoBehaviour
{
    [SerializeField] Text hstext;

    // Start is called before the first frame update
    void Start()
    {
        //high scores will be printed when the "GameOver" scene is loaded. High Scores are derived from player prefs
        //See "FallApple.cs/ManageHighScores" function for actual handling of high scores within player prefs

        int indexer = 1;
        List<int> hslist = new List<int>();
        while (indexer < 11)
        {
            if(PlayerPrefs.HasKey("HighScore" + indexer))
            {
                hslist.Add(PlayerPrefs.GetInt("HighScore" + indexer));
                indexer += 1;
            }
            else { break; }
        }

        foreach (int score in hslist) { hstext.text += score + "\r\n"; }
    }
}
