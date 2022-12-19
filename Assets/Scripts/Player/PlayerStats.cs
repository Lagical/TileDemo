using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private GameStatus gameStatus;
    [SerializeField] private int hitpoints = 99;
    [SerializeField] private int prayerpoints = 99;
    [SerializeField] private int staminapoints = 99;
    [SerializeField] private TextMeshProUGUI playerHP, playerPrayer, playerStamina;
    [SerializeField] private float attackRange;
    //[SerializeField] private Prayers prayers;
    private Vector3 bossPosition;

    void Start()
    {
    }
    public Vector3 getPlayerPosition()
    {
        return transform.position;
    }

    public int getHitpoints()
    {
        return hitpoints;
    }
    public int setHitpoints(int newHitpoints)
    {
        playerHP.text = "PlayerHP: " + newHitpoints.ToString() + "/" + 99;
        return hitpoints = newHitpoints;
    }

    public int getPrayerpoints()
    {
        return prayerpoints;
    }
    public int setPrayerpoints(int prayerDrain)
    {
        int newPrayerpoints = prayerpoints - prayerDrain;
        playerPrayer.text = "Prayer: " + newPrayerpoints.ToString() + "/" + 99;
        return prayerpoints = newPrayerpoints;
    }

    void Update()
    {
        if(hitpoints < 1){
            gameStatus.Restart();
        }
    }
}
