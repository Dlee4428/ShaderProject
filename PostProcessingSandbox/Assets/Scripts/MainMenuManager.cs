using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField]public string mainMenu = "MainMenu";
    [SerializeField]public string SampleScene = "SampleScene";
    public void PlayGame()
    {
        SceneManager.LoadScene(SampleScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
