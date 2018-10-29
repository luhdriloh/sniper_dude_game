using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
    public float health;
    public float speed;

    private GameObject player;
    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start ()
    {
        health = 100f;
        player = GameObject.FindWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 direction = player.transform.position - transform.position;

        rotateEnemyTowardsPlayer(direction);
        moveTowardPlayer(direction);
	}

    public void initialize() {
        health = 100f;
        gameObject.SetActive(true);
    }

    public void hit(float power)
    {
        health -= power;
        spriteRenderer.color = getNewColor();

        if (health <= 0) {
            gameObject.SetActive(false);
            spriteRenderer.color = Color.white;
        }
    }

    private void rotateEnemyTowardsPlayer(Vector3 direction)
    {
        // get player position, move and rotate towards that position
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
    }


    private void moveTowardPlayer(Vector3 direction) 
    {
        Vector3 movement = direction.normalized * speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x + movement.x, transform.position.y + movement.y, 0);
    }


    private Color getNewColor() {
        Color original = Color.white;
        Color end = Color.red;
        float percentDead = (100f - health) / 100f;

        float newR = original.r + (end.r - original.r) * percentDead;
        float newG = original.g + (end.g - original.g) * percentDead;
        float newB = original.b + (end.b - original.b) * percentDead;

        return new Color(newR, newG, newB, 1); ;
    }
}























