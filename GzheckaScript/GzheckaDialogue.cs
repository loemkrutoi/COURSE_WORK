using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GzheckaDialogue : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    public Movement movement;
    public FirstLocDialogue firstLocDialogue;
    private SkillPointManager skillPointManager;
    private GameObject canvasScriptListObject;
    private CanvasScriptList canvasScriptList;

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private GameObject choicePanel;

    [SerializeField] private TMP_Text nameNPC;
    [SerializeField] private Image IconNPC;

    [SerializeField] private Sprite IconCalm;
    [SerializeField] private Sprite IconHappy;
    [SerializeField] private Sprite IconTired;
    [SerializeField] private Sprite IconAngry;
    [SerializeField] private Sprite IconSad;

    [SerializeField] private string[] dialogue;
    private int index;

    private bool canDash = false;
    
    [SerializeField] private float wordSpeed;
    private bool isPlayer;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        canvasScriptListObject = GameObject.Find("ScreenManager");
        canvasScriptList = canvasScriptListObject.GetComponent<CanvasScriptList>();


        if (collision.CompareTag("Player"))
        {
            movement = collision.GetComponent<Movement>();
            firstLocDialogue = collision.GetComponent<FirstLocDialogue>();
            skillPointManager = collision.GetComponent<SkillPointManager>();

            isPlayer = true;

            if (skillPointManager.killCount >= 10 && canDash == false)
            {
                dialogue[0] = "?";
                dialogue[1] = "здравствуй";
                dialogue[2] = "спасибо, что меня освободил";
                dialogue[3] = "кажется, ты уже неплохо умеешь сражаться";
                dialogue[4] = "хочешь научу тебя уворачиваться от врагов?";
            }
            else if (skillPointManager.killCount < 10 && canDash == false)
            {
                dialogue[0] = "?";
                dialogue[1] = "здравствуй";
                dialogue[2] = "спасибо, что меня освободил";
                dialogue[3] = "меня зовут *имя*";
                dialogue[4] = "а ты кто?";
                dialogue[5] = "кажется, ты еще не очень хорош в сражениях";
                dialogue[6] = "возвращайся ко мне когда будешь немного сильнее";
            }
            else if (canDash)
            {
                dialogue[0] = "и снова здравствуй";
                dialogue[1] = "надеюсь тебе пригодился рывок";
                dialogue[2] = "?";
                dialogue[3] = "ты хочешь еще чему-нибудь научиться?";
                dialogue[4] = "извини, давай как-нибудь в следующий раз";
                dialogue[5] = "мы с тобой еще точно встретимся";
                dialogue[6] = "пока";
            }
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayer = false;
            NullText();
        }
    }
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
    }
    public void Update()
    {
        DialogueCheck();
        ChangeNPCState();
        ChangeNPCSprite();
    }
    public void ChangeNPCSprite()
    {
        if (dialogueText.text == dialogue[0] && canDash)
        {
            IconNPC.sprite = IconHappy;
        }
        if (dialogueText.text == dialogue[2] && !canDash) { 
            IconNPC.sprite = IconHappy;
        }
        else
        {
            IconNPC.sprite = IconCalm;
        }
    }
    public void ChangeNPCState()
    {
        if (dialogueText.text == dialogue[3])
        {
            nameNPC.text = "имя";
        }
        if (dialogueText.text == dialogue[4])
        {
            if (canDash == false && skillPointManager.killCount >= 10)
            {
                choicePanel.SetActive(true);
                canvasScriptList.tasksEmptyText.SetActive(true);
                canvasScriptList.task.SetActive(false);
            }
        }
        if (dialogueText.text == dialogue[6] && skillPointManager.killCount < 10)
        {
            choicePanel.SetActive(false);
            canvasScriptList.messagePanel.SetActive(true);
            canvasScriptList.messageG.SetActive(true);
            canvasScriptList.tasksEmptyText.SetActive(false);
            canvasScriptList.task.SetActive(true);
        }
    }
    public void ChoiceNPCYes()
    {
        IconNPC.sprite = IconHappy;

        dialogue[5] = "хорошо, чтобы использовать новый навык, жми ПКМ. Рывок делает тебя временно неуязвимым и атакует врагов позади";
        dialogue[6] = "но будь осторожен, рывок также требует 1 единицу здоровья. Удачи тебе";

        canvasScriptList.tasksEmptyText.SetActive(true);
        canvasScriptList.task.SetActive(false);
        choicePanel.SetActive(false);
        NextSentence();
        firstLocDialogue.panelNewSkillDash.gameObject.SetActive(true);

        playerInputActions.Player.Dash.started += _ => movement.DashState();
        canDash = true;
    }
    public void ChoiceNPCNo()
    {
        dialogue[5] = "как скажешь";
        dialogue[6] = "ты всегда можешь вернуться, чтобы научиться";
        choicePanel.SetActive(false);
        NextSentence();
    }
    public void DialogueCheck()
    {
        if (Input.GetKeyDown(KeyCode.F) && isPlayer)
        {
            if (dialoguePanel.activeInHierarchy)
            {
                NextSentence();
            }
            else
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(DialogueType());
            }
        }
    }
    public void NullText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }
    private IEnumerator DialogueType()
    {
        foreach(char character in dialogue[index].ToCharArray())
        {
            dialogueText.text += character;
            yield return new WaitForSeconds(wordSpeed);
        }
    }
    public void NextSentence()
    {
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(DialogueType());
        }
        else
        {
            NullText();
        }
    }
}
