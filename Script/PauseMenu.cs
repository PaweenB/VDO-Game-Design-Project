using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu, settingMenu, tutorial1, tutorial2;
    public bool isPaused, isSetting , isTutorial;

    void Start()
    {
        Time.timeScale = 0f;
        tutorial1.SetActive(true);
        tutorial2.SetActive(false);
        pauseMenu.SetActive(false);
        settingMenu.SetActive(false);
        isTutorial = true;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused && !isSetting  && !isTutorial)
            {
                ConGame();
                Cursor.visible = false;
            }
            else if(isPaused && isSetting && !isTutorial)
            {
                Back();
                Cursor.visible = false;
            }
            else if(!isPaused && !isTutorial)
            {
                PauseGame();
                Cursor.visible = true;
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ConGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
        Time.timeScale = 1f;
    }

    public void Setting()
    {
        settingMenu.SetActive(true);
        isSetting = true;
    }

    public void Back()
    {
        settingMenu.SetActive(false);
        isSetting = false;
    }

    public void Next()
    {
        tutorial1.SetActive(false);
        tutorial2.SetActive(true);
    }

    public void OK()
    {
        tutorial2.SetActive(false);
        Time.timeScale = 1f;
        isTutorial = false;
    }

}
