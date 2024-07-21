using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sceneswitch : MonoBehaviour
{
    public void Level1()
    {
        Invoke("Golevel", 12);
    }

    public void About()
    {
        Application.OpenURL("https://zherblast.com/");
    }

    public void Golevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Exitgame()
    {
        Application.Quit();
    }

    public void pausegame()
    {
        LevelManager.instance.IsPaused = true;
        LevelManager.instance.PausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void OnClickDeath()
    {
        LevelManager.instance.IsDead = true;
        LevelManager.instance.DeadPanel.SetActive(true);
    }

    public void resumegame()
    {
        LevelManager.instance.IsPaused = false;
        LevelManager.instance.PausePanel.SetActive(false);
        Time.timeScale = 1;
    }
}
