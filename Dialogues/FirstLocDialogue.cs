using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System.Linq;

public class FirstLocDialogue : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private Movement movement;

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private GameObject choicePanel;

    [SerializeField] private TMP_Text nameNPC;
    [SerializeField] private Image IconNPC;

    [SerializeField] private string[] dialogue;
    private int index;

    [SerializeField] private float wordSpeed;
    private bool isPlayer;

    private bool isPickedUp = false;

    [SerializeField] private GameObject panelNewSkillAttack;
    [SerializeField] public GameObject panelNewSkillDash;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
    }
    public void Update()
    {
        DialogueCheck();
        HandToPickUp();
        if (isPickedUp)
        {
            Destroy(GameObject.FindWithTag("hand"));
        }
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
        foreach (char character in dialogue[index].ToCharArray())
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
    private void HandToPickUp()
    {
        if (dialogueText.text == "���������?")
        {
            choicePanel.SetActive(true);
        }
        else choicePanel.SetActive(false);
    }
    public void ChoiceHandYes()
    {
        if (isPickedUp == false)
        {
            playerInputActions.Player.Attack.started += _ => movement.AttackState();
            choicePanel.SetActive(false);
            panelNewSkillAttack.SetActive(true);
            NextSentence();
            Destroy(GameObject.FindWithTag("hand"));
            isPickedUp = true;
        }
    }
    public void ClosePanelNewSkill()
    {
        panelNewSkillAttack.SetActive(false);
        panelNewSkillDash.SetActive(false);
    }
    public void ChoiceHandNo()
    {
        dialoguePanel.SetActive(false);
        choicePanel.SetActive(false);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("capsule"))
        {
            isPlayer = true;
            dialogue[0] = "��� �������";
            dialogue[1] = "���������.";
        }
        if (collision.CompareTag("liquid"))
        {
            isPlayer = true;
            dialogue[0] = "��� ��";
            dialogue[1] = "�� �������?";
        }
        if (collision.CompareTag("glitch"))
        {
            isPlayer = true;
            dialogue[0] = "���";
            dialogue[1] = "�������, ���� ������.";
        }
        if (collision.CompareTag("liquidRoom"))
        {
            isPlayer = true;
            dialogue[0] = "��� ����";
            dialogue[1] = "� �� �������?";
        }
        if (collision.CompareTag("hand"))
        {
            isPlayer = true;
            dialogue[0] = "���-�� ����";
            dialogue[1] = "���������?";
        }
        if (collision.CompareTag("toDestroy"))
        {
            isPlayer = true;
            dialogue[0] = "������";
            dialogue[1] = "���� ����������.";
        }
        if (collision.CompareTag("prison"))
        {
            isPlayer = true;
            dialogue[0] = "���";
            dialogue[1] = "��� ����� ���-�� ����";
        }
        if (collision.CompareTag("Gzhechka"))
        {
            isPlayer = false;
            NullText();
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        isPlayer = false;
        NullText();
    }
}
