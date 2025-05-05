using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PickUp : MonoBehaviour
{   
    private GameObject player;
    [SerializeField] private float speed = 0.1f;
    private SkillPointManager skillPointManager;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        skillPointManager = player.GetComponent<SkillPointManager>();
    }
    public void Update()
    {
        speed += Time.deltaTime * 6f;
        this.transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CompareTag("SkillOrb") && collision.CompareTag("Player"))
        {
            skillPointManager.AddSkillPoint(2);
            Destroy(gameObject);
        }
        if (CompareTag("skillOrbBoss") && collision.CompareTag("Player"))
        {
            skillPointManager.BossDefeat(10);
            Destroy(gameObject);
        }
    }
}
