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
        StartCoroutine(StaminaRegen());
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
        playerHP.text = "HP: " + newHitpoints.ToString() + "/" + 99;
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

    public int getStamina()
    {
        return staminapoints;
    }

    public int setStamina(int staminaDrain)
    {
        int newStamina = staminapoints - staminaDrain;
        playerStamina.text = "Stam: " + newStamina.ToString() + "/" + 99;
        return staminapoints = newStamina;
    }

    IEnumerator StaminaRegen()
    {
        while (true)
        {
            if(staminapoints < 99)
            {
                yield return new WaitForSeconds(1f);
                int newStamina = staminapoints + 1;
                playerStamina.text = "Stam: " + newStamina.ToString() + "/" + 99;
                staminapoints = newStamina;
            } else
            {
                yield return new WaitForSeconds(1f);
            }
        }
     }

    void Update()
    {
        if(hitpoints < 1){
            gameStatus.Restart();
        }
    }
}
