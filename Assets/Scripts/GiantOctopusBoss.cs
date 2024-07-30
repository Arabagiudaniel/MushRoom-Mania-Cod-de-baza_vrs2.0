using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GiantOctopusBoss : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public GameObject[] enemyPrefabs = new GameObject[3];

    [Header("Spawn Points")]
    public Transform[] attackSpawnPoints;

    [Header("Attack Cooldown")]
    public float timeBetweenWaves = 5f;

    private int currentWave = 0;
    private int remainingEnemies = 0;
    private bool isAttacking = false;

    void Start()
    {
        StartCoroutine(StartNextWave());
    }

    IEnumerator StartNextWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);

        if (currentWave < enemyPrefabs.Length)
        {
            isAttacking = true;
            SpawnEnemiesForCurrentWave();
        }
    }

    void SpawnEnemiesForCurrentWave()
    {
        if (currentWave < enemyPrefabs.Length)
        {
            GameObject enemyPrefab = enemyPrefabs[currentWave];
            remainingEnemies = 3;

            for (int i = 0; i < 3; i++)
            {
                if (attackSpawnPoints.Length == 0)
                {
                    Debug.LogError("No spawn points defined for attacks!");
                    return;
                }

                int randomIndex = Random.Range(0, attackSpawnPoints.Length);

                if (enemyPrefab != null)
                {
                    GameObject enemy = Instantiate(enemyPrefab, attackSpawnPoints[randomIndex].position, Quaternion.identity);
                    enemy.SetActive(true);

                    Enemy enemyScript = enemy.GetComponent<Enemy>();
                    if (enemyScript != null)
                    {
                        enemyScript.giantOctopusBoss = this;
                    }
                    else
                    {
                        Debug.LogError("Spawned enemy does not have an Enemy script!");
                    }
                }
            }

            currentWave++;
        }
    }

    public void EnemyDestroyed()
    {
        remainingEnemies--;
        Debug.Log("Enemy destroyed. Remaining enemies: " + remainingEnemies);

        if (remainingEnemies <= 0)
        {
            if (currentWave >= enemyPrefabs.Length)
            {
                Debug.Log("All waves completed and all enemies destroyed!");
                DestroyBoss(); // Destroy the boss and then load the cutscene
            }
            else
            {
                isAttacking = false;
                StartCoroutine(StartNextWave());
            }
        }
    }

    private void DestroyBoss()
    {
        Debug.Log("Destroying Giant Octopus Boss!");
        Destroy(gameObject);
        LoadEndScene();
    }

    private void LoadEndScene()
    {
        SceneManager.LoadScene("CutScene"); // Load the CutScene
    }
}
