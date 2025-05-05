using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Shooter : MonoBehaviour
{
    [SerializeField] private State startingState;
    [SerializeField] private float roamingDistanceMax = 7f;
    [SerializeField] private float roamingDistanceMin = 3f;
    [SerializeField] private float roamingTimerMax = 2f;

    [SerializeField] private GameObject projectilePrefab;

    [SerializeField] private float attackRange = 5f;
    [SerializeField] private float attackCD = 2f;

    private bool canAttack = true;

    private NavMeshAgent navMeshAgent;
    private State state;
    private float roamingTime;
    private Vector3 roamPosition;
    private Vector3 startingPosition;
    private EnemyKnockback knockback;
    private enum State
    {
        Roaming,
        Attacking
    }
    private void Awake()
    {
        knockback = GetComponent<EnemyKnockback>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        state = startingState;
    }
    private void Update()
    {
        switch (state)
        {
            default:
            case State.Roaming:
                roamingTime -= Time.deltaTime;
                if (roamingTime < 0)
                {
                    Roaming();
                    roamingTime = roamingTimerMax;
                }
                break;
        }
    }
    private void FixedUpdate()
    {
        if (knockback.gettingKnockedBack)
        {
            return;
        }
    }
    private void Roaming()
    {
        startingPosition = transform.position;
        roamPosition = GetRoamingPosition();
        navMeshAgent.SetDestination(roamPosition);
        if (Vector2.Distance(transform.position, Movement.Instance.transform.position) > attackRange)
        {
            state = State.Roaming;
        }
        if (attackRange != 0 && canAttack)
        {
            canAttack = false;
            Attack();
            StartCoroutine(AttackCDRoutine());
        }
    }
    private void Attack()
    {
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);
    }
    private IEnumerator AttackCDRoutine()
    {
        yield return new WaitForSeconds(attackCD);
        canAttack = true;
    }
    //private void FlipSprite()
    //{
    //    if (direction.x == 1)
    //    {
    //        this.transform.rotation = Quaternion.Euler(0, 180, 0);
    //    }
    //    if (direction.x == -1)
    //    {
    //        this.transform.rotation = Quaternion.Euler(0, 0, 0);
    //    }
    //}
    //private void FlipSprite()
    //{
    //    facingRight = !facingRight;
    //    Vector3 Scaler = transform.localScale;
    //    Scaler.x = -1;
    //    transform.localScale = Scaler;
    //}

    private Vector3 GetRoamingPosition()
    {
        return startingPosition + SpecimenNew.Utils.Utils.GetRandomDir() * Random.Range(roamingDistanceMin, roamingDistanceMax);
    }
}
