using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject[] projectiles;
    [SerializeField] BossStats bossStats;
    private bool attacking = false;

    void Start()
    {
    }

    public void tryToAttack()
    {
        if(attacking == false)
        {
            attacking = true;
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        while (bossStats.getHitpoints()>1 && attacking == true)
        {
            int rand1 = Random.Range(0, projectiles.Length);
            Instantiate(projectiles[rand1], transform.position, Quaternion.identity);
            yield return new WaitForSeconds(2.5f);
            int rand = Random.Range(0, projectiles.Length);
            Instantiate(projectiles[rand], transform.position, Quaternion.identity);
        }
    }
}
