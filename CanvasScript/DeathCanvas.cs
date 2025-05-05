using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathCanvas : MonoBehaviour
{
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private GameObject playerDeathVFXPrefab;
    private GameObject player;
    private SpriteRenderer playerSprite;
    private PlayerHealth playerHealth;
    private Movement playerMovement;

    private int i = 0;
    private void Start()
    {
        player = GameObject.Find("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerSprite = player.GetComponent<SpriteRenderer>();
        playerMovement = player.GetComponent<Movement>();
    }
    public void Update()
    {
        if (i <=  0 && playerHealth.currentHealth <= 0)
        {
            StartCoroutine(DeathPlayerRoutine());
        }
    }
    private IEnumerator DeathPlayerRoutine()
    {
        playerSprite.sortingOrder = -10000;
        Instantiate(playerDeathVFXPrefab, player.transform.position, Quaternion.identity);
        i++;
        yield return new WaitForSeconds(1.5f);
        deathPanel.SetActive(true);
    }
    public void RestartGame()
    {
        //playerMovement.movingSpeed = 4f;
        deathPanel.SetActive(false);
        playerSprite.sortingOrder = 0;
        playerHealth.currentHealth = playerHealth.maxHealth;
        Time.timeScale = 1;
        player.transform.position = new Vector3 (0, 0, 0);
        SceneManager.LoadScene("SceneFirstLocation");
        i = 0;
    }
}
