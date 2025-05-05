using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] public int damageAmount = 1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<EnemyHealth>())
        {
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(damageAmount);
        }
        if (other.gameObject.GetComponent<BossHealth>())
        {
            BossHealth bossHealth = other.gameObject.GetComponent<BossHealth>();
            bossHealth.TakeDamage(damageAmount);
        }
        if (other.gameObject.GetComponent<ToDestroy>())
        {
            ToDestroy toDestroy = other.gameObject.GetComponent<ToDestroy>();
            toDestroy.DestroyObject();
        }
        if (other.gameObject.GetComponent<ToDestroyForever>())
        {
            ToDestroyForever toDestroyForever = other.gameObject.GetComponent<ToDestroyForever>();
            toDestroyForever.DestroyObject();
        }
    }
}
