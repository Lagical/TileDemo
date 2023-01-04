using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Healer : Minion
{
    [SerializeField] private TextMeshPro healsplash;
    private HealthBar bossHB;
    protected override void Start()
    {
        base.Start(); // Etsii bossin ja playerin
        healsplash = GameObject.Find("Healsplash").GetComponent<TextMeshPro>();
        bossHB = GameObject.Find("BossHB").GetComponent<HealthBar>();
        healsplash.enabled = false;
    }

    protected override void Update()
    {
        base.Update();  // Tarkistaa mm. onko minionin hp:t 0
        if (!IsAttacked())
        {
            FollowBoss();
            //WalkAround();
            return;
        }
        FollowPlayer();
    }

    private IEnumerator HealsplashShown()
    {
        yield return new WaitForSeconds(1.5f);
        healsplash.enabled = false;
    }

    protected override void InteractionWithBoss()
    {
        base.InteractionWithBoss(); // Ei tee t‰ll‰ hetkell‰ mit‰‰n, tekee ehk‰ joskus jtn??
        int bossHp = bossStats.getHitpoints();
        if(bossStats.getHitpoints() < 245)
        {
            healsplash.enabled = true;
            StartCoroutine(HealsplashShown());
            bossStats.setHitpoints(bossHp + GetHealPower());
            bossHB.setHealthBarUp(GetHealPower());
            healsplash.text = "+" + GetHealPower().ToString();
        }
    }

    protected override void InteractionWithPlayer()
    {
        base.InteractionWithPlayer(); // Ei tee t‰ll‰ hetkell‰ mit‰‰n, tekee ehk‰ joskus jtn??
        int playerHp = playerStats.getHitpoints();
        playerStats.setHitpoints(playerHp - GetAttackDamage());
    }
}
