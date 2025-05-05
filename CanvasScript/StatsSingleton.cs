using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatsSingleton : Singleton<StatsSingleton>
{
    [SerializeField] private GameObject bars;
    public void Update()
    {
        if (SceneManager.GetActiveScene().name == "MenuScene")
        {
            bars.SetActive(false);
        }
        else
        {
            bars.SetActive(true);
        }
    }
}
