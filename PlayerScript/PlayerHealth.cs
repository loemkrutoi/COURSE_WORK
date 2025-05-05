using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHealth : Singleton<PlayerHealth>
{
    [SerializeField] public float maxHealth = 50f;
    [SerializeField] private TMP_Text currentHealthText;
    [SerializeField] private float knockBackThrustAmount = 10f;
    [SerializeField] private float damageRecoveryTime = 1f;
    [SerializeField] private float restoreDefaultAnim = 0.2f;

    [SerializeField] private Image healthBar;
    [SerializeField] private Image activeIcon;

    [SerializeField] private Sprite healthIconHealthy;
    [SerializeField] private Sprite healthIconMildDamaged;
    [SerializeField] private Sprite healthIconModerateDamaged;
    [SerializeField] private Sprite healthIconSeverlyDamaged;
    [SerializeField] private Sprite healthIconBroken;

    private Animator animator;
    public float currentHealth;
    private bool canTakeDamage = true;
    private void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }
    public void Update()
    {
        currentHealthText.text = currentHealth.ToString();
        healthBar.fillAmount = currentHealth / maxHealth;
        HealthCheck();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        EnemyAINEW enemy = collision.gameObject.GetComponent<EnemyAINEW>();
        BossHealth bossHealth = collision.gameObject.GetComponent<BossHealth>();

        EnemyKnockback knockback = GetComponent<EnemyKnockback>();
        if (canTakeDamage)
        {
            if (enemy || bossHealth)
            {
                if (collision.gameObject.CompareTag("Boss"))
                {
                    TakeDamage(10);
                }
                else
                {
                    knockback.GetKnockedBack(enemy.transform, knockBackThrustAmount);
                    TakeDamage(5);
                }
                StartCoroutine(AnimChangePlayer());
            }
        }
    }
    private void HealthCheck()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            currentHealthText.color = Color.black;
        }
        if (healthBar.fillAmount >= 0.8)
        {
            activeIcon.sprite = healthIconHealthy;
            currentHealthText.color = Color.black;
        }
        if (healthBar.fillAmount < 0.8 && healthBar.fillAmount >= 0.6)
        {
            activeIcon.sprite = healthIconMildDamaged;
            currentHealthText.color = Color.black;
        }
        if (healthBar.fillAmount < 0.6 && healthBar.fillAmount >= 0.3)
        {
            activeIcon.sprite = healthIconModerateDamaged;
            currentHealthText.color = new Color(130 / 255f, 0f, 0f, 1f);
        }
        if (healthBar.fillAmount < 0.3 && healthBar.fillAmount >= 0.1)
        {
            activeIcon.sprite = healthIconSeverlyDamaged;
            currentHealthText.color = new Color(176 / 255f, 0f, 0f, 1f);
        }
        if (healthBar.fillAmount <= 0)
        {
            activeIcon.sprite = healthIconBroken;
            currentHealthText.color = new Color(205 / 255f, 0f, 0f, 1f);
        }
    }
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        canTakeDamage = false;
        StartCoroutine(DamageRecoveryRoutine());
    }
    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }
    private IEnumerator AnimChangePlayer()
    {
        animator.SetBool("GettingHit", true);
        yield return new WaitForSeconds(restoreDefaultAnim);
        animator.SetBool("GettingHit", false);
    }
}
