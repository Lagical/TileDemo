using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampingBurstAttack : MonoBehaviour
{
    [SerializeField] GameObject[] projectiles;
    [SerializeField] private BossStats bossStats;
    [SerializeField] private VelkotLaserwipe laser;

    private int rand;
    private float burstAttackTime;
    private float attackTime;
    private int burstAttacks;
    private float startHp;
    // Start is called before the first frame update
    void Start()
    {
        burstAttackTime = 5.0f;
        attackTime = 4.0f;
        burstAttacks = 2;
        
        bossStats = GameObject.Find("Boss").GetComponent<BossStats>();
        startHp = bossStats.getHitpoints();
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(burstAttackTime);
            for (int i = 0; i < burstAttacks; i++)
            {
                
                rand = Random.Range(0, projectiles.Length);
                float rand1 = Random.Range(0, 4);
                if (/*rand1 == 1 && */!laser.laserEnabled)
                    StartCoroutine(laser.RightSwipe());

                //Instantiate(projectiles[rand], transform.position, Quaternion.identity);

                // Estää, että aikaa ei odoteta viimeisen iskun jälkeen
                if (i < burstAttacks - 1)
                    yield return new WaitForSeconds(attackTime);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        float hp = bossStats.getHitpoints();
        float relation = hp / startHp;
        if (relation < 0.2f)
        {
            attackTime = 1.0f;
            burstAttacks = 4;
            burstAttackTime = 2.5f;
        }
        else if (relation < 0.4f)
        {
            attackTime = 1.5f;
            burstAttackTime = 3.5f;
            burstAttacks = 3;
        }
        else if (relation < 0.6f)
        {
            attackTime = 1.5f;
            burstAttackTime = 4f;
            burstAttacks = 3;
        }
        else if (relation < 0.8f)
        {
            burstAttackTime = 4.0f;
            attackTime = 2.0f;
            burstAttacks = 2;
        }
        
    }

    public int getAttackStyle()
    {
        return rand;
    }
}
