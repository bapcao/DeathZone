using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TMP_Text highScoreUI;

    string newGameScene = "Map_1";

    public AudioClip bg_music;
    public AudioSource main_channel;


    void Start()
    {
        main_channel.PlayOneShot(bg_music);
        
        // Set the hight score text
        int highScore = SaveLoadManager.Instance.LoadHightScore();
        highScoreUI.text = $"Top Wave Survived: {highScore}";
    }

   
    public void Sinhton()
    {
        main_channel.Stop();
        SceneManager.LoadScene("Map");
    }
    public void De()
    {
        main_channel.Stop();
        SceneManager.LoadScene(newGameScene);
    }

    public void Trungbinh()
    {
        main_channel.Stop();
        SceneManager.LoadScene("");
    }

    public void ExitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;    
#else
    
#endif        
    }
}
