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
        SceneManager.LoadScene(1);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void Exitgame()
    {
        Application.Quit();
    }
}
