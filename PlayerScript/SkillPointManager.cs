using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEngine.EventSystems.EventTrigger;

public class SkillPointManager : Singleton<SkillPointManager>
{
    [SerializeField] private GameObject attackCollider;

    [SerializeField] private TMP_Text skillPointText;
    public int skillPointCount;

    [SerializeField] private TMP_Text damageAmountText;
    private DamageSource damageSource;

    [SerializeField] private TMP_Text healthAmountText;
    private PlayerHealth playerHealth;

    [SerializeField] private TMP_Text dashCDText;
    private Movement dash;

    int progressionAttack = 0;
    int progressionHP = 0;
    int progressionCD = 0;

    [SerializeField] private TMP_Text killCountText;
    public int killCount = 0;
    private void SaveGame()
    {
        PlayerPrefs.SetInt("savedEXP", skillPointCount);
        PlayerPrefs.SetFloat("savedHP", playerHealth.currentHealth);
        PlayerPrefs.SetInt("savedATK", damageSource.damageAmount);
        PlayerPrefs.SetFloat("savedCD", dash.dashCD);
        PlayerPrefs.Save();
        //PlayerPrefs.DeleteAll();
    }
    private void OnDisable()
    {
        SaveGame();
    }
    public void Start()
    {
        damageSource = attackCollider.GetComponent<DamageSource>();
        playerHealth = GetComponent<PlayerHealth>();
        dash = GetComponent<Movement>();

        if (PlayerPrefs.HasKey("savedEXP") && PlayerPrefs.HasKey("savedHP") && PlayerPrefs.HasKey("savedATK") && PlayerPrefs.HasKey("savedCD"))
        {
            skillPointCount = PlayerPrefs.GetInt("savedEXP");
            playerHealth.currentHealth = PlayerPrefs.GetFloat("savedHP");
            damageSource.damageAmount = PlayerPrefs.GetInt("savedATK");
            dash.dashCD = PlayerPrefs.GetFloat("savedCD");
        }
    }
    public void Update()
    {
        killCountText.text = killCount.ToString();
        skillPointText.text = skillPointCount.ToString();
        damageAmountText.text = damageSource.damageAmount.ToString();
        healthAmountText.text = playerHealth.maxHealth.ToString();
        dashCDText.text = dash.dashCD.ToString();
        SkillPointCheck();
        MaxHealthCheck();
        if (killCount >= 10)
        {
            killCountText.color = Color.white;
        }
    }
    public void SkillPointCheck()
    {
        if (skillPointCount <= 0)
        {
            skillPointCount = 0;
        }
    }
    public void MaxHealthCheck()
    {
        if (playerHealth.maxHealth <= playerHealth.currentHealth)
        {
            playerHealth.currentHealth = playerHealth.maxHealth;
        }
    }
    public void AddSkillPoint(int skillPoint)
    {
        skillPointCount += skillPoint;
        playerHealth.currentHealth += 5;
        killCount++;
    }
    public void BossDefeat(int skillPoint)
    {
        playerHealth.maxHealth += 10;
        damageSource.damageAmount += 5;
        skillPointCount += skillPoint;
        playerHealth.currentHealth = playerHealth.maxHealth;
        killCount++;
    }
    public void BuyAttack()
    {
        switch (progressionAttack)
        {
            case 0:
                if (skillPointCount >= 5)
                {
                    progressionAttack++;
                    skillPointCount -= 5;
                    damageSource.damageAmount += 2;
                }
                break;

            case 1:
                if (skillPointCount >= 10)
                {
                    progressionAttack++;
                    skillPointCount -= 10;
                    damageSource.damageAmount += 3;
                }
                break;

            case 2:
                if (skillPointCount >= 15)
                {
                    progressionAttack++;
                    skillPointCount -= 15;
                    damageSource.damageAmount += 4;
                }
                break;

            case 3:
                if (skillPointCount >= 20)
                {
                    progressionAttack++;
                    skillPointCount -= 20;
                    damageSource.damageAmount += 5;
                }
                break;

            case 4:
                if (skillPointCount >= 50)
                {
                    progressionAttack++;
                    skillPointCount -= 50;
                    damageSource.damageAmount += 10;
                }
                break;

            default: break;
        }

    }
    public void BuyHP()
    {
        switch (progressionHP)
        {
            case 0:
                if (skillPointCount >= 5)
                {
                    progressionHP++;
                    skillPointCount -= 5;
                    playerHealth.maxHealth += 5;
                }
                break;

            case 1:
                if (skillPointCount >= 10)
                {
                    progressionHP++;
                    skillPointCount -= 10;
                    playerHealth.maxHealth += 5;
                }
                break;

            case 2:
                if (skillPointCount >= 15)
                {
                    progressionHP++;
                    skillPointCount -= 15;
                    playerHealth.maxHealth += 10;
                }
                break;

            case 3:
                if (skillPointCount >= 20)
                {
                    progressionHP++;
                    skillPointCount -= 20;
                    playerHealth.maxHealth += 10;
                }
                break;

            case 4:
                if (skillPointCount >= 50)
                {
                    progressionHP++;
                    skillPointCount -= 50;
                    playerHealth.maxHealth += 20;
                }
                break;

            default: break;
        }
    }
    public void BuyDash()
    {
        switch (progressionCD)
        {
            case 0:
                if (skillPointCount >= 5)
                {
                    progressionCD++;
                    skillPointCount -= 5;
                    dash.dashCD -= 0.2f;
                }
                break;

            case 1:
                if (skillPointCount >= 10)
                {
                    progressionCD++;
                    skillPointCount -= 10;
                    dash.dashCD -= 0.3f;
                }
                break;

            case 2:
                if (skillPointCount >= 15)
                {
                    progressionCD++;
                    skillPointCount -= 15;
                    dash.dashCD -= 0.4f;
                }
                break;

            case 3:
                if (skillPointCount >= 20)
                {
                    progressionCD++;
                    skillPointCount -= 20;
                    dash.dashCD -= 0.5f;
                }
                break;

            case 4:
                if (skillPointCount >= 50)
                {
                    progressionCD++;
                    skillPointCount -= 50;
                    dash.dashCD -= 0.6f;
                }
                break;

            default : break;
        }
    }
}
