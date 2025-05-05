using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToDestroy : MonoBehaviour
{
    [SerializeField] private GameObject deathVFXPrefab;
    [SerializeField] private GameObject hitVFXPrefab;

    public void DestroyObject()
    {
        Instantiate(hitVFXPrefab, transform.position, Quaternion.identity);
        Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
