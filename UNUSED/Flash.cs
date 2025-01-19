//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Flash : MonoBehaviour
//{
//    [SerializeField] private Material flashMaterial;
//    [SerializeField] private float restoreDefaultMaterial = 0.2f;

//    private Material defaultMaterial;
//    private SpriteRenderer spriteRenderer;
//    private EnemyHealth enemyHealth;

//    private void Awake()
//    {
//        enemyHealth = GetComponent<EnemyHealth>();
//        spriteRenderer = GetComponent<SpriteRenderer>();
//        defaultMaterial = spriteRenderer.material;
//    }
//    public IEnumerator FlashRoutine()
//    {
//        //spriteRenderer.material = flashMaterial;
//        yield return new WaitForSeconds(restoreDefaultMaterial);
//        //spriteRenderer.material = defaultMaterial;
//        enemyHealth.DetectDeath();
//    }
//}
