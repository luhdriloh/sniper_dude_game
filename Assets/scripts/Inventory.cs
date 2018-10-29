using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public int numberOfItems;

    public GameObject slot1;
    public GameObject slot2;

    private GameObject[] inventory;
    private SpriteRenderer[] renderers;


    private void Start()
    {
        numberOfItems = 2;
        inventory = new GameObject[numberOfItems];
        renderers = new SpriteRenderer[numberOfItems];

        renderers[0] = slot1.GetComponent<SpriteRenderer>();
        renderers[1] = slot2.GetComponent<SpriteRenderer>();
    }

    public void putInInventory(GameObject gameObj)
    {
        for (int i = 0; i < numberOfItems; i++) {
            if (inventory[i] == null) {
                inventory[i] = gameObj;
                renderers[i].sprite = gameObj.GetComponent<SpriteRenderer>().sprite;
                renderers[i].color = gameObj.GetComponent<SpriteRenderer>().color;
                return;
            }
        }
    }

    public LaserGunState useInventory(int slot)
    {
        if (inventory[slot] == null) {
            return null;
        }

        LaserGunState toReturn = inventory[slot].GetComponent<LaserGunState>();
        inventory[slot] = null;
        renderers[slot].sprite = null;

        return toReturn;
    }

    public GameObject[] getInventory() {
        return inventory;
    }
}
