using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject[] powerups;
    [SerializeField]
    private GameObject enemyContainer;

    private bool stopSpawning = false;
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());

        StartCoroutine(SpawnPowerUpRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (stopSpawning == false)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, new Vector3(Random.Range(-9f, 9f), 7f, 0), Quaternion.identity);
            newEnemy.transform.parent = enemyContainer.transform;
            yield return new WaitForSeconds(Random.Range(3f, 5f));
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while(stopSpawning == false)
        {
            yield return new WaitForSeconds(Random.Range(9f, 11f));
            GameObject newPowerup = Instantiate(powerups[Random.Range(0,3)], new Vector3(Random.Range(-9f, 9f), 7f, 0), Quaternion.identity);
        }
    }

    public void OnPlayerDeath()
    {
        stopSpawning = true;
    }
}
