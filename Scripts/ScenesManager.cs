using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    private Transform pauseMenu;
    private Transform confirmationWindow;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu" && SceneManager.GetActiveScene().name != "EndStageScene")
        {
            pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu").transform.GetChild(0);
            confirmationWindow = pauseMenu.transform.GetChild(3);
            //Cursor.visible = false; //CURSOR
            //Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SceneOne");
    }
    
    public void QuitGame()
    {
        if (Time.timeScale == 0) Time.timeScale = 1f;
        Application.Quit();
    }

    public void ConfirmationWidnow()
    {
        if (confirmationWindow.gameObject.activeSelf == true)
            confirmationWindow.gameObject.SetActive(false);
        else confirmationWindow.gameObject.SetActive(true);
    }

    public void MainMenu()
    {
        if (Time.timeScale == 0) Time.timeScale = 1f;

        GameManager.instance.moneyAmount += GameManager.instance.moneyCollectedInStage;
        GameManager.instance.moneyCollectedInStage = 0f;
        SaveLoad.instance.SaveGame(); //REMOVE SAVE GAME LATER! ONLY ON FINISH STAGE
        SceneManager.LoadScene("MainMenu");
    }

    public void ResumePause()
    {
        OnMenuOpen();
    }

    public void EndStageScene()
    {
        SceneManager.LoadScene("EndStageScene");
    }

    private void OnMenuOpen()
    {
        //Cursor.visible = true; //CURSOR
        if (confirmationWindow.gameObject.activeSelf == true)
        {
            confirmationWindow.gameObject.SetActive(false);
            return;
        }

        if (pauseMenu.gameObject.activeSelf == true)
        {
            pauseMenu.gameObject.SetActive(false);
            //Cursor.visible = false; //CURSOR
            Time.timeScale = 1f;
        }
        else
        {
            
            pauseMenu.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
