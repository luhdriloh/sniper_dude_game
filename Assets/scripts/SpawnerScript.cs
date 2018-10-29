using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {
    public float spawnSpeed;
    public int poolSize;
    public GameObject enemyPrototype;

    private float width;
    private float height;
    private float lastSpawn;

    private GameObject[] enemyPool;

	// Use this for initialization
	void Start () {
        // lets get the area that we can place our enemies
        enemyPool = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++) {
            enemyPool[i] = Instantiate(enemyPrototype);
        }

        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        width = -worldPoint.x;
        height = -worldPoint.y;

        lastSpawn = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        lastSpawn += Time.deltaTime;

        if (lastSpawn >= spawnSpeed) {
            for (int i = 0; i < poolSize; i++) {
                if (enemyPool[i].gameObject.activeSelf == false) {
                    EnemyScript enemy = enemyPool[i].GetComponent<EnemyScript>();

                    enemy.initialize();
                    enemyPool[i].transform.position = randomLocationOnScreen();
                    break;
                }
            }

            lastSpawn = 0f;
        }
	}

    private Vector3 randomLocationOnScreen() {
        // TODO move away from player if too close
        Vector3 newLocation = new Vector3(Random.Range(-width, width), Random.Range(-height, height), 0);
        return newLocation;
    }
}
