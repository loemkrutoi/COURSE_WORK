using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [SerializeField] public float maxHealth = 100f;
    public float currentHealth;

    [SerializeField] private TMP_Text currentHealthText;

    [SerializeField] private Image healthBar;

    [SerializeField] private GameObject healthBarWhole;

    [SerializeField] private GameObject deathVFXPrefab;
    [SerializeField] public GameObject particleEXPPrefab;
    [SerializeField] private float restoreDefaultAnim = 0.2f;
    [SerializeField] private GameObject hitVFXPrefab;

    private SpriteRenderer spriteRenderer;
    private GameObject player;

    private EnemyKnockback knockback;
    private Animator healthBarAnimator;
    private Animator animator;
    private void Awake()
    {
        knockback = GetComponent<EnemyKnockback>();
        animator = GetComponent<Animator>();
        healthBarAnimator = healthBarWhole.GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
    }
    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void Update()
    {
        currentHealthText.text = currentHealth.ToString();
        healthBar.fillAmount = currentHealth / maxHealth;
        spriteRenderer.flipX = player.transform.position.x > transform.position.x;
    }
    public void TakeDamage(int damage)
    {
        Instantiate(hitVFXPrefab, transform.position, Quaternion.identity);
        currentHealth -= damage;
        knockback.GetKnockedBack(Movement.Instance.transform, 15f);
        StartCoroutine(AnimChangeEnemy());
    }
    public void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            StartCoroutine(BossDeath());
        }
    }
    private IEnumerator AnimChangeEnemy()
    {
        animator.SetBool("GettingHit", true);
        yield return new WaitForSeconds(restoreDefaultAnim);
        animator.SetBool("GettingHit", false);
        DetectDeath();
    }
    private IEnumerator BossDeath()
    {
        animator.SetTrigger("DeathBoss");
        healthBarAnimator.SetTrigger("DeathHealthBar");
        yield return new WaitForSeconds(1);
        Destroy(healthBarWhole);
        Destroy(gameObject);
        Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
        Instantiate(particleEXPPrefab, transform.position, Quaternion.identity);
    }
}
