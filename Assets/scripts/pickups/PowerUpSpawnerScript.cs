using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawnerScript : MonoBehaviour {
    public GameObject sniper;
    public GameObject heavy;
    public GameObject marksmen;
    public GameObject assault;

    private float width;
    private float height;

    public GameObject[] weapons;

    public float spawnTimeMin;
    public float spawnTimeMax;

    // Use this for initialization
    private void Start () {
        weapons = new GameObject[4];

        weapons[0] = Instantiate(sniper);
        weapons[0].SetActive(false);

        weapons[1] = Instantiate(heavy);
        weapons[1].SetActive(false);

        weapons[2] = Instantiate(marksmen);
        weapons[2].SetActive(false);

        weapons[3] = Instantiate(assault);
        weapons[3].SetActive(false);

        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        width = -worldPoint.x;
        height = -worldPoint.y;

        float nextDropTime = Random.Range(spawnTimeMin, spawnTimeMax);
        Invoke("createDrop", nextDropTime);
    }

    private void createDrop() {
        int typeOfWeapon = (int)Mathf.Floor(Random.Range(0, weapons.Length));
        Vector3 randomLocation = randomLocationOnScreen();

        if (!weapons[typeOfWeapon].activeSelf) {
            weapons[typeOfWeapon].gameObject.transform.position = randomLocation;
            weapons[typeOfWeapon].SetActive(true);
        }

        float nextDropTime = Random.Range(spawnTimeMin, spawnTimeMax);
        Invoke("createDrop", nextDropTime);
    }

    private Vector3 randomLocationOnScreen()
    {
        Vector3 newLocation = new Vector3(Random.Range(-width, width), Random.Range(-height, height), 0);
        return newLocation;
    }
}
