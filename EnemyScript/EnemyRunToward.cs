using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyRunToward : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private float speed;

    private void Start()
    {
        player = GameObject.Find("Player");
    }
    public void Update()
    {
        //gameObject.transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
}
