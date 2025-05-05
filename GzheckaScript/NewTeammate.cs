//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class NewTeammate : MonoBehaviour
//{
//    public GameObject teammate;
//    private Follower follower;
//    //private Animator animator;
//    //private int count = 0;

//    private void Awake()
//    {
//        //animator = GetComponent<Animator>();
//        follower = GetComponent<Follower>();
//    }
//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        // i dont want to JOIN your team

//        //count++;
//        //if (count >= 10) {
//        //    animator.SetBool("IsAnnoyed", true);
//        //}
//        if (collision.gameObject.tag == "Player")
//        {
//            if (!teammate.gameObject.GetComponent<Follower>())
//            {
//                // okay i guess
//                teammate.gameObject.AddComponent<Follower>();
//                Destroy(teammate.gameObject.GetComponent<NewTeammate>());
//            }
//        }
//    }
//}
