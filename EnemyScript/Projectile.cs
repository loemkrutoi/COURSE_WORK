using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 1.5f;
    private Rigidbody2D rbProjectile;

    private GameObject player;
    private PlayerHealth playerHealth;

    private bool isProjectileHit = false;

    [SerializeField] private GameObject hitVFXPrefab;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        rbProjectile = GetComponent<Rigidbody2D>();
    }
    public void Update()
    {
        if (isProjectileHit == false)
        {
            MoveProjectile();
        }
        else if (isProjectileHit == true)
        {
            ParryProjectile(player.transform, speed);
        }
    }
    private void MoveProjectile()
    {
        Physics2D.IgnoreLayerCollision(7, 8, true);
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, (speed * 3) * Time.deltaTime);
    }
    private void ParryProjectile(Transform damageSource, float knockbackThrust)
    {
        Physics2D.IgnoreLayerCollision(7, 8, false);
        Vector2 differrence = knockbackThrust * rbProjectile.mass * (transform.position - damageSource.position).normalized;
        rbProjectile.AddForce(differrence, ForceMode2D.Impulse);
    }
    private IEnumerator ProjectileLifespan()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerHealth.TakeDamage(7);
            Instantiate(hitVFXPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (isProjectileHit && collision.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(10);
            Destroy(gameObject);
        }
        if (isProjectileHit && collision.CompareTag("Boss"))
        {
            BossHealth bossHealth = collision.GetComponent<BossHealth>();
            bossHealth.TakeDamage(5);
            Destroy(gameObject);
        }
        if (collision.CompareTag("AttackCollider"))
        {
            isProjectileHit = true;
            StartCoroutine(ProjectileLifespan());
        }
    }
}
