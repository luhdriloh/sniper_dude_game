using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickupScript : MonoBehaviour {
    private bool pickedUp;

    private void Start()
    {
        pickedUp = false;
    }

    private void OnEnable()
    {
        pickedUp = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (pickedUp) {
            return;
        }

        // put this in the inventory
        collision.gameObject.GetComponent<SniperDudeScript>().storePickup(gameObject);
        pickedUp = true;
        gameObject.SetActive(false);
    }
}
