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
        //������������ playerprefs ������ ����� ������ ����� ������ ����� ����� ������� � ���������� � ���������

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
        if (musicBtnText.text == "���.")
        {
            musicBtnText.text = "����.";
        }
        else if (musicBtnText.text == "����.")
        {
            musicBtnText.text = "���.";
        }
    }
    public void OnOffSound()
    {
        if (soundBtnText.text == "���.")
        {
            soundBtnText.text = "����.";
        }
        else if (soundBtnText.text == "����.")
        {
            soundBtnText.text = "���.";
        }
    }
}
