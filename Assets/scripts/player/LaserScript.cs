using UnityEngine;

public class LaserScript : MonoBehaviour
{
    public LaserGunState laserGunState;

    private Vector3 startPosition;
    private Vector3 lineEnd;

    private GameObject beamGameObject;
    private LineRenderer beamRenderer;

    // Use this for initialization
    void Start()
    {
        // create an object with a line renderer that we will use as a laser
        beamGameObject = new GameObject();
        beamGameObject.AddComponent<LineRenderer>();
        beamRenderer = beamGameObject.GetComponent<LineRenderer>();

        initializeLaserGunSettings();
    }


    public void getLaserGunState(LaserGunState state) {
        beamRenderer.enabled = false;
        laserGunState.copy(state);

        // set the line renderer settings
        initializeLaserGunSettings();
    }


    public void initializeLaserGunSettings()
    {
        // set default locations for the start and the end line positions
        startPosition = Vector3.zero;
        lineEnd = Vector3.zero;

        beamRenderer.material = new Material(Shader.Find("Particles/Additive"));
        beamRenderer.useWorldSpace = true;

        // set the line renderer settings
        beamRenderer.startWidth = laserGunState.startWidth;
        beamRenderer.endWidth = laserGunState.endWidth;

        beamRenderer.startColor = laserGunState.startColor;
        beamRenderer.endColor = laserGunState.endColor;

        beamRenderer.enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        updateLaser();
    }


    public void shootLaser(Vector3 target)
    {
        bool abletoFire = laserGunState.fire();

        if (!abletoFire)
        {
            return;
        }

        // set the position for the line
        beamRenderer.enabled = true;

        initializeBeamStartAndEndPositions(target);
        beamRenderer.SetPosition(0, startPosition);
        beamRenderer.SetPosition(1, lineEnd * laserGunState.range + startPosition);
        beamHit();
    }


    public float laserBulletsLeftPercent()
    {
        return laserGunState.bulletsLeftPercent();
    }

    public float laserBulletsLeft() {
        return laserGunState.getBulletsLeft();
    }


    private void initializeBeamStartAndEndPositions(Vector3 target)
    {
        // set the target and the start position
        // for machine guns we can vary the end position slightly. 
        startPosition = transform.position;
        lineEnd = target - startPosition;
        lineEnd.z = 0;

        // add recoil here
        lineEnd = lineEnd.normalized * 2.5f;

        lineEnd.x += Random.Range(-laserGunState.recoil, laserGunState.recoil);
        lineEnd.y += Random.Range(-laserGunState.recoil, laserGunState.recoil);

        lineEnd = lineEnd.normalized;
    }
    
    private void beamHit()
    {
        RaycastHit2D[] hits = new RaycastHit2D[3];

        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(LayerMask.GetMask("enemy"));

        // check the length here
        int collidersHit = Physics2D.Raycast(startPosition + lineEnd * .2f, lineEnd, filter, hits);
        damageEnemies(collidersHit, hits);
    }


    private void damageEnemies(int collidersHit, RaycastHit2D[] hits)
    {
        if (collidersHit > 0)
        {
            Collider2D hitCollider;
            for (int i = 0; i < collidersHit; i++)
            {
                hitCollider = hits[i].collider;
                hitCollider.GetComponent<EnemyScript>().hit(laserGunState.power);
            }
        }
    }


    private void updateLaser()
    {
        float timeLeftToFirePercent = laserGunState.timeLeftToFirePercent();

        if (timeLeftToFirePercent >= 1)
        {
            beamRenderer.enabled = false;
        }
        else
        {
            Vector3 frameStartPosition = (lineEnd * laserGunState.range * timeLeftToFirePercent) + startPosition;

            beamRenderer.SetPosition(0, frameStartPosition);
            beamRenderer.SetPosition(1, lineEnd * laserGunState.range + startPosition);
        }
    }
}
