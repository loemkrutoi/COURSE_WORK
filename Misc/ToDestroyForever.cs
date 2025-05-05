using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToDestroyForever : MonoBehaviour
{
    [SerializeField] private GameObject deathVFXPrefab;
    [SerializeField] private GameObject hitVFXPrefab;
    [SerializeField] private GameObject gzhechka;

    private GameObject player;
    private Movement movement;

    private void Start()
    {
        player = GameObject.Find("Player");
        movement = player.GetComponent<Movement>();
    }

    private void Update()
    {
        if (movement.hasDestroyedPrison == true)
        {
            Destroy(GameObject.FindWithTag("prison"));
            Instantiate(gzhechka, transform.position, Quaternion.identity);
        }
    }
    public void DestroyObject()
    {
        if (movement.hasDestroyedPrison == false)
        {
            Instantiate(hitVFXPrefab, transform.position, Quaternion.identity);
            Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
            Instantiate(gzhechka, transform.position, Quaternion.identity);
            Destroy(GameObject.FindWithTag("prison"));
            movement.hasDestroyedPrison = true;
        }
    }
}
