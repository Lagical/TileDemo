using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prayers : MonoBehaviour
{
    private string activePrayer = "";
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite [] allPrayers;
    [SerializeField] private PlayerStats playerStats;
    private int prayerDrain = 1;

    void Start()
    {
    }
    public void protectMagic()
    {
        StopAllCoroutines();
        if (activePrayer == "magic")
        {
            spriteRenderer.sprite = null;
            activePrayer = "";
        } else if (playerStats.getPrayerpoints() > 0 && activePrayer != "magic")
        {
            activePrayer = "magic";
            spriteRenderer.sprite = allPrayers[0];
            StartCoroutine(prayerDraining());
        }
    }
    public void protectRanged()
    {
        StopAllCoroutines();
        if (activePrayer == "ranged")
        {
            spriteRenderer.sprite = null;
            activePrayer = "";
        } else if (playerStats.getPrayerpoints() > 0 && activePrayer != "ranged")
        {
            activePrayer = "ranged";
            spriteRenderer.sprite = allPrayers[1];
            StartCoroutine(prayerDraining());
        }
    }
    public void protectMelee()
    {
        StopAllCoroutines();
        if (activePrayer == "melee")
        {
            spriteRenderer.sprite = null;
            activePrayer = "";
        } else if (playerStats.getPrayerpoints() > 0 && activePrayer != "melee")
        {
            activePrayer = "melee";
            spriteRenderer.sprite = allPrayers[2];
            StartCoroutine(prayerDraining());
        }
    }

    public int getPrayerDrain()
    {
        return prayerDrain;
    }

    public string getPrayer()
    {
        return activePrayer;
    }

    private IEnumerator prayerDraining()
    {
        while (activePrayer != "")
        {
           yield return new WaitForSeconds(1f);
           playerStats.setPrayerpoints(prayerDrain);
        }
    }

    void Update()
    {
        
    }
}
