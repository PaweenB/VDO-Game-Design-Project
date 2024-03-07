using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingMenu;

    void Start()
    {
        settingMenu.SetActive(false);
    }
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SettingGame()
    {
        settingMenu.SetActive(true);
    }

    public void Back()
    {
        settingMenu.SetActive(false);
    }
}
