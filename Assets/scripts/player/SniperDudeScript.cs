using UnityEngine;

public class SniperDudeScript : MonoBehaviour {
    public float defaultSpeed;
    public float health;
    public float ramCooldown;

    private bool ramOn;
    private float speed;
    private float ramTime;
    private Camera cam;
    private LaserScript lasergun;
    private Inventory inventory;

	// Use this for initialization
	void Start ()
    {
        cam = Camera.main;
        lasergun = GetComponentInChildren<LaserScript>();
        inventory = GetComponent<Inventory>();

        ramTime = ramCooldown;
        speed = defaultSpeed;
        ramOn = false;
    }


    // Update is called once per frame
    void Update ()
    {
        Vector3 target = cam.ScreenToWorldPoint(Input.mousePosition);

        rotateSniperDude(target);
        moveSniperDude();

        if (Input.GetMouseButton(0))
        {
            lasergun.shootLaser(target);
        }

        ramTime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (ramTime >= ramCooldown) {
                ramOn = true;
                speed = 3.0f;
                ramTime = 0f;
                Invoke("setSpeedToNormal", 1);
            }
        }

        
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            LaserGunState state = inventory.useInventory(0);

            if (state != null)
            {
                getPickup(state);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LaserGunState state = inventory.useInventory(1);

            if (state != null) {
                getPickup(state);
            }
        }
    }

    private void gameOver() {
        // when health reaches 0
    }


    public float ramCooldownDonePercent() {
        return ramTime >= ramCooldown ? 1 : ramTime / ramCooldown;
    }


    public float sniperdudeHealthPercent() {
        return health / 100f;
    }

    public int getSniperdudeHealth() {
        return (int)health;
    }

    public void storePickup(GameObject gameObj) {
        inventory.putInInventory(gameObj);
    }

    public void getPickup(LaserGunState state) {
        lasergun.getLaserGunState(state);
    }


    private void setSpeedToNormal() {
        ramOn = false;
        speed = defaultSpeed;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("enemy")) {
            return;
        }

        if (ramOn) {
            collision.gameObject.GetComponent<EnemyScript>().hit(100f);
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("enemy")) {
            return;
        }

        float percentDamage = Time.deltaTime / 1;
        if (ramOn) {
            collision.gameObject.GetComponent<EnemyScript>().hit(100f);
        }
        else {
            health -= 20f * percentDamage;
            collision.gameObject.GetComponent<EnemyScript>().hit(50f * percentDamage);
        }
    }


    private void rotateSniperDude(Vector3 target)
    {
        // get mouse position then rotate sniper dude
        Vector3 direction = target - transform.position;

        // get player position, move and rotate towards that position
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
    }


    private void moveSniperDude()
    {
        // get axis for sniper dude and move him about
        Vector3 movementInDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized * Time.deltaTime * speed;
        transform.position += movementInDirection;
    }
}
