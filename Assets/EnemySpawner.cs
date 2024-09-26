using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float minTimer = 1.5f, maxTimer = 3;

    private float timer;

    void Start()
    {
        timer = Random.Range(minTimer, maxTimer);
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0) {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            timer = Random.Range(minTimer, maxTimer);
        }
    }
}
