using UnityEngine;

public class LaserGunState : MonoBehaviour
{
    public int power;
    public float timeForOneRound; // the time it takes to fire one round
    public int magazineSize;
    public float reloadTime;
    public float range;
    public float recoil;

    public Color startColor;
    public Color endColor;
    public float startWidth;
    public float endWidth;

    private int bulletsLeft;
    private float laserSpeed = .3f;
    private float timeSinceLastShotFired = 0f;
    private bool reloading = false;
    private float reloadingTimeAmount = 0f;


    private void Start()
    {
        bulletsLeft = magazineSize;
    }


    private void Update()
    {
        timeSinceLastShotFired += Time.deltaTime;
        if (reloading) {
            reloadingTimeAmount += Time.deltaTime;
        }
    }


    public LaserGunState(int powerVal, int timeForOneRoundVal, int magazineSizeVal, float reloadTimeVal, Color startColorVal, Color endColorVal, float startWidthVal, float endWidthVal) {
        power = powerVal;
        timeForOneRound = timeForOneRoundVal;
        magazineSize = magazineSizeVal;
        reloadTime = reloadTimeVal;
        startColor = startColorVal;
        endColor = endColorVal;
        startWidth = startWidthVal;
        endWidth = endWidthVal;
        // i should add a max range to the laser beam depending on the type of weapon that it is

        bulletsLeft = magazineSizeVal;
        timeSinceLastShotFired = 0f;
        reloading = false;
    }


    public void copy(LaserGunState state) {
        power = state.power;
        timeForOneRound = state.timeForOneRound;
        magazineSize = state.magazineSize;
        reloadTime = state.reloadTime;

        startColor.r = state.startColor.r;
        startColor.g = state.startColor.g;
        startColor.b = state.startColor.b;
        endColor.r = state.endColor.r;
        endColor.g = state.endColor.g;
        endColor.b = state.endColor.b;

        recoil = state.recoil;
        range = state.range;
        startWidth = state.startWidth;
        endWidth = state.endWidth;
        bulletsLeft = magazineSize;
        timeSinceLastShotFired = 1f;
        reloading = false;
        reloadingTimeAmount = 0f;
    }


    public bool magazineFull() {
        return bulletsLeft == magazineSize;
    }


    public float bulletsLeftPercent() {
        if (reloading) {
            return reloadingTimeAmount / reloadTime;
        }
        else {
            return (float)bulletsLeft / magazineSize;
        }
    }


    public int getBulletsLeft() {
        return bulletsLeft;
    }


    public bool fire() {
        if (reloading || timeSinceLastShotFired < timeForOneRound) {
            return false;
        }

        timeSinceLastShotFired = 0f;
        bulletsLeft--;
        if (bulletsLeft == 0) {
            reloading = true;
            Invoke("reloadingBullets", reloadTime);
        }

        return true;
    }


    public float timeLeftToFirePercent() {
        return timeSinceLastShotFired / Mathf.Min(laserSpeed, timeForOneRound);
    }


    private void reloadingBullets()
    {
        if (reloading == false) {
            return;
        }

        bulletsLeft = magazineSize;
        reloading = false;
        reloadingTimeAmount = 0f;
    }
}
