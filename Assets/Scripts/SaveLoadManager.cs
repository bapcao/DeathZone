using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance { get; set; }

    string hightScoreKey = "BestWaveSavedValue";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(this);
    }

    public void SaveHightScore(int score)
    {
        PlayerPrefs.SetInt(hightScoreKey, score);
    }

    public int LoadHightScore()
    {
        if (PlayerPrefs.HasKey(hightScoreKey))
        {
            return PlayerPrefs.GetInt(hightScoreKey);
        }
        else
        {
            return 0;
        }
    }

}
