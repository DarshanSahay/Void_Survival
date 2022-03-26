using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private ObjectPool pool;
    private float minX, maxX;
    private float minY, maxY;
    private float xValue, yValue;
    [SerializeField] private GameObject[] enemyPrefab;
    [SerializeField] private GameObject[] asteroid;
    void OnEnable()
    {
        minX = -6f;  minY = -2f;
        maxX =  6f;  maxY =  2f;
        pool = ObjectPool.Instance;

        Invoke(nameof(SpawnEnemies), 2f);
        Invoke(nameof(SpawnAsteroid), 5f);
        Invoke(nameof(SpawnPowerUp), 30f);
    }
    private Vector3 GetRandomLocation()                                       //get random loaction within the spawn range
    {
        xValue = Random.Range(minX, maxX);
        yValue = Random.Range(minY, maxY);
        return new Vector3(xValue, yValue, 0f);
    }
    private GameObject GetRandomPrefab(GameObject[] prefab)
    {
        GameObject obj = prefab[Random.Range(0, prefab.Length)];
        return obj;
    }
    private void SpawnEnemies()                                             //get enemy prefabs from object pool and activate them
    { 
        GameObject enemy = pool.GetPooledObject(GetRandomPrefab(enemyPrefab).tag);
        if (enemy != null)
        {
            enemy.transform.position = GetRandomLocation();
            enemy.transform.rotation = Quaternion.identity;
            enemy.gameObject.SetActive(true);
        }
        Invoke(nameof(SpawnEnemies), 2f);
    }
    private void SpawnAsteroid()                                           //get asteroid prefabs from object pool and activate them
    {
        GameObject obj = pool.GetPooledObject(GetRandomPrefab(asteroid).tag);
        if (obj != null)
        {
            obj.transform.position = GetRandomLocation();
            obj.transform.rotation = Quaternion.identity;
            obj.gameObject.SetActive(true);
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.down * 10f;
        }
        Invoke(nameof(SpawnAsteroid), 5f);
    }

    private void SpawnPowerUp()                                            //get powerUp prefab from object pool and activate it
    {
        GameObject power = pool.GetPooledObject("PowerUp");
        if (power != null)
        {
            power.transform.position = GetRandomLocation();
            power.transform.rotation = Quaternion.identity;
            power.gameObject.SetActive(true);

            Rigidbody2D rb = power.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.down * 10f;
        }
        Invoke(nameof(SpawnPowerUp), 30f);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
}
