using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour
{
    public void QuitGameOnClick()
    {
        Application.Quit();
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
