using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;
    [SerializeField] private GameObject deathVFXPrefab;
    [SerializeField] private float restoreDefaultAnim = 0.2f;

    private EnemyKnockback knockback;
    private int currentHealth;
    private Animator animator;
    private EnemyHealth enemyHealth;
    //private Flash flash;
    private void Awake()
    {
        //flash = GetComponent<Flash>();
        knockback = GetComponent<EnemyKnockback>();
        animator = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
    }
    private void Start()
    {
        currentHealth = startingHealth;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        knockback.GetKnockedBack(Movement.Instance.transform, 15f);
        StartCoroutine(AnimChangeEnemy());
        //StartCoroutine(flash.FlashRoutine());

    }
    public void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            Instantiate(deathVFXPrefab,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }
    private IEnumerator AnimChangeEnemy()
    {
        animator.SetBool("GettingHit",true);
        yield return new WaitForSeconds(restoreDefaultAnim);
        animator.SetBool("GettingHit",false);
        enemyHealth.DetectDeath();
    }
}
