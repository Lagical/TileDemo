using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prayers : MonoBehaviour
{
    private string activePrayer;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite [] allPrayers;

    void Start()
    {
    }
    public void protectMagic()
    {
        activePrayer = "magic";
        spriteRenderer.sprite = allPrayers[0];
    }
    public void protectRanged()
    {
        activePrayer = "ranged";
        spriteRenderer.sprite = allPrayers[1];
    }
    public void protectMelee()
    {
        activePrayer = "melee";
        spriteRenderer.sprite = allPrayers[2];
    }

    public string getPrayer()
    {
        return activePrayer;
    }



    void Update()
    {
        
    }
}
