using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsBar : MonoBehaviour {
    public LaserScript laser;
    public Image laserCooldown;
    public Text laserBulletsLeft;

    public SniperDudeScript sniperDude;
    public Image ramCooldown;
    public Image health;
    public Text healthText;

    // Update is called once per frame
    private void Update () {
        HandleStats();
	}

    private void HandleStats() {
        laserCooldown.fillAmount = laser.laserBulletsLeftPercent();
        ramCooldown.fillAmount = sniperDude.ramCooldownDonePercent();
        health.fillAmount = sniperDude.sniperdudeHealthPercent();

        laserBulletsLeft.text = laser.laserBulletsLeft().ToString();
        healthText.text = sniperDude.getSniperdudeHealth().ToString();
    }
}
