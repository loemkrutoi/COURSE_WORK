using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;

    [SerializeField] private TMP_Text musicBtnText;
    [SerializeField] private TMP_Text soundBtnText;
    public void Start()
    {
        Time.timeScale = 1;
    }
    public void LoadNewGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SceneFirstLocation");
    }
    public void LoadContinueGame()
    {
        //использовать playerprefs скорее всего каждой сцене давать номер через который в дальнейшем и запускать

        Time.timeScale = 1;
        SceneManager.LoadScene("SceneFirstLocation");
    }
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }
    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("quit");
    }
    public void OnOffMusic()
    {
        if (musicBtnText.text == "ВКЛ.")
        {
            musicBtnText.text = "ВЫКЛ.";
        }
        else if (musicBtnText.text == "ВЫКЛ.")
        {
            musicBtnText.text = "ВКЛ.";
        }
    }
    public void OnOffSound()
    {
        if (soundBtnText.text == "ВКЛ.")
        {
            soundBtnText.text = "ВЫКЛ.";
        }
        else if (soundBtnText.text == "ВЫКЛ.")
        {
            soundBtnText.text = "ВКЛ.";
        }
    }
}
