﻿using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject creditsPopUp;

    public TextMeshProUGUI textHighScore;

    private void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Screen.SetResolution(1080, 1920, true);
        textHighScore.text = "High Score: " + PlayerPrefs.GetInt("highscore");
    }

    private void Update()
    {
    }

    public void loadScene()
    {
        SceneManager.LoadScene(1);
    }

    public void gameExit()
    {
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    public void creditsButton()
    {
        creditsPopUp.SetActive(true);
    }

    public void creditsButton(bool b)
    {
        creditsPopUp.SetActive(b);
    }
}