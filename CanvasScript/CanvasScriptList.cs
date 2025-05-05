using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class CanvasScriptList : Singleton<CanvasScriptList>
{
    public GameObject canvasPause;
    public GameObject canvasSkill;
    public GameObject messagePanel;
    public GameObject tasksPanel;

    public GameObject messageG;
    public GameObject messageF;
    public GameObject messageC;

    public GameObject tasksEmptyText;
    public GameObject task;

    private bool isShowing;
    private bool isShowingSkill;
    private bool isShowingTasks;

    private GameObject player;
    private SkillPointManager skillPointManager;

    private int index = 0;
    private void Start()
    {
        player = GameObject.Find("Player");
        skillPointManager = player.GetComponent<SkillPointManager>();
    }
    private void Update()
    {
        PauseCanvasCheck();
        SkillCanvasCheck();
        MessagePanelCheck();
    }
    public void SkillCanvasCheck()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            messageC.SetActive(false);
            messagePanel.SetActive(false);
            if (!isShowingSkill)
            {
                Time.timeScale = 0;
            }
            if (isShowingSkill)
            {
                Time.timeScale = 1;
            }
            isShowingSkill = !isShowingSkill;
            canvasSkill.SetActive(isShowingSkill);
        }
    }
    public void PauseCanvasCheck()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isShowing)
            {
                Time.timeScale = 0;
            }
            if (isShowing)
            {
                Time.timeScale = 1;
            }
            isShowing = !isShowing;
            canvasPause.SetActive(isShowing);
        }
    }
    public void ExitPlayingButton()
    {
        canvasPause.SetActive(false);
        SceneManager.LoadScene("MenuScene");
    }
    public void ContinuePlayingButton()
    {
        Time.timeScale = 1;
        canvasPause.SetActive(false);
    }
    public void MessagePanelCheck()
    {
        if (messageF.activeSelf && Input.GetKeyDown(KeyCode.F))
        {
            messagePanel.SetActive(false);
            messageF.SetActive(false);
        }
        if (messageG.activeSelf && Input.GetKeyDown(KeyCode.G))
        {
            messagePanel.SetActive(false);
            messageG.SetActive(false);
            tasksPanel.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (!isShowingTasks)
            {
                Time.timeScale = 0;
            }
            if (isShowingTasks)
            {
                Time.timeScale = 1;
            }
            isShowingTasks = !isShowingTasks;
            tasksPanel.SetActive(isShowingTasks);
        }
        if (index == 0 && skillPointManager.skillPointCount > 0)
        {
            index++;
            messagePanel.SetActive(true);
            messageC.SetActive(true);
        }
    }
    //public void CloseMessagePanel()
    //{
    //    messagePanel.SetActive(false);
    //}
}
