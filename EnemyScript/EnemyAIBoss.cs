//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.AI;
//using SpecimenNew.Utils;
//using UnityEngine.Diagnostics;

//public class EnemyAIBoss : MonoBehaviour
//{
//    [SerializeField] private State startingState;
//    [SerializeField] private float roamingDistanceMax = 7f;
//    [SerializeField] private float roamingDistanceMin = 3f;
//    [SerializeField] private float roamingTimerMax = 2f;

//    private NavMeshAgent navMeshAgent;
//    private State state;
//    private float roamingTime;
//    private Vector3 roamPosition;
//    private Vector3 startingPosition;
//    private EnemyKnockback knockback;

//    private enum State
//    {
//        Roaming
//    }

//    private void Awake()
//    {
//        knockback = GetComponent<EnemyKnockback>();
//        navMeshAgent = GetComponent<NavMeshAgent>();
//        navMeshAgent.updateRotation = false;
//        navMeshAgent.updateUpAxis = false;
//        state = startingState;
//    }

//    private void Update()
//    {
//        switch (state)
//        {
//            default:
//            case State.Roaming:
//                roamingTime -= Time.deltaTime;
//                if (roamingTime < 0)
//                {
//                    Roaming();
//                    roamingTime = roamingTimerMax;
//                }
//                break;
//        }
//    }
//    private void FixedUpdate()
//    {
//        if (knockback.gettingKnockedBack)
//        {
//            return;
//        }
//    }

//    private void Roaming()
//    {
//        startingPosition = transform.position;
//        roamPosition = GetRoamingPosition();
//        navMeshAgent.SetDestination(roamPosition);
//    }
//    private Vector3 GetRoamingPosition()
//    {
//        return startingPosition + SpecimenNew.Utils.Utils.GetRandomDir() * Random.Range(roamingDistanceMin, roamingDistanceMax);
//    }

//}
